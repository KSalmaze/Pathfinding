using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Botao : MonoBehaviour
{
    public void Prescionado()
    {
        Debug.Log("O componente existe");
        if (Input.GetKey(KeyCode.P))
        {
            //  gameObject.GetComponent<Image>();
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
