using System.Collections;
using System.Collections.Generic;
using System.Diagnostics; // Utilizado para cronometrar eventos, como a dura��o da busca.
using TMPro; // Text Mesh Pro para exibi��o de texto na interface do usu�rio.
using Unity.VisualScripting;
using UnityEngine;

// Classe principal que representa o grafo sobre o qual os algoritmos de busca ir�o operar.
public class Grafo : MonoBehaviour
{
    [SerializeField] private TMP_Text caixaDeTexto;// Refer�ncia ao texto na UI onde as estat�sticas ser�o exibidas.
    public ColorConsts colorConsts;// Inst�ncia que armazena as cores utilizadas para diferenciar estados dos n�s.
    public Node[][] nos;// Matriz bidimensional de n�s que comp�em o grafo.
    public (int x, int y) PosicaoPlayer = (-1,-1); // Posi��o inicial do jogador no grafo.
    public (int x, int y) PosicaoInimigo = (-1,-1); // Posi��o inicial do inimigo no grafo.
    private (int x, int y) comprimeto; // Tamanho do grafo (largura e altura).
    Stopwatch cronometro; // Cron�metro para medir o tempo de execu��o dos algoritmos.

    public bool algumaCoisaEstaAcontecendo = false; // Flag para verificar se uma busca j� est� em progresso.

    // M�todo para inicializar o grafo com um determinado tamanho.
    public void InicializarGrafo(int tamanho_X, int tamanho_Y)
    {
        cronometro = new Stopwatch(); // Inicializa o cron�metro.
        PosicaoPlayer = (-1, -1); // Reseta a posi��o do jogador.
        PosicaoInimigo = (-1, -1); // Reseta a posi��o do inimigo.

        comprimeto = (tamanho_X,tamanho_Y); // Define o tamanho do grafo.

        // Cria a matriz de n�s com base no tamanho fornecido.
        nos = new Node[tamanho_Y][];
        for (int i = 0; i < tamanho_Y; i++)
        {
            nos[i] = new Node[tamanho_X];
        }
    }

    // M�todo para obter a lista de n�s vizinhos de um n� espec�fico.
    public List<Node> NosVizinhos((int x, int y)posicaoDoNo)
    {
        // Lista para armazenar os vizinhos.
        List<Node> lista = new List<Node>();

        // Verifica e adiciona vizinhos v�lidos � lista (cima, baixo, esquerda, direita).
        if (posicaoDoNo.x - 1 >= 0)
        {
            lista.Add(nos[posicaoDoNo.y][posicaoDoNo.x - 1]);
        }
        if (posicaoDoNo.x + 1 < comprimeto.x)
        {
            lista.Add(nos[posicaoDoNo.y][posicaoDoNo.x + 1]);
        }
        if (posicaoDoNo.y - 1 >= 0)
        {
            lista.Add(nos[posicaoDoNo.y - 1][posicaoDoNo.x]);
        }
        if (posicaoDoNo.y + 1 < comprimeto.y)
        {
            lista.Add(nos[posicaoDoNo.y + 1][posicaoDoNo.x]);
        }

        return lista; // Retorna a lista de vizinhos.
    }

    // M�todo para calcular a heur�stica (dist�ncia Manhattan) entre dois n�s.
    public double Heuristica((int x, int y) posicaoDoNo)
    {
        return Manhattan(posicaoDoNo);
    }

    private int Manhattan((int x, int y) posicaoDoNo)
    {
        int x = Mathf.Abs(posicaoDoNo.x - PosicaoPlayer.x);
        int y = Mathf.Abs(posicaoDoNo.y - PosicaoPlayer.y);

        return x + y;
    }

    // M�todo auxiliar para calcular a dist�ncia Euclidiana entre dois pontos.
    private double DistanceBetween((int x, int y) a, (int x, int y) b)
    {
        double deltaX = b.x - a.x;
        double deltaY = b.y - a.y;
        // Retorna a raiz quadrada da soma dos quadrados das diferen�as.
        return System.Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
    }

    // M�todos para iniciar as buscas em largura e A*.
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

    // M�todo para verificar se � poss�vel come�ar uma busca.
    private bool VerificarSePodeComecarABusca()
    {
        if (PosicaoPlayer != (-1,-1) && PosicaoInimigo != (-1,-1) && !algumaCoisaEstaAcontecendo)
        {
            return true;
        }

        return false;
    }

    // M�todo para obter a posi��o inicial do grafo (normalmente a posi��o do inimigo).
    public Node PosicaoInicial()
    {
        return nos[PosicaoInimigo.y][PosicaoInimigo.x] ;
    }

    // M�todo para mostrar o caminho encontrado no grafo.
    public void MostrarCaminho(List<Node> caminho)
    {
        cronometro.Stop();

        if (caminho != null)
        {
            StartCoroutine(MostrarCaminhoCoroutine(caminho));
        }
        else
        {
            GerarEstatisticas();
        }
    }

    // Coroutine para mostrar o caminho.
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

    // Limpa o grafo, mantendo os obst�culos, o player e o inimigo.
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

    // Calcula a quantidade de n�s descobertos, explorados e custo total.
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
