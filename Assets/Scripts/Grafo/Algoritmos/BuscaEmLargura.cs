using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuscaEmLargura : AlgoritmoDeBusca
{
    private Grafo _grafo;

    // Corotina para a execu��o do algoritmo de Busca em Largura
    override public IEnumerator Comecar(Grafo grafo, ColorConsts cc)
    {
        _grafo = grafo; // Armazena a refer�ncia do grafo.
        colorConsts = cc; // Configura as constantes de cores.

        // Dicion�rio para amazenar o caminho para cada n�
        Dictionary<Node, Node> nosAnteriores = new Dictionary<Node, Node>();
        // Fila necess�ria para a execu��o do m�todo
        Queue<Node> fila = new Queue<Node>();

        // Inicializa a fila com o primeiro n�
        fila.Enqueue(grafo.PosicaoInicial());

        // Enquanto houver algum n� na fila, continua a busca
        while (fila.Count > 0)
        {
            // Define o n� atual como o primeiro da fila
            Node noAtual = fila.Dequeue();


            // Se o n� atual j� tiver sido descoberto, ele � marcado como visitado
            if (noAtual.status == colorConsts.DESCOBERTO)
            {
                noAtual.MudarStatus(colorConsts.VISITADO);
            }

            // Para cada n� vizinho ao n� atual
            foreach (Node no in grafo.NosVizinhos(noAtual.position))
            {
                Debug.Log("Descobrindo o n� " + no.position);

                // Verifica o status do vizinho.
                Color status = no.status;

                // Se o vizinho for o jogador
                if (status == colorConsts.PLAYER)
                {
                    Debug.Log("Encontrou o jogador");
                    // Marca o n� atual como o n� anterior ao jogador
                    nosAnteriores.Add(no, noAtual);

                    // Chama a fun��o de Player encontrado
                    PlayerEncontrado(nosAnteriores, noAtual);

                    // Finaliza a busca
                    yield break;
                }

                // Se o n� ainda n�o tiver sido descoberto, marca como descoberto
                if (status == colorConsts.NAO_DESCOBERTO)
                {
                    nosAnteriores.Add(no, noAtual);
                    no.MudarStatus(grafo.colorConsts.DESCOBERTO);

                    // Adiciona na fila
                    fila.Enqueue(no);
                }

                // Passa para o pr�ximo frame para atualizar a tela
                yield return null;
            }

            // Passa para o pr�ximo frame para atualizar a tela
            yield return null;
        }

        grafo.GerarEstatisticas();
    }


    // M�todo privado para tratar o encontro com o jogador.
    private void PlayerEncontrado(Dictionary<Node, Node> nosAnteriores, Node ultimoNo)
    {
        // Caso Player esteja do lado do Inimigo
        if (ultimoNo == _grafo.PosicaoInicial())
        {
            Debug.Log("Problema detectado");
            _grafo.MostrarCaminho(null);
            return;
        }

        // Lista para armazenar o caminho encontrado.
        List<Node> caminho = new List<Node>();
        // Come�a pelo �ltimo n� (onde o jogador foi encontrado).
        Node noAtual = ultimoNo;

        // Retrocede pelo caminho usando o dicion�rio at� chegar ao in�cio.
        do
        {
            caminho.Add(noAtual);
            noAtual = nosAnteriores[noAtual];
        } while (noAtual.position != _grafo.PosicaoInimigo);
        // O loop termina quando chega � posi��o do inimigo.

        // Ap�s construir o caminho, mostra-o no jogo.
        _grafo.MostrarCaminho(caminho);
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
