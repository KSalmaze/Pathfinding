using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grafo : MonoBehaviour
{
    public Node[][] nos;
    public (int x, int y) PosicaoPlayer = (-1,-1);
    public (int x, int y) PosicaoInimigo = (-1,-1);
    
    public void InicializarGrafo(int tamanho_X, int tamanho_Y)
    {
        nos = new Node[tamanho_Y][];

        for (int i = 0; i < tamanho_Y; i++)
        {
            nos[i] = new Node[tamanho_X];
        }
    }

    public List<Node> NosVizinhos((int x, int y)posicaoDoNo)
    {
        return null;
    }

    public float Heuristica((int x, int y) posicaoDoNo)
    {
        return 0;
    }
}
