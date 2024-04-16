using UnityEngine;

public class Botao : MonoBehaviour
{
    private ColorConsts colorConsts;

    private void Awake()
    {
        colorConsts = GameObject.Find("GameManager").GetComponent<ColorConsts>();
    }

    public void Prescionado()
    {
        if (Input.GetKey(KeyCode.P))
        {
            ChangeColor(colorConsts.PLAYER);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            ChangeColor(colorConsts.INIMIGO);
        }
        else
        {
            ChangeColor(colorConsts.OBSTACULO);
        }
    }

    public void ChangeColor(Color newColor)
    {
       gameObject.GetComponent<UnityEngine.UI.Image>().color = newColor;
    }
}
