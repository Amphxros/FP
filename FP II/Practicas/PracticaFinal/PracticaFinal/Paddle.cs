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
        Vector2D pos_, dir_;
        int vida_;
        int width_;

        public Paddle(Vector2D pos, int vida)
        {
            pos_ = pos;
            vida_ = vida;
            dir_ = new Vector2D();
            width_ = WIDTH;
        }

        public Vector2D getPos()
        {
            return pos_;
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
