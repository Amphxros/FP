//Amparo Rubio Bellon
using System;
using System.Collections.Generic;
using System.Text;

namespace Practica2
{
    class Coord
    {
        // fila y columna (como propiedades)
        public int fil { get; set; }
        public int col { get; set; }
        public Coord(int _fil = 0, int _col = 0) { fil = _fil; col = _col; }

        // sobrecarga de + y - para hacer "desplazamientos" con coordenadas
        public static Coord operator +(Coord c1, Coord c2)
        {
            return new Coord(c1.fil + c2.fil, c1.col + c2.col);
        }

        public static Coord operator -(Coord c)
        {
            return new Coord(-c.fil, -c.col);
        }

        // sobrecarga de los operadores == y != para comparar coordenadas mediante fil y col
        public static bool operator ==(Coord c1, Coord c2)
        {
            return c1.fil == c2.fil && c1.col == c2.col;
        }
        public static bool operator !=(Coord c1, Coord c2)
        {
            //public bool Equals(Coor c){
            return !(c1 == c2);
        }

        public override bool Equals(object c)
        {
            return (c is Coord) && this == (Coord)c;
        }

    }
}
