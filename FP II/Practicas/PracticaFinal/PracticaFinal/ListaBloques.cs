// Rubio Bellon Amparo
using System;

namespace PracticaFinal
{
    class ListaBloques
    {
        class NodoConBloque
        {
            public Bloque bloque;
            public NodoConBloque sig;

            public NodoConBloque(Bloque b)
            {
                bloque = b;
                sig = null;
            }
            public NodoConBloque(Bloque b, NodoConBloque n)
            {
                bloque = b;
                sig = n;
            }

        }
        NodoConBloque pri, ult;
        int nElems;
        public int NumElems
        {
            get
            {
                return nElems;
            }
        }

        public ListaBloques()
        {
            pri = ult = null;
            nElems = 0;
        }
        public ListaBloques(Bloque b)
        {
            pri = ult = null;
            nElems = 0;
            InsertaFin(b);
        }

        public void InsertaFin(Bloque b)
        {
            if (pri == null)
            {
                pri = new NodoConBloque(b);
                pri.sig = null;
                ult = pri;
            }
            else
            {
                ult.sig = new NodoConBloque(b);
                ult = ult.sig;
                ult.sig = null;
            }
            nElems++;
        }
        public Bloque getnEsimo(int n)
        {
            if (n < 0 || n >= nElems)
            {
                return null;
            }
            else
            {
                NodoConBloque aux = pri;
                while (n > 0)
                {
                    aux = aux.sig;
                    n--;
                }
                return aux.bloque;
            }
        }

        public bool borraElto(Bloque b)
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
                if (b == pri.bloque)
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
                    NodoConBloque aux = pri;
                    while (aux.sig != null && b != aux.sig.bloque)
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
