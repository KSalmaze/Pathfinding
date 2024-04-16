using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class GenTiles : MonoBehaviour
{
    [SerializeField] private TMP_InputField qntTiles_X;
    [SerializeField] private TMP_InputField qntTiles_Y;
    private int quantidadeDeTiles_X;
    private int quantidadeDeTiles_Y;
    [SerializeField] private int maxTiles_X;
    [SerializeField] private int maxTiles_Y;
    [Tooltip("Prefab de Tile")]
    [SerializeField] private GameObject tilePrefab;
    [Tooltip("Distancia entre os tiles")]
    [Range(0f,2f)][SerializeField] private float distanciaEntreTiles;
    [Tooltip("Objeto pai de todos os tiles, também é o objeto que contém o componente de grade")]
    [SerializeField] private GameObject grade;


    public void ButaoGerarTiles()
    {
        Debug.Log("Apertou o botão");
        if (quantidadeDeTiles_Y == 0 && quantidadeDeTiles_X == 0 &&
            VerificarInputField(qntTiles_Y) && VerificarInputField(qntTiles_X))
        {
            quantidadeDeTiles_X = int.Parse(qntTiles_X.text);
            quantidadeDeTiles_Y = int.Parse(qntTiles_Y.text);
            GerarTiles();
        }
    }

    private bool VerificarInputField(TMP_InputField inputField ,int quantidadeMax = 100)
    {
        Debug.Log("Verifcando Input Field");
        string text = inputField.text;
        if (text != string.Empty && int.Parse(text) <= quantidadeMax)
        {
            Debug.Log("Correta");
            return true;
        }
        else
        {
            return false;
        }
    }

    private void GerarTiles()
    {
        CriarTiles();

        AdicionarEspacamentoAosTiles();
    }

    private void CriarTiles()
    {
        for (int i = 0; i < quantidadeDeTiles_X * quantidadeDeTiles_Y; i++)
        {
            // Cria um tile
            GameObject gameObject = Instantiate(tilePrefab);
            // Coloca esse tile na grade
            gameObject.transform.SetParent(grade.transform);
        }
    }

    private void AdicionarEspacamentoAosTiles()
    {
        GridLayoutGroup grid = grade.GetComponent<GridLayoutGroup>();

        grid.constraintCount = quantidadeDeTiles_X;

        grid.spacing = new Vector2(distanciaEntreTiles, distanciaEntreTiles);
    }

    public void LimparGrade()
    {
        // Itera por todos os filhos do objeto pai
        for (int i = grade.transform.childCount - 1; i >= 0; i--)
        {
            // Obtém o filho atual
            Transform filho = grade.transform.GetChild(i);

            // Destroi o filho
            Destroy(filho.gameObject);
            // Ou use DestroyImmediate(filho.gameObject) se estiver fora do modo de reprodução

            // Opcionalmente, você pode chamar recursivamente LimparTodosFilhos se deseja limpar os filhos dos filhos
            // LimparTodosFilhos(filho);
        }

        quantidadeDeTiles_X = 0;
        quantidadeDeTiles_Y = 0;
    }
}
