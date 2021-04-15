using System;
//Amparo Rubio Bellon
using System.Collections.Generic;
using System.Text;

namespace Practica2
{
    class ListaPares
    {
        
        public ListaPares()
        {

        }
        
    }

    class Nodo
    {
        Coord c_;
        Nodo sig;
        public Nodo(Coord c)
        {
            c_ = c;
        }
        public Coord GetCoord()
        {
            return c_;
        }
       
    }
}
