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

            ListaPares lst; //para acumular direcciones
        }
        Personaje[] pers;
        // colores para los personajes
        ConsoleColor[] colors = {ConsoleColor.Yellow, ConsoleColor.Red,ConsoleColor.Magenta, ConsoleColor.Cyan, ConsoleColor.DarkBlue };
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
                int num_char = 0;
                while (!read.EndOfStream)
                {
                    string s = read.ReadLine();
                    w = s.Length;
                    for(int i = 0; i < s.Length; i++)
                    {
                        int t = (int)Char.GetNumericValue(s[i]);
                        temp[i, h] = t;
                        if (t >= 5 && t<10)
                        {
                            num_char++;
                        }
                    }
                    Console.WriteLine();
                    h++;
                }

                cas = new Casilla[h, w];
                pers = new Personaje[num_char];
                for(int i = 0; i < w; i++)
                {
                    for(int j = 0; j < h; j++)
                    {
                        switch (temp[i, j])
                        {
                            case 0:
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                                cas[j, i] = (Casilla)temp[i, j];
                                break;
                            default:
                                if (temp[i, j] >= 5)
                                {
                                    cas[j, i] = Casilla.Libre;
                                    int char_ = 9-temp[i, j];
                                    pers[char_] = new Personaje();
                                     pers[char_].pos = new Coord(i,j);
                                     pers[char_].ini = pers[char_].pos;
                                     pers[char_].dir = new Coord(0,0);
                                }
                           
                                break;
                        }
                    }
                }

            }
        }
        public void Dibuja()
        {
            Console.Clear();
            int dimX = cas.GetLength(0);
            int dimY = cas.GetLength(1);

            for (int i=0;i<dimY; i++)
            {
                for(int j=0;j<dimX; j++)
                {
                    Console.SetCursorPosition((2* i), ( 2*j));
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
                            Console.BackgroundColor = colorsbg[0];
                            break;
                        
                        case Casilla.Comida:
                            Console.BackgroundColor = colorsbg[0];
                            Console.ForegroundColor = colorsbg[1];
                            Console.Write("**");
                            break;

                        case Casilla.Vitamina:
                            Console.BackgroundColor = colorsbg[0];
                            Console.ForegroundColor = colorsbg[2];
                            Console.Write("**");
                            Console.BackgroundColor = colorsbg[0];
                            break;
                    }

                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            
            for(int i = 0; i < pers.Length; i++)
            {
                Console.SetCursorPosition((2*pers[i].pos.fil), ( 2*pers[i].pos.col));

                Console.BackgroundColor = colorsbg[0];
                if (i == 0)
                {
                    Console.ForegroundColor = colors[i];
                    Console.Write(">>");
                    Console.ForegroundColor = ConsoleColor.White;

                }
                else
                {
                    Console.BackgroundColor = colors[i];
                    Console.Write("ºº");
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            if (DEBUG)
            {
            Console.SetCursorPosition(8* dimX +2, 3); ;
            Console.WriteLine(" DEBUG ");
                for (int i = 0; i < pers.Length; i++)
                {
                    Console.SetCursorPosition(8*dimX + 2, 5+ 2*i); ;

                    Console.ForegroundColor = colors[i];
                   
                    Console.Write(" pos: " + pers[i].pos.col + " " + pers[i].pos.fil);
                    Console.SetCursorPosition(8 * dimX + 2, 5 + 2 * i + 1); ;
                    Console.Write(" dir: " + pers[i].dir.col + " " + pers[i].dir.fil);
                
                }
            }

        }
        bool Siguiente(Coord pos, Coord dir, out Coord newPos)
        {
            bool result = false;

            newPos = pos + dir;

            if (newPos.fil >= cas.GetLength(0))
            {
                newPos.fil = 0;
            }
            else if (newPos.fil <= 0)
            {
                newPos.fil = cas.GetLength(0)-1;
            }

            if (newPos.col >= cas.GetLength(1))
            {
                newPos.col = 0;
            }
            else if (newPos.col <= 0)
            {
                newPos.col = cas.GetLength(1)-1;
            }

            if (cas[newPos.fil, newPos.col] != Casilla.Muro)
            {
                result = true;
            }

            return result;
        }
        public void MuevePacman()
        {
         
        }

        public bool CambiaDir(char c)
        {
            return false;
        }
    }
}
