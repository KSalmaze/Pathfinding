using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color status; // Cor que define o status do n�
    public (int x, int y) position; // A posi��o do n� na malha
    private Grafo grafo; // Refer�ncia ao grafo
    private UnityEngine.UI.Image image; // A sprite do n�

    // Fun��o da Unity que � chamada assim que o n� � criado
    public void Awake()
    {
        // Inicializa as v�riaveis de grafo e imagem
        grafo = GameObject.Find("GameManager").GetComponent<Grafo>();
        image = gameObject.GetComponent<UnityEngine.UI.Image>();
    }

    // Fun��o chama na cria��o do n�, recebe qual deve ser a sua posi��o no grafo
    // e o status inicial
    public void InicializarNo((int x,int y) pos, Color statusInicial)
    {
        position = pos;
        status = statusInicial;
        SeColocarNoGrafo();
    }

    // Assim que o n� � criado ele chama essa fun��o pra ser colocado no grafo
    public void SeColocarNoGrafo()
    {
        grafo.nos[position.y][position.x] = this;
    }

    // Fun��o para facilitar a mundan�a de status de um n�
    public void MudarStatus(Color cor)
    {
        image.color = cor;
        status = cor;
    }
}
