using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaFinal
{
    class Ball
    {
        private Vector2D pos_, dir_;
        private int screen_height_;
        private int screen_width_;

        public Ball()
        {

        }
        
        public Ball(Vector2D pos)
        {
            pos_ = pos;

        }

        public void Init()
        {

        }

        public void Render()
        {
            Console.SetCursorPosition(2 * pos_.getX(), pos_.getY());
            Console.Write("  ");
        }

        public void MoveBall()
        {
            pos_.setX(pos_.getX() + dir_.getX());
            pos_.setY(pos_.getY() + dir_.getY());
        }
        public void ChangeX()
        {
            dir_.setX(-dir_.getX());
        }

        public void ChangeY()
        {
            dir_.setY(-dir_.getY());
        }

    }
}
