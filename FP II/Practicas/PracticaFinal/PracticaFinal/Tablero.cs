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
        Random rnd = new Random();
        ConsoleColor[] col = { ConsoleColor.Red, ConsoleColor.Cyan, ConsoleColor.Yellow, ConsoleColor.Magenta, ConsoleColor.White,ConsoleColor.Green };
        public Tablero(int width, int height)
        {
            bloques = new Bloque[width, height];
            for(int i = 0; i < width; i++)
            {
                
                for (int j = 0; j < height; j++)
                {
                    Vector2D pos = new Vector2D(i, j);
                    bloques[i, j] = new Bloque(pos, 1,col[rnd.Next(0, col.Length)]);
                
                }
            }

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

            Console.SetCursorPosition(40, 40);
        }
        public void Update()
        {

        }

    }
}
