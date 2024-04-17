using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color status;
    public (int x, int y) position;
    private Grafo grafo;
    private UnityEngine.UI.Image image;

    public void Awake()
    {
        grafo = GameObject.Find("GameManager").GetComponent<Grafo>();
        image = gameObject.GetComponent<UnityEngine.UI.Image>();
    }

    public void InicializarNo((int x,int y) pos, Color statusInicial)
    {
        position = pos;
        status = statusInicial;
        SeColocarNoGrafo();
    }

    public void SeColocarNoGrafo()
    {
        grafo.nos[position.y][position.x] = this;
    }

    public void MudarStatus(Color cor)
    {
        image.color = cor;
        status = cor;
    }
}
