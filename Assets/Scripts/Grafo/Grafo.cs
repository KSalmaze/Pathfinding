using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grafo : MonoBehaviour
{
    public ColorConsts colorConsts;
    public Node[][] nos;
    public (int x, int y) PosicaoPlayer = (-1,-1);
    public (int x, int y) PosicaoInimigo = (-1,-1);
    private (int x, int y) comprimeto;
    
    public void InicializarGrafo(int tamanho_X, int tamanho_Y)
    {
        PosicaoPlayer = (-1, -1);
        PosicaoInimigo = (-1, -1);

        comprimeto = (tamanho_X,tamanho_Y);

        nos = new Node[tamanho_Y][];

        for (int i = 0; i < tamanho_Y; i++)
        {
            nos[i] = new Node[tamanho_X];
        }
    }

    public List<Node> NosVizinhos((int x, int y)posicaoDoNo)
    {
        //Debug.Log("Veriicand Vizinhos para: " + posicaoDoNo);
        List<Node> lista = new List<Node>();

        if (posicaoDoNo.x - 1 >= 0)
        {
            lista.Add(nos[posicaoDoNo.y][posicaoDoNo.x - 1]);
            //Debug.Log("Vizinho encontrado em " + nos[posicaoDoNo.y][posicaoDoNo.x - 1].position);
        }
        if (posicaoDoNo.x + 1 < comprimeto.x)
        {
            lista.Add(nos[posicaoDoNo.y][posicaoDoNo.x + 1]);
            //Debug.Log("Vizinho encontrado em " + nos[posicaoDoNo.y][posicaoDoNo.x + 1].position);
        }
        if (posicaoDoNo.y - 1 >= 0)
        {
            lista.Add(nos[posicaoDoNo.y - 1][posicaoDoNo.x]);
            //Debug.Log("Vizinho encontrado em " + nos[posicaoDoNo.y - 1][posicaoDoNo.x].position);
        }
        if (posicaoDoNo.y + 1 < comprimeto.y)
        {
            lista.Add(nos[posicaoDoNo.y + 1][posicaoDoNo.x]);
            //Debug.Log("Vizinho encontrado em " + nos[posicaoDoNo.y + 1][posicaoDoNo.x].position);
        }

        return lista;
    }

    public double Heuristica((int x, int y) posicaoDoNo)
    {
        return DistanceBetween(posicaoDoNo, PosicaoPlayer);
    }

    private double DistanceBetween((int x, int y) a, (int x, int y) b)
    {
        double deltaX = b.x - a.x;
        double deltaY = b.y - a.y;
        return System.Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
    }

    public void BuscaEmLargura()
    {
        BuscaEmLargura buscaEmLargura = new BuscaEmLargura();

        if (VerificarSePodeComecarABusca())
        {
            StartCoroutine(buscaEmLargura.Comecar(this, colorConsts));
        }
    }

    public void AEstrela()
    {
        AStar aEstrela = new AStar();

        if (VerificarSePodeComecarABusca())
        {
            StartCoroutine(aEstrela.Comecar(this, colorConsts));
        }
    }

    private bool VerificarSePodeComecarABusca()
    {
        if (PosicaoPlayer != (-1,-1) && PosicaoInimigo != (-1,-1))
        {
            return true;
        }

        return false;
    }

    public Node PosicaoInicial()
    {
        Debug.Log(PosicaoInimigo);
        return nos[PosicaoInimigo.y][PosicaoInimigo.x] ;
    }

    public void MostrarCaminho(List<Node> caminho)
    {
        StartCoroutine(MostrarCaminhoCoroutine(caminho));
    }

    private IEnumerator MostrarCaminhoCoroutine(List<Node> caminho)
    {
        caminho.Reverse();

        foreach (Node no in caminho)
        {
            no.MudarStatus(colorConsts.CAMINHO);           
            yield return null;
        }

        yield break;
    }

    private void GerarEstatisticas()
    {

    }
}
