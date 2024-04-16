using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuscaEmLargura : AlgoritmoDeBusca
{
    override public IEnumerator Comecar(Grafo grafo)
    {
        // Aqui fica o algoritmo :)
        // A cada operação relevante usar o seguinte retorno
        // yield return null;
        // Isso vai fazer as alterações aparecerem na tela
        return null;
    }
}
/*  Documentação de Nó (Node)
 *  
 *  (int x, int y) Posicao;
 *  
 *  AlterarEstatoPara(ESTADO);
 *  Os posiveis estados são na váriavel Estado
 *  sendo eles:
 *  Estado.Descoberto
 *  Estado.Visitado
 *  Tem mais alguns mas acho q não vão precisar usar
 *  
 *  Documentação de Grafo
 * 
 * -List<Node> NosVizinhos((int, int) posicao);
 *  Recebe a posição de um nó e retorna seus vizinhos
 * 
 * - float Heuristica((int, int) posicao);
 *  Recebe a posiçaõ de um nó e calcula a distancia esperada
 * 
 */
