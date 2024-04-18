using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuscaEmLargura : AlgoritmoDeBusca
{
    private Grafo _grafo;

    override public IEnumerator Comecar(Grafo grafo, ColorConsts cc)
    {
        _grafo = grafo;
        colorConsts = cc;
        Dictionary<Node, Node> nosAnteriores = new Dictionary<Node, Node>();
        Queue<Node> fila = new Queue<Node>();
        fila.Enqueue(grafo.PosicaoInicial());

        while (fila.Count > 0)
        {
            Node noAtual = fila.Dequeue();
            if (noAtual.status == colorConsts.DESCOBERTO)
            {
                noAtual.MudarStatus(colorConsts.VISITADO);
            }

            foreach (Node no in grafo.NosVizinhos(noAtual.position))
            {
                Debug.Log("Descobrindo o n� " + no.position);
                Color status = no.status;

                if (status == colorConsts.PLAYER)
                {
                    Debug.Log("Encontrou o jogador");
                    nosAnteriores.Add(no, noAtual);
                    PlayerEncontrado(nosAnteriores, noAtual);
                    yield break;
                }

                if (status == colorConsts.NAO_DESCOBERTO)
                {
                    nosAnteriores.Add(no, noAtual);
                    no.MudarStatus(grafo.colorConsts.DESCOBERTO);
                    fila.Enqueue(no);
                }
                yield return null;
            }
            yield return null;
        }

        grafo.GerarEstatisticas();
    }

    private void PlayerEncontrado(Dictionary<Node, Node> nosAnteriores, Node ultimoNo)
    {
        List<Node> caminho = new List<Node>();
        Node noAtual = ultimoNo;

        do
        {
            caminho.Add(noAtual);
            noAtual = nosAnteriores[noAtual];
        } while (noAtual.position != _grafo.PosicaoInimigo);

        _grafo.MostrarCaminho(caminho);
    }
}
/*  Documenta��o de N� (Node)
 *  
 *  (int x, int y) Posicao;
 *  
 *  AlterarEstatoPara(ESTADO);
 *  Os posiveis estados s�o na v�riavel Estado
 *  sendo eles:
 *  Estado.Descoberto
 *  Estado.Visitado
 *  Tem mais alguns mas acho q n�o v�o precisar usar
 *  
 *  Documenta��o de Grafo
 * 
 * -List<Node> NosVizinhos((int, int) posicao);
 *  Recebe a posi��o de um n� e retorna seus vizinhos
 * 
 * - float Heuristica((int, int) posicao);
 *  Recebe a posi�a� de um n� e calcula a distancia esperada
 * 
 */
