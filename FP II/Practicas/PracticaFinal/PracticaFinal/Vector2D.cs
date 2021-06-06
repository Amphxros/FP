// Rubio Bellon Amparo
using System;

namespace PracticaFinal
{
    //clase necesaria para definir posiciones y velocidades, equivalente a Coor
    class Vector2D 
    {
        int x_, y_; //coordenada x e y
        public Vector2D()
        {
            x_ = y_ = 0;
        }
        public Vector2D(int x, int y)
        {
            x_ = x;
            y_ = y;
        }
        public int getX() { return x_; }
        public void setX(int x) { x_ = x; }
        public void setY(int y) { y_ = y; }
        public int getY() { return y_; }
    }
}
