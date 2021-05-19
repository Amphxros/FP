﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaFinal
{
    class Bloque
    {
        Vector2D pos_;
        int vida_;
        ConsoleColor color_;

        public Vector2D Position
        {
            get
            {
                return pos_;
            }
        }


        public Bloque(Vector2D pos, int vida, ConsoleColor color)
        {
            pos_ = pos;
            vida_ = vida;
            color_ = color;
        }
        

        public void Render()
        {
            Console.SetCursorPosition(4 * pos_.getX(), pos_.getY());
            Console.BackgroundColor = color_;
            Console.Write("    ");
        }
    }
}
