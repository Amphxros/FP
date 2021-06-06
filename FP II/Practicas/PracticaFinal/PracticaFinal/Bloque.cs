//Rubio Bellon Amparo

using System;

namespace PracticaFinal
{
    class Bloque
    {
        private int vida_; //num de golpes que les tienes que dar para destruir el bloque
        ConsoleColor color_; //color del bloque

        private Vector2D pos_;
        public Vector2D Position => pos_; //posicion
        
        private int width_;
        public int Width => width_; //tamaño del bloque
        
        //constructora
        public Bloque(Vector2D pos, int vida, ConsoleColor color, int width)
        {
            pos_ = pos;
            vida_ = vida;
            color_ = color;
            width_ = width;
        }
        
        //renderizado
        public void Render()
        {
            for(int i = 0; i < width_; i++)
            {
            Console.SetCursorPosition(pos_.getX() + i, pos_.getY());
            Console.BackgroundColor = color_;
            Console.Write("#");

            }
        }

        //en caso de colision con la pelota se llama a este metodo 
        //devolverá true si este se tiene que destruir 
        public bool OnCollision()
        {
            vida_--;
            return vida_ <= 0;
        }
    }
}
