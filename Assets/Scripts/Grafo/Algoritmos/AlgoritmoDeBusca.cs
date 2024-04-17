using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AlgoritmoDeBusca
{
    public ColorConsts colorConsts;

    abstract public IEnumerator Comecar(Grafo grafo, ColorConsts cc);

}
