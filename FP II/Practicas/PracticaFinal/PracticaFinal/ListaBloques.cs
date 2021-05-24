using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaFinal
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
    class ListaBloques
    {
        public ListaBloques()
        {

        }
    }
}
