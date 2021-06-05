using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaFinal
{
    public enum RewardID
    {
        AddWidth,
        LessWidth,
        AddBalls,
        LessBlocks,
        MorePrices,
        NextLevel,
        lastID
    };
    class Reward
    {
        Vector2D pos_, dir_;
        RewardID id_;
        public int Width { get; set; }
        public Vector2D Position { get { return pos_; } }
        public Vector2D Direction { get { return dir_; } }
        public RewardID ID { get { return id_; } }    
        public Reward()
        {
            pos_ = new Vector2D();
            dir_ = new Vector2D();
            Width = 0;

            throw new Exception("Reward not valid");
        }
        
        public Reward(Vector2D pos, Vector2D dir,int width,RewardID id)
        {
            pos_ = pos;
            dir_ = dir;
            id_ = id;
            Width = width;
        }

        public void Render()
        {
            for (int i = 0; i < Width; i++)
            {
                Console.SetCursorPosition(pos_.getX() + i, pos_.getY());
                Console.BackgroundColor = ConsoleColor.Green;
                Console.Write(" ");
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public void Move()
        {
            pos_.setY(pos_.getY() + dir_.getY());
        }
    }
}
