using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuscaEmLargura : AlgoritmoDeBusca
{
    override public IEnumerator Comecar(Grafo grafo)
    {
        // Aqui fica o algoritmo :)
        // A cada opera��o relevante usar o seguinte retorno
        // yield return null;
        // Isso vai fazer as altera��es aparecerem na tela
        return null;
    }
}
/*  Documenta��o de N� (Node)
 *  
 *  (int x, int y) Posicao;
 *  
 *  AlterarEstatoPara(ESTADO);
 *  Os posiveis estados s�o na v�riavel Estado
 *  sendo eles:
 *  Estado.Descoberto
 *  Estado.Visitado
 *  Tem mais alguns mas acho q n�o v�o precisar usar
 *  
 *  Documenta��o de Grafo
 * 
 * -List<Node> NosVizinhos((int, int) posicao);
 *  Recebe a posi��o de um n� e retorna seus vizinhos
 * 
 * - float Heuristica((int, int) posicao);
 *  Recebe a posi�a� de um n� e calcula a distancia esperada
 * 
 */
