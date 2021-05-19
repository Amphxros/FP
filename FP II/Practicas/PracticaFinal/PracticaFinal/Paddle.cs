using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaFinal
{
    enum DIRS { LEFT,RIGHT};
    class Paddle
    {
        const int WIDTH = 10;
        private Vector2D pos_, dir_;
        private int vida_;
        int width_;

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
        public Vector2D Direction
        {
            get
            {
                return dir_;
            }
            set
            {
                dir_ = value;
            }
        }

        public int Width
        {
            get
            {
                return width_;
            }
            set
            {
                width_ = value;
            }
        }
        public Paddle(Vector2D pos, int vida)
        {
            pos_ = pos;
            vida_ = vida;
            dir_ = new Vector2D();
            width_ = WIDTH;
        }

        public void Render()
        {
            for (int i = 0; i < width_; i++)
            {
                Console.SetCursorPosition(2* pos_.getX() + i, pos_.getY());
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write("  ");
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public void Update()
        {

            pos_.setX(pos_.getX() + dir_.getX());
        }

        public void handleInput(char c)
        {
            switch (c)
            {
                case 'l':
                    dir_.setX(-1);
                    break;
                case 'r':
                    dir_.setX(1);
                    break;
                default:
                    dir_.setX(0);
                    break;
            }
        }

        public bool isDead()
        {
            return vida_ <= 0;
        }

    }



}
