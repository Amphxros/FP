using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaFinal
{
    class Paddle
    {
        private Vector2D pos_;
        private int vida_;
        private int initialWidth_;
        //get and sets the position
        public Vector2D Position
        {
            get
            {
                return pos_;
            }
            set
            {
                pos_ = value;
            }
        }

        //get and sets the direction
        public Vector2D Direction { get; set; }

        public int Width { get; set; }
        public Paddle(Vector2D pos, int vida, int width)
        {
            pos_ = pos;
            vida_ = vida;
            Direction = new Vector2D();
            Width = width;
            initialWidth_ = width;



        }

        public void Render()
        {
            for (int i = 0; i < Width; i++)
            {
                Console.SetCursorPosition(2* pos_.getX() + i, pos_.getY());
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write("  ");
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public void Update()
        {
            pos_.setX(pos_.getX() + Direction.getX());
        }

        public void handleInput(char c)
        {
            switch (c)
            {
                case 'l':
                    Direction.setX(-1);
                    break;
                case 'r':
                    Direction.setX(1);
                    break;
                default:
                    Direction.setX(0);
                    break;
            }
        }

        public bool isDead()
        {
            return vida_ <= 0;
        }

        public void resetWidth()
        {
            Width = initialWidth_;
        }

    }



}
