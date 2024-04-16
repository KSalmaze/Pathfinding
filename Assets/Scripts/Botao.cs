using UnityEngine;

public class Botao : MonoBehaviour
{
    public void Prescionado()
    {
        if (Input.GetKey(KeyCode.P))
        {
            
        }
        else if (Input.GetKey(KeyCode.E))
        {

        }
        else
        {

        }
    }

    public void ChangeColor(Color newColor)
    {
       gameObject.GetComponent<UnityEngine.UI.Image>().color = newColor;
    }
}
