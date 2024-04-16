using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField] private Grafo grafo;
    private ColorConsts colors;

    private void Start()
    {
        colors = gameObject.GetComponent<ColorConsts>();
    }

    public void BotaoGerarTiles()
    {
        if (quantidadeDeTiles_Y == 0 && quantidadeDeTiles_X == 0 &&
            VerificarInputField(qntTiles_Y) && VerificarInputField(qntTiles_X))
        {
            quantidadeDeTiles_X = int.Parse(qntTiles_X.text);
            quantidadeDeTiles_Y = int.Parse(qntTiles_Y.text);
            GerarTiles();
        }
    }

    private bool VerificarInputField(TMP_InputField inputField ,int quantidadeMax = 20)
    {
        string text = inputField.text;
        if (text != string.Empty && int.Parse(text) <= quantidadeMax && int.Parse(text) > 0)
        {
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
        for (int i = 0; i < quantidadeDeTiles_X; i++)
        {
            for (int j = 0; j < quantidadeDeTiles_Y; j++)
            {
                // Cria um tile
                GameObject novoTile = Instantiate(tilePrefab);
                // Coloca esse tile na grade
                novoTile.transform.SetParent(grade.transform);
                // Coloca a cor certa
                novoTile.GetComponent<Botao>().ChangeColor(colors.NAO_DESCOBERTO);
                // Nomeia baseado na sua posição
                novoTile.name = "Tile X:" + i + " Y:" + j;
                // Inicializa o nó com a sua posiçaõ correta
                novoTile.GetComponent<Node>().position = (i,j);
            }
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
