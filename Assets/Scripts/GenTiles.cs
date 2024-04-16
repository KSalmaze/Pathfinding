using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenTiles : MonoBehaviour
{
    [Tooltip("Quantidade de tiles em X e em Y")]
    [SerializeField] private float quantidadeDeTiles_X, quantidadeDeTiles_Y;
    [Tooltip("Prefab de Tile")]
    [SerializeField] private GameObject tilePrefab;
    [Tooltip("Distancia entre os tiles")]
    [Range(0f,2f)][SerializeField] private float distanciaEntreTiles;
    [Tooltip("Objeto pai, todos os tiles serão filhos dele")]
    [SerializeField] private GameObject objetoPai;

    void Start()
    {
        for(int i = 0;i < quantidadeDeTiles_X; i++)
        {
            GameObject gameObject = Instantiate(tilePrefab);
            gameObject.transform.parent = objetoPai.transform;
        }
    }


    void Update()
    {
        
    }
}
