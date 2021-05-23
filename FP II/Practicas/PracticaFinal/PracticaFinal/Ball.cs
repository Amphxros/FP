using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaFinal
{
    class Ball
    {
        public Random rnd;
        private Vector2D pos_, dir_;
        private int screen_height_;
        private int screen_width_;

        public Vector2D Position { get { return pos_; } }

        public Vector2D Direction { get { return dir_; } }

        public Ball()
        {
            pos_ = new Vector2D();
            dir_ = new Vector2D();
        
        }
        
        public Ball(Vector2D pos)
        {
            pos_ = pos;
            dir_ = new Vector2D();
            rnd = new Random();
        }

        public void Init()
        {
            Vector2D[] posDirs = { 
                new Vector2D(-1,-1),
                new Vector2D(-1, 1),
                new Vector2D(1, -1),
                new Vector2D(1,1)
            };

            dir_ = posDirs[rnd.Next(0, posDirs.Length)];

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
