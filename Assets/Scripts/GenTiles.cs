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
    [Tooltip("Ponto inicial para a geração de tiles")]
    [SerializeField] private Transform pontoIncial;

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
