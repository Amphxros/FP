using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaFinal
{
    class ListaPremios
    {
        class NodoConPremio
        {
            public Reward r;
            public NodoConPremio sig;

            public NodoConPremio(Reward b)
            {
                r = b;
                sig = null;
            }
            public NodoConPremio(Reward b, NodoConPremio n)
            {
                r = b;
                sig = n;
            }

        }

        NodoConPremio pri, ult;
        int nElems;
        public ListaPremios()
        {
            pri = ult = null;
        }

        public ListaPremios(Reward b)
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

        public void InsertaFin(Reward b)
        {
            if (pri == null)
            {
                pri = new NodoConPremio(b);
                pri.sig = null;
                ult = pri;
            }
            else
            {
                ult.sig = new NodoConPremio(b);
                ult = ult.sig;
                ult.sig = null;
            }
            nElems++;
        }

        public Reward getnEsimo(int n)
        {
            if (n < 0 || n >= nElems)
            {
                return null;
            }
            else
            {
                NodoConPremio aux = pri;
                while (n > 0)
                {
                    aux = aux.sig;
                    n--;
                }
                return aux.r;
            }
        }


        public bool borraElto(Reward b)
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
                if (b == pri.r)
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
                    NodoConPremio aux = pri;
                    while (aux.sig != null && b != aux.sig.r)
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
