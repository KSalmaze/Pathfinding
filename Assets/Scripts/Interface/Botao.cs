using UnityEngine;

public class Botao : MonoBehaviour
{
    public Node no;
    private ColorConsts colorConsts;
    private Grafo grafo;
    private UnityEngine.UI.Image image;

    private void Awake()
    {
        GameObject gameManager = GameObject.Find("GameManager");
        colorConsts = gameManager.GetComponent<ColorConsts>();
        grafo = gameManager.GetComponent<Grafo>();
        image = gameObject.GetComponent<UnityEngine.UI.Image>();
    }

    public void Prescionado()
    {
        if (Input.GetKey(KeyCode.P))
        {
            if (grafo.PosicaoPlayer != (-1, -1))
                return;
            ChangeColor(colorConsts.PLAYER);
            grafo.PosicaoPlayer = no.position;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            if (grafo.PosicaoInimigo != (-1,-1)) 
                return;
            
            ChangeColor(colorConsts.INIMIGO);
            grafo.PosicaoInimigo = no.position;
        }
        else if(image.color == colorConsts.OBSTACULO)
        {
            ChangeColor(colorConsts.NAO_DESCOBERTO);
        }
        else
        {
            if (image.color == colorConsts.PLAYER)
            {
                grafo.PosicaoPlayer = (-1,-1);
                ChangeColor(colorConsts.NAO_DESCOBERTO);
            }
            else if (image.color == colorConsts.INIMIGO)
            {
                grafo.PosicaoInimigo = (-1, -1);
                ChangeColor(colorConsts.NAO_DESCOBERTO);
            }
            else
            {
                ChangeColor(colorConsts.OBSTACULO);
            }
        }
    }

    public void ChangeColor(Color newColor)
    {
        no.MudarStatus(newColor);
    }
}
