using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaFinal
{
    class Lista
    {
        class Nodo
        {
            public Ball ball;
            public Nodo sig;

            public Nodo(Ball b)
            {
                ball = b;
                sig = null;
            }
            public Nodo(Ball b, Nodo n)
            {
                ball = b;
                sig = n;
            }

        }
        Nodo pri, ult;
        int nElems;
        public Lista()
        {
            pri = ult = null;
            nElems = 0;
        }
        public Lista(Ball b)
        {
            pri = ult = null;
            nElems = 0;
            InsertaFin(b);
        }
        public int NumElems
        {
            get
            {
                return nElems;
            }
        }

        public void InsertaFin(Ball b)
        {
            if (pri == null)
            {
                pri = new Nodo(b);
                pri.sig = null;
                ult = pri;
            }
            else
            {
                ult.sig = new Nodo(b);
                ult = ult.sig;
                ult.sig = null;
            }
            nElems++;
        }

        public Ball getnEsimo(int n)
        {
            if (n < 0 || n >= nElems)
            {
                return null;
            }
            else
            {
                Nodo aux = pri;
                while (n > 0)
                {
                    aux = aux.sig;
                    n--;
                }
                return aux.ball;
            }
        }


        public bool borraElto(Ball b)
        {
            // lista vacia
            if (pri == null)
            {
                return false;
            }
            else
            {
                bool rst = false;
                // eliminar el primero
                if (b == pri.ball)
                {
                    rst = true;
                    nElems--;
                    // si solo tienen un elto
                    if (pri == ult)
                        pri = ult = null;
                    // si tiene más de uno
                    else
                        pri = pri.sig;
                }
                // eliminar otro distino al primero
                else
                {
                    // recorremos lista buscando el ANTERIOR al que hay que eliminar (para poder luego enlazar)
                    Nodo aux = pri;
                    while (aux.sig != null && b != aux.sig.ball)
                        aux = aux.sig;
                    // si lo encontramos
                    if (aux.sig != null)
                    {
                        rst = true;
                        nElems--;
                        // si es el ultimo cambiamos referencia al ultimo
                        if (aux.sig == ult)
                            ult = aux;
                        // puenteamos
                        aux.sig = aux.sig.sig;
                    }
                }
                return rst;

            }

        }
    }
}