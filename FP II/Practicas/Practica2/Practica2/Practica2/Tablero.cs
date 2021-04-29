//Amparo Rubio Bellon

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Practica2
{
    class Tablero
    {
        Random rnd;
        enum Casilla { Libre, Muro, Comida, Vitamina, MuroCelda }
        Casilla[,] cas;

        Coor[] dirs = { new Coor(1, 0), new Coor(0, 1), new Coor(-1, 0), new Coor(0, -1) };
        enum Tipodir { RIGHT, DOWN, LEFT, UP }; //enum para que sea mas legible las dirs en lugar de dirs[0] usar dirs[LEFT]

        const int lapCarcelFantasmas = 30; // retardo para quitar el muro a los fantasmas
        int lapFantasmas; // tiempo restante para quitar el muro
        int numComida; // numero de casillas restantes con comida o vitamina

        struct Personaje
        {
            public Coor pos, dir, // posicion y direccion actual
            ini; // posicion inicial (para fantasmas)

        }
        Personaje[] pers;

        // colores para los personajes
        ConsoleColor[] colors = { ConsoleColor.Yellow, ConsoleColor.Red, ConsoleColor.Magenta, ConsoleColor.Cyan, ConsoleColor.DarkBlue }; //colores de los personajes
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
                 
                    string[] line = s.Split(' '); //asi quitamos os espacios de la matriz
                    if (w == 0 || line.Length==w)
                    {
                        for (int i = 0; i < line.Length; i++)
                        {
                            int t = int.Parse(line[i]);
                            temp[i, h] = t;
                            if (DEBUG)
                            {
                                Console.Write(t + " ");
                            }
                        }

                    Console.WriteLine();
                    w = line.Length;
                    h++;
                    }
                }

                cas = new Casilla[h, w];
                pers = new Personaje[5];
                numComida = 0;

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
                                if(cas[j, i] == Casilla.Comida)
                                {
                                    numComida++;
                                }
                                break;
                            default:
                                if (temp[i, j] >= 5)
                                {
                                    cas[j, i] = Casilla.Libre;
                                    int cas_ = 9 - temp[i, j];
                                    pers[cas_] = new Personaje();
                                    pers[cas_].pos = new Coor(i, j);
                                    pers[cas_].ini = pers[cas_].pos;
                                    pers[cas_].dir = new Coor(0, 0);
                                  
                                }
                                
                                break;
                        }
                    }
                }

                if (DEBUG)
                {
                    rnd = new Random(100);
                }
                else
                {
                    rnd = new Random();
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
                            Console.BackgroundColor = colorsbg[2];
                            Console.Write("  ");
                            break;
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
            if (DEBUG) //debugeado
            {
                Console.SetCursorPosition(3 * dimX + 10, 0);
                Console.Write("Debug: "+" dimX: "+cas.GetLength(0) + " dimY " + cas.GetLength(1) + " lap: " + lapFantasmas + " comida: " + numComida);

                Console.ForegroundColor = colors[0];
                Console.SetCursorPosition(3 * dimX + 10, 1);
                Console.Write("Pos: " + pers[0].pos.col + " " + pers[0].pos.fil + " dir: " + pers[0].dir.col + " " + pers[0].dir.fil);

                Console.ForegroundColor = colors[1];
                Console.SetCursorPosition(3 * dimX + 10, 2);
                Console.Write("Pos: "+ pers[1].pos.col + " " + pers[1].pos.fil + " dir: " + pers[1].dir.col+" "+ pers[1].dir.fil);

                Console.ForegroundColor = colors[2];
                Console.SetCursorPosition(3 * dimX + 10, 3);
                Console.Write("Pos: " + pers[2].pos.col + " " + pers[2].pos.fil + " dir: " + pers[2].dir.col + " " + pers[2].dir.fil);

                Console.ForegroundColor = colors[3];
                Console.SetCursorPosition(3 * dimX + 10, 4);
                Console.Write("Pos: " + pers[3].pos.col + " " + pers[3].pos.fil + " dir: " + pers[3].dir.col + " " + pers[3].dir.fil);

                Console.ForegroundColor = colors[4];
                Console.SetCursorPosition(3 * dimX + 10, 5);
                Console.Write("Pos: " + pers[4].pos.col + " " + pers[4].pos.fil + " dir: " + pers[4].dir.col + " " + pers[4].dir.fil);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            for (int i = 0; i < pers.Length; i++)
            {
                int n = pers[i].pos.fil;
                int m = pers[i].pos.col;
                Console.SetCursorPosition((2 * n), m);
                Console.ForegroundColor = colors[i];
                if (i == 0)
                {
                    if (pers[0].dir == dirs[(int)Tipodir.RIGHT]) //si esta yendo hacia la derecha
                    {
                        Console.Write("<<");
                    }
                    else if (pers[0].dir == dirs[(int)Tipodir.DOWN])//si esta yendo hacia abajo
                    {
                        Console.Write("^^");
                    }
                    else if (pers[0].dir == dirs[(int)Tipodir.UP]) //si esta yendo hacia arriba
                    {
                        Console.Write("VV");
                    }
                    else //si esta yendo a la izquierda o por defecto
                    {
                        Console.Write(">>");
                    }
                }
                else
                {
                    Console.Write("ºº");
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        bool Siguiente(Coor pos, Coor dir, out Coor newPos)
        {
            bool result = false;
            newPos = new Coor();
            newPos.col = pos.col + dir.col;
            newPos.fil = pos.fil + dir.fil;

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

            if (cas[newPos.col, newPos.fil] != Casilla.Muro && cas[newPos.col, newPos.fil] != Casilla.MuroCelda)
            {
                result = true;
            }

            return result;
        }
        public void MuevePacman()
        {
            Coor n = new Coor();

            if (Siguiente(pers[0].pos, pers[0].dir, out n))
            {
                pers[0].pos = n;
            }
            if (cas[pers[0].pos.col, pers[0].pos.fil] == Casilla.Comida)
            {
                numComida -= 1;

            }

            if (cas[pers[0].pos.col, pers[0].pos.fil] == Casilla.Vitamina)
            {
               
                for (int i = 1; i < pers.Length; i++)
                {
                    reseteaPosicion(i);
                }

            }
            cas[pers[0].pos.col, pers[0].pos.fil] = Casilla.Libre;


        }

        private void reseteaPosicion(int fant)
        {
            pers[fant].pos = pers[fant].ini;
            pers[fant].dir = new Coor();
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
                if (Siguiente(pers[fant].pos, dirs[i], out c)) // si no hay muro insertamos en la lista como posible
                {
                    lst.insertaFin(dirs[i]);
                }

            }
            for (int i = 0; i < lst.getElems(); i++)
             {
                 Coor dir = lst.getnEsimo(i);
                 if ((dir == -pers[fant].dir || HayFantasma(pers[fant].pos + dir))) //si es la opuesta a la direccion actual o si se borra para dar un movimiento mas fluido 
                 {
                     lst.borraElto(dir);
                 }
             }
            
            return lst.getElems();

        }
        void SeleccionaDir(int fant)
        {
            ListaPares lst= new ListaPares();
            int dir = PosiblesDirs(fant, out lst);
            if (dir > 1)
            {
                pers[fant].dir = lst.getnEsimo(rnd.Next(0, dir));
            }
            //esto es para ademas teletransportar a los fantasmas si es necesario
            Coor newpos;
          if( Siguiente(pers[fant].pos, pers[fant].dir, out newpos))
                pers[fant].pos = newpos; 
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
                SeleccionaDir(i); //seleccionamos una dir entre las posiblesç
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
        public bool Captura()
        {
            int i = 1;
            while (i < pers.Length && pers[i].pos != pers[0].pos)
            {
                i++;
            }

            return i<pers.Length;
        }
        public bool finNivel()
        {
            return numComida <= 0;
        }
    }
}
