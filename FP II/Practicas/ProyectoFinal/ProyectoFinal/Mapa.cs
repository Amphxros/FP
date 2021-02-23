using System;
using System.IO;
namespace ProyectoFinal
{
    
    public enum CellType { Empty,Wall,Food,PowerUp,Prize};
    public struct Cell
    {
       public CellType cel;
    }
    class Mapa
    {
        Random rnd = new Random();
        private Cell[,] tab;

        //mapa vacio
        public Mapa()
        {
            tab = new Cell[0, 0];
        }

        //genera un mapa random de tamaño dimX, dimY
        public Mapa(int dimX, int dimY)
        {
            tab = new Cell[dimX, dimY];

            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    tab[i, j].cel = (CellType)rnd.Next(0, 6);
                }
            }
        }
     
        public void Render()
        {
            for(int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    Console.SetCursorPosition(2 * i, j);
                    switch (tab[i, j].cel)
                    {
                        case CellType.Empty:
                            Console.Write("  ");
                            break;
                        case CellType.Wall:
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.Write("  ");
                            break;
                        case CellType.Food:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("· ");
                            Console.BackgroundColor = ConsoleColor.Black;

                            break;
                        case CellType.PowerUp:
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write("··");

                            break;
                        case CellType.Prize:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("# ");
                            break;
                    }
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        public void setCell(int i, int j, CellType c)
        {
            tab[i, j].cel = c;
        }
    }


}
