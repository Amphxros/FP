﻿//Amparo Rubio Bellon

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Practica2
{
    class Tablero
    {
        Random rnd= new Random();
        enum Casilla { Libre, Muro, Comida, Vitamina, MuroCelda }
        Casilla[,] cas;

        Coor[] dirs = { new Coor(1, 0), new Coor(0, 1), new Coor(-1, 0), new Coor(0, -1) };
        enum Tipodir { RIGHT, DOWN, LEFT, UP };

        const int lapCarcelFantasmas = 3000; // retardo para quitar el muro a los fantasmas
        int lapFantasmas; // tiempo restante para quitar el muro
        int numComida; // numero de casillas restantes con comida o vitamina

        struct Personaje
        {
            public Coor pos, dir, // posicion y direccion actual
            ini; // posicion inicial (para fantasmas)

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
                int[,] temp = new int[100, 100];
                int w = 0;
                int h = 0;
                int num_char = 0;
                while (!read.EndOfStream)
                {
                    string s = read.ReadLine();
                    w = s.Length;
                    string[] line = s.Split(' ');
                    for (int i = 0; i < line.Length; i++)
                    {
                        int t = int.Parse(line[i]);
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
                                    pers[char_].pos = new Coor(i, j);
                                    pers[char_].ini = pers[char_].pos;
                                    pers[char_].dir = new Coor(0, 0);
                                  
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
                    Console.SetCursorPosition((2 * i), ( j));
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
            if (DEBUG)
            {
                Console.SetCursorPosition(4* dimX,0);

              //  for (int i = 0; i < pers.Length; i++)
              //  {
              //      Console.ForegroundColor = colors[i];
              //
              //      Console.SetCursorPosition(4* dimX +2, 2*i);
              //      Console.Write(pers[i].pos.col + " " + pers[i].pos.fil);
              //
              //      Console.SetCursorPosition(4*dimX, 2 * (i + 1));
              //      Console.Write(pers[i].dir.col + " " + pers[i].dir.fil);
              //
              //  }
              //
                Console.SetCursorPosition(4* dimX, dimY);
                Console.Write(lapFantasmas);

            }
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < pers.Length; i++)
            {
                int n = pers[i].pos.fil;
                int m = pers[i].pos.col;
                Console.SetCursorPosition((2 * n), ( m));
                Console.ForegroundColor = colors[i];
                if (i == 0)
                {
                    if (pers[0].dir == dirs[(int)Tipodir.LEFT])
                    {
                        Console.Write(">>");
                    }
                    else if (pers[0].dir == dirs[(int)Tipodir.RIGHT])
                    {

                        Console.Write("<<");
                    }
                    else if (pers[0].dir == dirs[(int)Tipodir.DOWN])
                    {

                        Console.Write("^^");
                    }
                    else if (pers[0].dir == dirs[(int)Tipodir.UP])
                    {

                        Console.Write("VV");
                    }

                }
                else
                {
                    Console.Write("ºº");
                }
            }
        }

        bool Siguiente(Coor pos, Coor dir, out Coor newPos)
        {
            bool result = false;

            newPos = pos + dir;

            if (newPos.col >= cas.GetLength(0))
            {
                newPos.col = 0;
            }
            else if (newPos.col < 0)
            {
                newPos.col = cas.GetLength(0) - 1;
            }
           
            if (newPos.fil >= cas.GetLength(1))
            {
                newPos.fil = 0;
            }
            else if (newPos.fil < 0)
            {
                newPos.fil = cas.GetLength(1) - 1;
            }
           
            if(cas[newPos.col,newPos.fil]!=Casilla.Muro && cas[newPos.col, newPos.fil] != Casilla.MuroCelda)
                result = true;
            

            return result;
        }
        public void MuevePacman()
        {
            Coor n = new Coor();
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
          
            if (Siguiente(pers[0].pos, pers[0].dir, out n))
            {
                pers[0].pos = n;
            }
            else
            {

            }

        }



        public bool CambiaDir(char c)
        {
            Coor d = new Coor();
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

            Coor n = new Coor();
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

        int PosiblesDirs(int fant, out ListaPares lst) {
            lst = new ListaPares();
            for (int i = 0; i < dirs.Length; i++)
            {
                Coor c = new Coor();
                if (Siguiente(pers[fant].pos, dirs[i], out c))
                {
                    lst.insertaFin(dirs[i]);
                }

            }
            for (int i = 0; i < lst.getElems(); i++)
            {
                Coor dir = lst.getnEsimo(i);
                if (dir == -pers[fant].pos) //si es la opuesta la borra
                {
                    lst.borraElto(dir);
                }
            }

            return lst.getElems();

        }
        void SeleccionaDir(int fant)
        {
            ListaPares lst;
            int dir = PosiblesDirs(fant, out lst);

            pers[fant].dir = lst.getnEsimo(rnd.Next(0, dir));
             
        }
       public void MueveFantasmas(int lap)
       {
            lapFantasmas += lap;
            if (lapFantasmas >= lapCarcelFantasmas)
            {
                EliminaMuroFantasmas();
            }

            for(int i=1; i< pers.Length; i++)
            {
                SeleccionaDir(i); //seleccionamos una dir entre las posibles
                pers[i].pos= pers[i].pos + pers[i].dir; //y moveos los fantasmas
            }

       }

        bool HayFantasma(Coor c)
        {
            bool result = false;
            int i = 1;
            while (i < pers.Length && c != pers[i].pos)
            {
                i++;
            }
            result = i < pers.Length;
            return result;
        }
    }
}
