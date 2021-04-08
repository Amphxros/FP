//Amparo Rubio Bellon
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace Practica2
{
    class Tablero
    {
        enum Casilla {Libre,Muro,Comida,Vitamina, MuroCelda}
        Casilla[,] cas;
        struct Personaje
        {
            public Coord pos, dir, // posicion y direccion actual
            ini; // posicion inicial (para fantasmas)
        }
        Personaje[] pers;
        // colores para los personajes
        ConsoleColor[] colors = {ConsoleColor.DarkYellow, ConsoleColor.Red,ConsoleColor.Magenta, ConsoleColor.Cyan, ConsoleColor.DarkBlue };
        ConsoleColor[] colorsbg = {ConsoleColor.Black, ConsoleColor.White, ConsoleColor.Green}; //color de la cas libre, muro y vitaminas

       const bool DEBUG = true;

        public Tablero(string file)
        {
            StreamReader read = new StreamReader(file);
            if (File.Exists(file))
            {
                int[,] temp= new int[200,200];
                int w = 0;
                int h = 0;

                while (!read.EndOfStream)
                {
                    string s = read.ReadLine();
                    w = s.Length;
                    for(int i = 0; i < s.Length; i++)
                    {
                        int t = int.Parse(s[i].ToString());
                        Console.Write(t);
                        temp[i, h] = t;
                    }
                    Console.WriteLine();
                    h++;
                }

            }
        }
        void Dibuja()
        {
            int dimX = cas.GetLength(0);
            int dimY = cas.GetLength(1);

            for(int i=0;i<dimY; i++)
            {
                for(int j=0;j<dimX; j++)
                {
                    Console.SetCursorPosition(2 * i, j);
                    switch (cas[j, i])
                    {
                        case Casilla.MuroCelda:
                        case Casilla.Libre:
                            Console.BackgroundColor = colorsbg[0];
                            Console.Write("  ");
                            break;

                        case Casilla.Muro:

                            Console.BackgroundColor = colorsbg[1];
                            Console.Write("  ");
                            break;
                        case Casilla.Comida:
                            Console.BackgroundColor = colorsbg[0];
                            Console.ForegroundColor = colorsbg[0];

                            Console.Write("**");
                            break;
                        case Casilla.Vitamina:

                            Console.BackgroundColor = colorsbg[0];
                            Console.ForegroundColor = colorsbg[2];

                            Console.Write("**");

                            break;
                    }

                }
            }

            for(int i = 0; i < pers.Length; i++)
            {
                Console.SetCursorPosition(2 * pers[i].pos.col, pers[i].pos.fil);
                
                if (i == 0)
                {

                }
                else
                {
                    Console.BackgroundColor = colors[i];
                    Console.Write("ºº");
                }
            }

            if (DEBUG)
            {
            Console.Write("DEBUG");
                Console.SetCursorPosition(dimX, 2);
                for (int i = 0; i < pers.Length; i++)
                {
                    Console.ForegroundColor = colors[i];
                    Console.WriteLine("pos" + pers[i].pos.col + " " + pers[i].pos.fil);
                    Console.WriteLine("dir" + pers[i].dir.col + " " + pers[i].dir.fil);
                    Console.WriteLine();
                }
            }

        }
    }
}
