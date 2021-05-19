using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaFinal
{
    class Tablero
    {
        Bloque[,] bloques;
        Paddle player;
        Random rnd = new Random();

        ConsoleColor[] col = { ConsoleColor.Red, ConsoleColor.Cyan, ConsoleColor.Yellow, ConsoleColor.Magenta, ConsoleColor.White,ConsoleColor.Green };
        public Tablero(int width, int height)
        {
            bloques = new Bloque[width, height];
            for(int i = 0; i < width; i++)
            {
                for (int j = 1; j < height+1; j++)
                {
                    Vector2D pos = new Vector2D(i, j);
                    bloques[i, j-1] = new Bloque(pos, 1,col[rnd.Next(0, col.Length)]);
                
                }
            }

            Vector2D p = new Vector2D(width / 2, 2 * height);
            player = new Paddle(p, 3);

        }
        public Tablero(string file)
        {

        }

        public void Render()
        {
            Console.Clear();
            for(int i=0;i<bloques.GetLength(0); i++)
            {
                for(int j= 0; j<bloques.GetLength(1); j++)
                {
                    bloques[i, j].Render();
                }
            }

            player.Render();
            Console.WriteLine();

        }
        public void MuevePala(char c)
        {
        
            player.handleInput(c);

            if (player.Position.getX() + player.Direction.getX() > 0 && player.Position.getX() + player.Direction.getX() - 1.5* player.Width < bloques.GetLength(0))
            {
                player.Update();
            }

        } 

    }

}
