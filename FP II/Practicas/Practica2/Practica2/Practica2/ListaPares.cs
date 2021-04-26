//Amparo Rubio Bellon

using System;
using System.Collections.Generic;
using System.Text;

namespace Practica2
{
    class ListaPares
    {
        class Nodo
        {
            public Coor coord;
            public Nodo sig;

            public Nodo(Coor c)
            {
                coord = c;
                sig = null;
            }
            public Nodo(Coor c, Nodo n)
            {
                coord = c;
                sig = n;
            }

        }

        Nodo pri, ult;
        int numElems;
        public ListaPares()
        {
            pri = ult = null;
            numElems = 0;
        }
        public ListaPares(Coor pr)
        {
            pri = ult = null;
            numElems = 0;
            insertaFin(pr);

        }
        public int getElems() { return numElems; }

        public void insertaFin(Coor c)
        {
            if (pri == null)
            {
                pri = new Nodo(c);
                pri.sig = null;
                ult = pri;
            }
            else
            {
                ult.sig = new Nodo(c);
                ult = ult.sig;
                ult.sig = null;
            }
            numElems++;
        }

        public Coor getnEsimo(int n)
        {
            if (n < 0 || n >= numElems)
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
                return aux.coord;
            }
        }

        public bool borraElto(Coor c)
        {
            // lista vacia
            if (pri == null) return false;
            else
            {
                bool result = false;
                // eliminar el primero
                if (c == pri.coord)
                {
                    result = true;
                    numElems--;
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
                    // busqueda 
                    Nodo aux = pri;
                    // recorremos lista buscando el ANTERIOR al que hay que eliminar (para poder luego enlazar)
                    while (aux.sig != null && c != aux.sig.coord)
                        aux = aux.sig;
                    // si lo encontramos
                    if (aux.sig != null)
                    {
                        result = true;
                        numElems--;
                        // si es el ultimo cambiamos referencia al ultimo
                        if (aux.sig == ult)
                            ult = aux;
                        // puenteamos
                        aux.sig = aux.sig.sig;
                    }
                }
                return result;

            }
        }
    }
}