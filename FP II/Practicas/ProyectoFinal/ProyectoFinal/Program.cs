using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal
{
    class Program
    {
       const int NUM_FANTASMAS=4;
       const int NUM_VIDAS=4;
       
       public static int[] ghosts; //array de fantasmas

        static void Main(string[] args)
        {
            Mapa map= new Mapa();
            ReadMap("Mapas/level01.dat", out map);

            Console.Clear();
            map.Render();
        }
        static void ReadMap(string file,out Mapa map)
        {
            StreamReader read = new StreamReader(file);

            map = new Mapa(20, 20);

            int i = 0;

            while(i<20 && !read.EndOfStream) { 
            
            string line = read.ReadLine();
                for (int j = 0; j < line.Length; j++)
                {
                    switch (line[j])
                    {
                        case '0':
                            map.setCell(i, j, (CellType.Empty));
                            break;
                        case '1':
                            map.setCell(i, j, (CellType.Wall));
                            break;
                        case '2':
                            map.setCell(i, j, (CellType.Food));
                            break;
                        case '3':
                            map.setCell(i, j, (CellType.PowerUp));
                            break;
                        case '4':
                            map.setCell(i, j, (CellType.Prize));
                            break;
       
                    }

                }
                i++;
            }
        }
        static void Render()
        {
           
        }
        static void Update()
        {

        }

        static void HandleInput()
        {

        }
    }
}
