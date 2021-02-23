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
       
       public static Mapa map;
       public static int[] ghosts; //array de fantasmas

        static void Main(string[] args)
        {
          
           
        }
        static void ReadMap(string file)
        {
            StreamReader read = new StreamReader(file);

            string s = read.ReadLine();
            int dimX, dimY;
            if (s.Length == 2)
            {
                dimX = (int)s[0];
                dimY = (int)s[1];
            }
            else
            {
                //excepcion
                throw new Exception("Archivo en formato incorrecto");
            }

            map = new Mapa(dimX, dimY);
            for (int i = 0; i < dimX; i++)
            {
                string line = read.ReadLine();
                for (int j = 0; j < dimY; j++)
                {
                    switch (line[j])
                    {
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                            map.setCell(i, j, (CellType)((int)line[j]));
                            break;

                        case '6':
                            break;
                        case '7':
                            break;
                        case '8':
                            break;
                        case '9':
                            //fantasmas

                            break;
                        default:
                            map.setCell(i, j, (CellType.Empty));
                            break;
                    }

                }
            }
        }
        static void Render()
        {
            map.Render();
        }
        static void Update()
        {

        }

        static void HandleInput()
        {

        }
    }
}
