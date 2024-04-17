using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : AlgoritmoDeBusca, IComparer<Node>
{
    Grafo _grafo;
    Dictionary<Node, double> custos;

    public override IEnumerator Comecar(Grafo grafo, ColorConsts cc)
    {
        colorConsts = cc;
        _grafo = grafo;

        List<Node> nosAbertos = new List<Node>();
        List<Node> nosFechados = new List<Node>();

        custos = new Dictionary<Node, double>();
        Dictionary<Node, Node> caminho = new Dictionary<Node, Node>();

        nosAbertos.Add(grafo.PosicaoInicial());
        custos.Add(grafo.PosicaoInicial(),0);
        caminho.Add(grafo.PosicaoInicial(),null);

        while (nosAbertos.Count > 0)
        {
            nosAbertos.Sort(Compare);
            Node noAtual = nosAbertos[0];
            Debug.Log("Novo nó : " + noAtual.position);

            if (noAtual.status == colorConsts.DESCOBERTO)
            {
                noAtual.MudarStatus(colorConsts.VISITADO);
            }

            nosAbertos.Remove(noAtual);
            nosFechados.Add(noAtual);

            foreach(Node no in grafo.NosVizinhos(noAtual.position))
            {
                Debug.Log("Descobrindo o nó " + no.position);
                Color status = no.status;

                if (status == colorConsts.NAO_DESCOBERTO)
                {
                    no.MudarStatus(colorConsts.DESCOBERTO);
                }
                else if (status == colorConsts.PLAYER)
                {
                    PlayerEncontrado(caminho,noAtual);
                    yield break;
                }

                if (nosFechados.Contains(no) || status == colorConsts.OBSTACULO)
                {
                    continue;
                }

                double novoCusto = custos[noAtual] + 1;

                if (!nosAbertos.Contains(no) || novoCusto < custos[no])
                {
                    custos[no] = novoCusto;
                    caminho[no] = noAtual;

                    if (!nosAbertos.Contains(no))
                    {
                        nosAbertos.Add(no);
                    }
                }
            }

            yield return null;
            yield return null;
        }

        yield break;
    }

    public int Compare(Node a, Node b)
    {
        double custoA = custos[a] + _grafo.Heuristica(a.position);
        double custoB = custos[b] + _grafo.Heuristica(b.position);

        return custoA.CompareTo(custoB);
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
