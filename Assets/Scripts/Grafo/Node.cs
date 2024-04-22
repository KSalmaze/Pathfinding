using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color status; // Cor que define o status do nó
    public (int x, int y) position; // A posição do nó na malha
    private Grafo grafo; // Referência ao grafo
    private UnityEngine.UI.Image image; // A sprite do nó

    // Função da Unity que é chamada assim que o nó é criado
    public void Awake()
    {
        // Inicializa as váriaveis de grafo e imagem
        grafo = GameObject.Find("GameManager").GetComponent<Grafo>();
        image = gameObject.GetComponent<UnityEngine.UI.Image>();
    }

    // Função chama na criação do nó, recebe qual deve ser a sua posição no grafo
    // e o status inicial
    public void InicializarNo((int x,int y) pos, Color statusInicial)
    {
        position = pos;
        status = statusInicial;
        SeColocarNoGrafo();
    }

    // Assim que o nó é criado ele chama essa função pra ser colocado no grafo
    public void SeColocarNoGrafo()
    {
        grafo.nos[position.y][position.x] = this;
    }

    // Função para facilitar a mundança de status de um nó
    public void MudarStatus(Color cor)
    {
        image.color = cor;
        status = cor;
    }
}
