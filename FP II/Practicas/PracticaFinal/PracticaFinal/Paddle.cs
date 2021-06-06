// Rubio Bellon Amparo

using System;

namespace PracticaFinal
{
    class Paddle
    {
        private int initialWidth_;
        //get and sets the position
        public Vector2D Position { get; set; }
        //get and sets the direction
        public Vector2D Direction { get; set; }
        public int Vida { get; set; }

        public int Width { get; set; }
        public Paddle(Vector2D pos, int vida, int width)
        {
            Position = pos;
            Vida = vida;
            Direction = new Vector2D();
            Width = width;
            initialWidth_ = width;



        }

        public void Render()
        {
            for (int i = 0; i < Width; i++)
            {
                Console.SetCursorPosition(Position.getX() + i, Position.getY());
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write(" ");
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public void Update()
        {
            Position.setX(Position.getX() + Direction.getX());
        }

        public void handleInput(char c)
        {
            switch (c)
            {
                case 'l':
                    Direction.setX(-2);
                    break;
                case 'r':
                    Direction.setX(2);
                    break;
                default:
                    Direction.setX(0);
                    break;
            }
        }

        public bool isDead()
        {
            return Vida <= 0;
        }

        public void resetWidth()
        {
            Width = initialWidth_;
        }

    }



}
