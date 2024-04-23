using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A classe AStar estende AlgoritmoDeBusca e implementa IComparer para poder comparar nós.
public class AStar : AlgoritmoDeBusca, IComparer<Node>
{
    Grafo _grafo;
    // Dicionário para manter o custo de chegar até cada nó.
    Dictionary<Node, double> custos;

    // Corrotina que inicia a execução do algoritmo A*.
    public override IEnumerator Comecar(Grafo grafo, ColorConsts cc)
    {
        colorConsts = cc; // Configura as constantes de cores.
        _grafo = grafo; // Armazena a referência do grafo.

        // Listas para gerenciar nós abertos e fechados.
        List<Node> nosAbertos = new List<Node>();
        List<Node> nosFechados = new List<Node>();

        // Inicializa os custos e o caminho.
        custos = new Dictionary<Node, double>();
        Dictionary<Node, Node> caminho = new Dictionary<Node, Node>();

        // Adiciona o nó inicial às estruturas de dados.
        nosAbertos.Add(grafo.PosicaoInicial());
        custos.Add(grafo.PosicaoInicial(),0);
        caminho.Add(grafo.PosicaoInicial(),null);

        // Enquanto houver nós abertos, continua a busca.
        while (nosAbertos.Count > 0)
        {
            // Ordena os nós abertos com base no custo e heurística.
            nosAbertos.Sort(Compare);
            Node noAtual = nosAbertos[0];
            Debug.Log("Novo nó : " + noAtual.position + "Com custo :" + custos[noAtual] + " Heristica :" + grafo.Heuristica(noAtual.position));

            // Se o nó atual foi apenas descoberto, agora é marcado como visitado.
            if (noAtual.status == colorConsts.DESCOBERTO)
            {
                noAtual.MudarStatus(colorConsts.VISITADO);
            }

            // Remove o nó atual dos abertos e o adiciona aos fechados.
            nosAbertos.Remove(noAtual);
            nosFechados.Add(noAtual);

            // Percorre todos os vizinhos do nó atual.
            foreach (Node no in grafo.NosVizinhos(noAtual.position))
            {
                // Verifica o status do vizinho.
                Color status = no.status;

                // Se for um nó não descoberto, marca como descoberto.
                if (status == colorConsts.NAO_DESCOBERTO)
                {
                    no.MudarStatus(colorConsts.DESCOBERTO);
                }
                // Se for o jogador, encontramos o caminho!
                else if (status == colorConsts.PLAYER)
                {
                    PlayerEncontrado(caminho,noAtual);
                    
                    // Finaliza o algoritmo
                    yield break;
                }

                // Se o nó já foi explorado ou é um obstáculo, ignora.
                if (nosFechados.Contains(no) || status == colorConsts.OBSTACULO)
                {
                    continue;
                }

                // Calcula o novo custo para o vizinho.
                double novoCusto = custos[noAtual] + 1;

                // Se é um novo nó ou temos um caminho melhor, atualiza o custo e o caminho.
                if (!nosAbertos.Contains(no) || novoCusto < custos[no])
                {
                    // Atualiza o custo e o caminho até esse nó
                    custos[no] = novoCusto;
                    caminho[no] = noAtual;

                    // Se não está na lista de abertos, adiciona para futura exploração.
                    if (!nosAbertos.Contains(no))
                    {
                        Debug.Log("Inserindo na lista: " + no.position + "Com custo : "
                           + (_grafo.Heuristica(no.position) - custos[no]));
                        nosAbertos.Add(no);
                    }
                }
                // Passa para o próximo frame para atualizar a tela
                yield return null;
            }
            // Passa para o próximo frame para atualizar a tela
            yield return null;
        }

        // Após concluir a busca, gera estatísticas de desempenho.
        grafo.GerarEstatisticas();
        yield break; // Encerra o algoritmo
    }

    // Método para comparar dois nós com base no custo f(n).
    public int Compare(Node a, Node b)
    {
        // Calcula o custo f(n) para os dois nós.
        double custoA = _grafo.Heuristica(a.position) + custos[a];
        double custoB = _grafo.Heuristica(b.position) + custos[b];

        // Compara os custos para determinar a ordem na lista de nós abertos.
        return custoA.CompareTo(custoB);
    }

    // Método privado para tratar o encontro com o jogador.
    private void PlayerEncontrado(Dictionary<Node, Node> nosAnteriores, Node ultimoNo)
    {
        // Caso Player esteja do lado do Inimigo
        if (ultimoNo == _grafo.PosicaoInicial())
        {
            Debug.Log("Problema detectado");
            _grafo.MostrarCaminho(null);
            return;
        }

        // Lista para armazenar o caminho encontrado.
        List<Node> caminho = new List<Node>();
        // Começa pelo último nó (onde o jogador foi encontrado).
        Node noAtual = ultimoNo;

        // Retrocede pelo caminho usando o dicionário até chegar ao início.
        do
        {
            caminho.Add(noAtual);
            noAtual = nosAnteriores[noAtual];
        } while (noAtual.position != _grafo.PosicaoInimigo);
        // O loop termina quando chega à posição do inimigo.

        // Após construir o caminho, mostra-o no jogo.
        _grafo.MostrarCaminho(caminho);
    }
}
