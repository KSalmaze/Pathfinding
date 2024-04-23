using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuscaEmLargura : AlgoritmoDeBusca
{
    private Grafo _grafo;

    // Corotina para a execução do algoritmo de Busca em Largura
    override public IEnumerator Comecar(Grafo grafo, ColorConsts cc)
    {
        _grafo = grafo; // Armazena a referência do grafo.
        colorConsts = cc; // Configura as constantes de cores.

        // Dicionário para amazenar o caminho para cada nó
        Dictionary<Node, Node> nosAnteriores = new Dictionary<Node, Node>();
        // Fila necessária para a execução do método
        Queue<Node> fila = new Queue<Node>();

        // Inicializa a fila com o primeiro nó
        fila.Enqueue(grafo.PosicaoInicial());

        // Enquanto houver algum nó na fila, continua a busca
        while (fila.Count > 0)
        {
            // Define o nó atual como o primeiro da fila
            Node noAtual = fila.Dequeue();


            // Se o nó atual já tiver sido descoberto, ele é marcado como visitado
            if (noAtual.status == colorConsts.DESCOBERTO)
            {
                noAtual.MudarStatus(colorConsts.VISITADO);
            }

            // Para cada nó vizinho ao nó atual
            foreach (Node no in grafo.NosVizinhos(noAtual.position))
            {
                Debug.Log("Descobrindo o nó " + no.position);

                // Verifica o status do vizinho.
                Color status = no.status;

                // Se o vizinho for o jogador
                if (status == colorConsts.PLAYER)
                {
                    Debug.Log("Encontrou o jogador");
                    // Marca o nó atual como o nó anterior ao jogador
                    nosAnteriores.Add(no, noAtual);

                    // Chama a função de Player encontrado
                    PlayerEncontrado(nosAnteriores, noAtual);

                    // Finaliza a busca
                    yield break;
                }

                // Se o nó ainda não tiver sido descoberto, marca como descoberto
                if (status == colorConsts.NAO_DESCOBERTO)
                {
                    nosAnteriores.Add(no, noAtual);
                    no.MudarStatus(grafo.colorConsts.DESCOBERTO);

                    // Adiciona na fila
                    fila.Enqueue(no);
                }

                // Passa para o próximo frame para atualizar a tela
                yield return null;
            }

            // Passa para o próximo frame para atualizar a tela
            yield return null;
        }

        grafo.GerarEstatisticas();
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
/*  Documentação de Nó (Node)
 *  
 *  (int x, int y) Posicao;
 *  
 *  AlterarEstatoPara(ESTADO);
 *  Os posiveis estados são na váriavel Estado
 *  sendo eles:
 *  Estado.Descoberto
 *  Estado.Visitado
 *  Tem mais alguns mas acho q não vão precisar usar
 *  
 *  Documentação de Grafo
 * 
 * -List<Node> NosVizinhos((int, int) posicao);
 *  Recebe a posição de um nó e retorna seus vizinhos
 * 
 * - float Heuristica((int, int) posicao);
 *  Recebe a posiçaõ de um nó e calcula a distancia esperada
 * 
 */
