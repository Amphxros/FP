//Amparo Rubio Bellon

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Practica2
{
    class Tablero
    {
        enum Casilla { Libre, Muro, Comida, Vitamina, MuroCelda }
        Casilla[,] cas;

        Coord[] dirs = { new Coord(2, 0), new Coord(0, 1), new Coord(-2, 0), new Coord(0, -1) };
        enum Tipodir { RIGHT, DOWN, LEFT, UP };
        struct Personaje
        {
            public Coord pos, dir, // posicion y direccion actual
            ini; // posicion inicial (para fantasmas)

            public ListaPares lst; //para acumular direcciones
        }
        Personaje[] pers;
        // colores para los personajes
        ConsoleColor[] colors = { ConsoleColor.Yellow, ConsoleColor.Red, ConsoleColor.Magenta, ConsoleColor.Cyan, ConsoleColor.DarkBlue };
        ConsoleColor[] colorsbg = { ConsoleColor.Black, ConsoleColor.White, ConsoleColor.Green }; //color de la cas libre, muro y vitaminas

        const bool DEBUG = true;

        public Tablero(string file)
        {
            StreamReader read = new StreamReader(file);
            if (File.Exists(file))
            {
                int[,] temp = new int[200, 200];
                int w = 0;
                int h = 0;
                int num_char = 0;
                while (!read.EndOfStream)
                {
                    string s = read.ReadLine();
                    w = s.Length;
                    for (int i = 0; i < s.Length; i++)
                    {
                        int t = (int)Char.GetNumericValue(s[i]);
                        temp[i, h] = t;
                        if (t >= 5 && t < 10)
                        {
                            num_char++;
                        }
                    }
                    Console.WriteLine();
                    h++;
                }

                cas = new Casilla[h, w];
                pers = new Personaje[num_char];


                for (int i = 0; i < w; i++)
                {
                    for (int j = 0; j < h; j++)
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
                                    int char_ = 9 - temp[i, j];
                                    pers[char_] = new Personaje();
                                    pers[char_].pos = new Coord(i, j);
                                    pers[char_].ini = pers[char_].pos;
                                    pers[char_].dir = new Coord(0, 0);
                                    pers[char_].lst = new ListaPares(pers[char_].dir);
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

            for (int i = 0; i < dimY; i++)
            {
                for (int j = 0; j < dimX; j++)
                {
                    Console.SetCursorPosition((2 * i), (2* j));
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

            for (int i = 0; i < pers.Length; i++)
            {
                int n = pers[i].pos.fil;
                int m = pers[i].pos.col;
                Console.SetCursorPosition((2 * n), (2 * m));
                Console.ForegroundColor = colors[i];
                if (i == 0)
                {

                    switch (pers[i].dir.fil)
                    {
                        case -1:
                            Console.Write(">>");
                            break;
                        case 1:
                            Console.Write("<<");
                            break;
                        default: //en caso de que sea 0
                            switch (pers[i].dir.col)
                            {
                                case 1:
                                    Console.Write("^^");
                                    break;
                                case -1:
                                    Console.Write("VV");
                                    break;
                                default:
                                    Console.Write(">>"); //si su dir es 0,0 
                                    break;
                            }
                            break;
                    }



                }
                else
                {
                    Console.Write("ºº");
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            if (DEBUG)
            {
                Console.SetCursorPosition(8 * dimX + 2, 3); 
                Console.WriteLine(" DEBUG " + cas.GetLength(0) + " "+ cas.GetLength(1));
                for (int i = 0; i < pers.Length; i++)
                {
                    Console.SetCursorPosition(8 * dimX + 2, 5 + 2 * i); ;

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

            if (newPos.col >= cas.GetLength(0))
            {
                newPos.col = 0;
            }
            else if (newPos.col <= 0)
            {
                newPos.col = cas.GetLength(0) - 1;
            }
           
            if (newPos.fil >= cas.GetLength(1))
            {
                newPos.fil = 0;
            }
            else if (newPos.fil <= 0)
            {
                newPos.fil = cas.GetLength(1) - 1;
            }
           
            if(cas[newPos.col,newPos.fil]!=Casilla.Muro)
                result = true;
            

            return result;
        }
        public void MuevePacman()
        {
            Coord n = new Coord();
            if (Siguiente(pers[0].pos, pers[0].dir, out n))
            {
                pers[0].pos = n;
                if (cas[pers[0].pos.col, pers[0].pos.fil] == Casilla.Comida)
                {
                    cas[pers[0].pos.col, pers[0].pos.fil] = Casilla.Libre;
                    Console.Beep(1440,30);
                }
                else if (cas[pers[0].pos.col, pers[0].pos.fil] == Casilla.Vitamina)
                {
                    cas[pers[0].pos.col, pers[0].pos.fil] = Casilla.Libre;
                    Console.Beep(1550,60);
                }
            }
            else
            {

            }

        }

        bool HayFantasma(Coord c)
        {
            return false;
        }

        public bool CambiaDir(char c)
        {
            Coord d = new Coord();
            switch (c)
            {
                case 'l':
                    d = dirs[(int)Tipodir.LEFT];
                    break;

                case 'u':
                    d = dirs[(int)Tipodir.UP];
                    break;

                case 'r':
                    d = dirs[(int)Tipodir.RIGHT];
                    break;

                case 'd':
                    d = dirs[(int)Tipodir.DOWN];
                    break;

            }

            Coord n = new Coord();
            if (Siguiente(pers[0].pos, d, out n))
            {
                pers[0].dir = d;
                return true;

            }
            else
            {
                return false;
            }

        }
        void EliminaMuroFantasmas()
        {
            int dimX = cas.GetLength(0);
            int dimY = cas.GetLength(1);

            for (int i = 0; i < dimY; i++)
            {
                for (int j = 0; j < dimX; j++)
                {
                    if (cas[j, i] == Casilla.MuroCelda)
                    {
                        cas[j, i] = Casilla.Libre;
                    }
                }
            }
        }
         void SeleccionaDir(int fant)
        {

        }
        void MueveFantasmas(int lap)
        {

        }
    }
}
