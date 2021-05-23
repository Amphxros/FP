using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaFinal
{
    class Bloque
    {
        private Vector2D pos_;
        private int vida_;
        ConsoleColor color_;
        private int width_;

        public Vector2D Position => pos_;
        public int Width => width_;
        
        public Bloque(Vector2D pos, int vida, ConsoleColor color, int width)
        {
            pos_ = pos;
            vida_ = vida;
            color_ = color;
            width_ = width;
        }
        

        public void Render()
        {
            for(int i = 0; i < width_; i++)
            {
            Console.SetCursorPosition(pos_.getX() + i, pos_.getY());
            Console.BackgroundColor = color_;
            Console.Write("#.");

            }
        }
    }
}
