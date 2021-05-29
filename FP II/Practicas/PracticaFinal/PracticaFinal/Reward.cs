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
        AddBalls,
        NextLevel
    };
    class Reward
    {
        Vector2D pos_, dir_;
        RewardID id_;
        int width_;
        ConsoleColor mColor_;
        public Reward()
        {
            pos_ = new Vector2D();
            dir_ = new Vector2D();
            mColor_ = ConsoleColor.Black;
            width_ = 0;

            throw new Exception("Reward not valid");
        }
        
        public Reward(Vector2D pos, Vector2D dir,int width,RewardID id)
        {
            pos_ = pos;
            dir_ = dir;
            id_ = id;
            width_ = width;
        }
        
    }
}
