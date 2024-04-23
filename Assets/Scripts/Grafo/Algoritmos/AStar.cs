using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A classe AStar estende AlgoritmoDeBusca e implementa IComparer para poder comparar n�s.
public class AStar : AlgoritmoDeBusca, IComparer<Node>
{
    Grafo _grafo;
    // Dicion�rio para manter o custo de chegar at� cada n�.
    Dictionary<Node, double> custos;

    // Corrotina que inicia a execu��o do algoritmo A*.
    public override IEnumerator Comecar(Grafo grafo, ColorConsts cc)
    {
        colorConsts = cc; // Configura as constantes de cores.
        _grafo = grafo; // Armazena a refer�ncia do grafo.

        // Listas para gerenciar n�s abertos e fechados.
        List<Node> nosAbertos = new List<Node>();
        List<Node> nosFechados = new List<Node>();

        // Inicializa os custos e o caminho.
        custos = new Dictionary<Node, double>();
        Dictionary<Node, Node> caminho = new Dictionary<Node, Node>();

        // Adiciona o n� inicial �s estruturas de dados.
        nosAbertos.Add(grafo.PosicaoInicial());
        custos.Add(grafo.PosicaoInicial(),0);
        caminho.Add(grafo.PosicaoInicial(),null);

        // Enquanto houver n�s abertos, continua a busca.
        while (nosAbertos.Count > 0)
        {
            // Ordena os n�s abertos com base no custo e heur�stica.
            nosAbertos.Sort(Compare);
            Node noAtual = nosAbertos[0];
            Debug.Log("Novo n� : " + noAtual.position + "Com custo :" + custos[noAtual] + " Heristica :" + grafo.Heuristica(noAtual.position));

            // Se o n� atual foi apenas descoberto, agora � marcado como visitado.
            if (noAtual.status == colorConsts.DESCOBERTO)
            {
                noAtual.MudarStatus(colorConsts.VISITADO);
            }

            // Remove o n� atual dos abertos e o adiciona aos fechados.
            nosAbertos.Remove(noAtual);
            nosFechados.Add(noAtual);

            // Percorre todos os vizinhos do n� atual.
            foreach (Node no in grafo.NosVizinhos(noAtual.position))
            {
                // Verifica o status do vizinho.
                Color status = no.status;

                // Se for um n� n�o descoberto, marca como descoberto.
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

                // Se o n� j� foi explorado ou � um obst�culo, ignora.
                if (nosFechados.Contains(no) || status == colorConsts.OBSTACULO)
                {
                    continue;
                }

                // Calcula o novo custo para o vizinho.
                double novoCusto = custos[noAtual] + 1;

                // Se � um novo n� ou temos um caminho melhor, atualiza o custo e o caminho.
                if (!nosAbertos.Contains(no) || novoCusto < custos[no])
                {
                    // Atualiza o custo e o caminho at� esse n�
                    custos[no] = novoCusto;
                    caminho[no] = noAtual;

                    // Se n�o est� na lista de abertos, adiciona para futura explora��o.
                    if (!nosAbertos.Contains(no))
                    {
                        Debug.Log("Inserindo na lista: " + no.position + "Com custo : "
                           + (_grafo.Heuristica(no.position) - custos[no]));
                        nosAbertos.Add(no);
                    }
                }
                // Passa para o pr�ximo frame para atualizar a tela
                yield return null;
            }
            // Passa para o pr�ximo frame para atualizar a tela
            yield return null;
        }

        // Ap�s concluir a busca, gera estat�sticas de desempenho.
        grafo.GerarEstatisticas();
        yield break; // Encerra o algoritmo
    }

    // M�todo para comparar dois n�s com base no custo f(n).
    public int Compare(Node a, Node b)
    {
        // Calcula o custo f(n) para os dois n�s.
        double custoA = _grafo.Heuristica(a.position) + custos[a];
        double custoB = _grafo.Heuristica(b.position) + custos[b];

        // Compara os custos para determinar a ordem na lista de n�s abertos.
        return custoA.CompareTo(custoB);
    }

    // M�todo privado para tratar o encontro com o jogador.
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
        // Come�a pelo �ltimo n� (onde o jogador foi encontrado).
        Node noAtual = ultimoNo;

        // Retrocede pelo caminho usando o dicion�rio at� chegar ao in�cio.
        do
        {
            caminho.Add(noAtual);
            noAtual = nosAnteriores[noAtual];
        } while (noAtual.position != _grafo.PosicaoInimigo);
        // O loop termina quando chega � posi��o do inimigo.

        // Ap�s construir o caminho, mostra-o no jogo.
        _grafo.MostrarCaminho(caminho);
    }
}
