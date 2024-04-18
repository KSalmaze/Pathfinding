using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class Grafo : MonoBehaviour
{
    [SerializeField] private TMP_Text caixaDeTexto;
    public ColorConsts colorConsts;
    public Node[][] nos;
    public (int x, int y) PosicaoPlayer = (-1,-1);
    public (int x, int y) PosicaoInimigo = (-1,-1);
    private (int x, int y) comprimeto;
    Stopwatch cronometro;
    
    public bool algumaCoisaEstaAcontecendo = false;
    
    public void InicializarGrafo(int tamanho_X, int tamanho_Y)
    {
        cronometro = new Stopwatch();
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

        //Debug.Log("DIstancia de " + posicaoDoNo + " até o Player" + DistanceBetween(posicaoDoNo, PosicaoPlayer));
        return System.Math.Abs(DistanceBetween(posicaoDoNo, PosicaoPlayer));
    }

    private double DistanceBetween((int x, int y) a, (int x, int y) b)
    {
        double deltaX = b.x - a.x;
        double deltaY = b.y - a.y;
        return System.Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
    }

    public void BuscaEmLargura()
    {
        cronometro.Start();
        BuscaEmLargura buscaEmLargura = new BuscaEmLargura();

        if (VerificarSePodeComecarABusca())
        {
            algumaCoisaEstaAcontecendo = true;
            StartCoroutine(buscaEmLargura.Comecar(this, colorConsts));
        }
    }

    public void AEstrela()
    {
        cronometro.Start();
        AStar aEstrela = new AStar();

        if (VerificarSePodeComecarABusca())
        {
            algumaCoisaEstaAcontecendo = true;
            StartCoroutine(aEstrela.Comecar(this, colorConsts));
        }
    }

    private bool VerificarSePodeComecarABusca()
    {
        if (PosicaoPlayer != (-1,-1) && PosicaoInimigo != (-1,-1) && !algumaCoisaEstaAcontecendo)
        {
            return true;
        }

        return false;
    }

    public Node PosicaoInicial()
    {
        return nos[PosicaoInimigo.y][PosicaoInimigo.x] ;
    }

    public void MostrarCaminho(List<Node> caminho)
    {
        cronometro.Stop();
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

        GerarEstatisticas();
        yield break;
    }

    public void Limpar()
    {
        if (algumaCoisaEstaAcontecendo)
        {
            return;
        }

        cronometro.Stop();
        caixaDeTexto.text = string.Empty;
        List<Color> coresParaLimpar = new List<Color>()
        {
            colorConsts.CAMINHO,
            colorConsts.DESCOBERTO,
            colorConsts.VISITADO
        };

        for (int i = 0; i < comprimeto.y; i++)
        {
            for (int j = 0; j < comprimeto.x; j++)
            {
                if (coresParaLimpar.Contains(nos[i][j].status))
                {
                    nos[i][j].MudarStatus(colorConsts.NAO_DESCOBERTO);
                }
            }
        }
    }

    public void GerarEstatisticas()
    {
        cronometro.Stop();
        int nosDescobertos = 0, nosExplorados = 0, custoTotal = 0;

        for (int i = 0; i < comprimeto.y; i++)
        {
            for (int j = 0; j < comprimeto.x; j++)
            {
                Color status = nos[i][j].status;

                if (status == colorConsts.NAO_DESCOBERTO || status == colorConsts.OBSTACULO)
                {
                    continue;
                }
                else if (status == colorConsts.DESCOBERTO)
                {
                    nosDescobertos++;
                }
                else if (status == colorConsts.VISITADO)
                {
                    nosExplorados++;
                    nosDescobertos++;
                }
                else if (status == colorConsts.CAMINHO)
                {
                    nosExplorados++;
                    nosDescobertos++;
                    custoTotal++;
                }
            }
        }

        caixaDeTexto.text = $"Custo Total: {custoTotal} Tempo Total: {cronometro.ElapsedMilliseconds}ms" +
            $" Nos descobertos: {nosDescobertos} Nos explorados: {nosExplorados}";
        algumaCoisaEstaAcontecendo = false;
        cronometro.Reset();
    }
}
