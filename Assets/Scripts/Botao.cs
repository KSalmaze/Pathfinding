using UnityEngine;

public class Botao : MonoBehaviour
{
    public void Prescionado()
    {
        Debug.Log("O componente existe");
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
        Debug.Log("Trocando a Cor");
        gameObject.GetComponent<UnityEngine.UI.Image>().color = newColor;
    }
}
