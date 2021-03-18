﻿using System;
using System.IO;

namespace Practica1
{
    struct Coor { public int fil, col; }; // coordenadas fila y columna en el tablero
    enum TipoCasilla { Muro, Libre, Destino }; // 3 tipos de casillas en el tablero
    struct Casilla
    {
        public TipoCasilla tipo; // información fija de la casilla (muro, libre o destino)
        public bool caja; // información variable: si tiene o no caja
    }
    struct Tablero
    { // tipo tablero
        public Casilla[,] cas; // matriz de casillas
        public Coor jug; // posición del jugador
    }
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            int n = -1;
            //aqui pediremos entrada para cargar 1 existente o no (en general inicializar cosas)
            Tablero t = new Tablero();
            string moves = "";
            
                int m = -1;
                while (m != 0 && m != 1)
                {
                    Console.Write("Cargar una partida existente[0] o crear una nueva[1]");
                    m = int.Parse(Console.ReadLine());

                }
                if (m == 1)
                {
                    t = LeeNivel("levels", n);

                }
                else
                {
                    int l = 0;
                    do
                    {
                        Console.Write("Introduce el nivel a cargar: ");
                        l = int.Parse(Console.ReadLine());

                    } while (l > 0 && l < 50);


                    if (!CargaPartida(l.ToString(), out n, out t, moves))
                    {
                        n = 0;
                        moves = "";
                        t = LeeNivel("levels", n);
                    }
                }
        
             while (!exit && n< 50) { 
               Dibuja(t, 0);
                while (!Terminado(t) && !exit)
                {
                    char c = LeeInput();
                    if (c == 'q')
                        exit = true;
                    else if (c == 's')
                    {
                        GuardaPartida(n, t, moves);
                    }
                    else
                    {
                        ProcesaInput(ref t, c, ref moves);
                    }
                    System.Threading.Thread.Sleep(300);
                }
                n++;
                t = LeeNivel("levels", n);
            }
        }
        static Tablero LeeNivel(string file,int n)
        {
            Tablero tab= new Tablero();
            StreamReader read_ = new StreamReader(file);
            if (read_ != null)
            {
                int fils = 0;
                int cols=0;
                bool encontrado = false, flag=false; //booleano de encontrar el nivel y bool para encontrar el blanco de linea
                
                string[] tmp = new string[100]; //array temporal

                while(!read_.EndOfStream &&  fils<tmp.Length && !flag)
                {
                    if (!encontrado) {
                        string line = read_.ReadLine();
                        string[] pals = line.Split(' ');
                        if (pals.Length==2 && pals[0]=="Level" && int.Parse(pals[1].ToString())==n)
                        {
                            encontrado = true;
                        } 
                    }
                    else
                    {
                        tmp[fils] = read_.ReadLine();

                        string[] pals = tmp[fils].Split(' ');
                        if (pals[0]=="Level" )
                        {
                            flag = true;
                        }
                       else if (cols < tmp[fils].Length)
                        {
                            cols =tmp[fils].Length; 
                        }

                        Console.WriteLine(tmp[fils]);
                        fils++;
                    }
                }

                tab.cas = new Casilla[fils, cols];

                for(int i=0; i < fils; i++)
                {
                    for(int j = 0; j < cols; j++)
                    {
                        tab.cas[i, j].tipo = TipoCasilla.Muro;
                    }
                }

                for (int i = 0; i < fils; i++)
                {
                   
                    for (int j = 0; j < tmp[i].Length; j++)
                    {
                        switch (tmp[i][j])
                        {
                            //casilla destino
                            case '.':
                                tab.cas[i, j].tipo = TipoCasilla.Destino;
                                tab.cas[i, j].caja = false;
                                break;
                            case '*':
                                tab.cas[i, j].tipo = TipoCasilla.Destino;
                                tab.cas[i, j].caja = true;
                                break;

                            //casilla libre
                            case ' ':
                                tab.cas[i, j].tipo = TipoCasilla.Libre;
                                tab.cas[i, j].caja = false;
                                break;

                            case '$':
                                tab.cas[i, j].tipo = TipoCasilla.Libre;
                                tab.cas[i, j].caja = true;
                                break;

                            //jugador
                            case '@':
                                tab.cas[i, j].tipo = TipoCasilla.Libre;
                                tab.jug.fil = j;
                                tab.jug.col = i;
                                break;
                            case '+':
                                tab.cas[i, j].tipo = TipoCasilla.Destino;
                                tab.jug.fil = j;
                                tab.jug.col = i;
                                break;
                            //muro
                            case '#':
                                tab.cas[i, j].tipo = TipoCasilla.Muro;
                                tab.cas[i, j].caja = false;
                                break;
                        }
                    }
                }
            }

            return tab;
        }
        static void Dibuja(Tablero tab, int mov)
        {
            Console.Clear();
            for(int i = 0; i < tab.cas.GetLength(0); i++)
            {
                for (int j = 0; j < tab.cas.GetLength(1); j++)
                {
                    Console.SetCursorPosition(2 * i, j);
                      switch (tab.cas[i, j].tipo)
                      {
                          case TipoCasilla.Muro:
                              Console.BackgroundColor = ConsoleColor.DarkCyan;
                              Console.Write("  ");
                              Console.BackgroundColor = ConsoleColor.Black;
                            break;
                          case TipoCasilla.Libre:
                            if (tab.cas[i, j].caja)
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.Write("[]");
                                Console.BackgroundColor = ConsoleColor.Black;

                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.Black;
                                 Console.Write("  ");
                            }
                            break;
                          case TipoCasilla.Destino:

                              if (tab.cas[i, j].caja)
                              {
                              Console.BackgroundColor = ConsoleColor.Yellow;
                              }
                              else
                              {
                                Console.BackgroundColor = ConsoleColor.Black;

                              }
                                Console.Write("()");
                                Console.BackgroundColor = ConsoleColor.Black;
                              break;
                
                      }

                }

            }

            Console.SetCursorPosition(2 * tab.cas.GetLength(0) + 3, 0);
            Console.Write("moves: " + mov);
            Console.BackgroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(2 * tab.jug.col, tab.jug.fil);
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write("ºº");
            Console.BackgroundColor = ConsoleColor.Black;
          
        }

        static bool Siguiente(Coor pos, char dir, Tablero tab, out Coor sig)
        {
            sig = pos;
            switch (dir)
            {
                case 'l':
                    sig.col=pos.col-1;
                 
                    break;
                case 'u':
                    sig.fil= pos.fil-1;
                  
                    break;
                case 'r':
                    sig.col=pos.col+1;
                   
                    break;
                case 'd':
                    sig.fil=pos.fil+1;
                 
                    break;
            }
           
           
            return tab.cas[sig.col, sig.fil].tipo != TipoCasilla.Muro;
            
        }
        static char LeeInput()
        {
            char d = ' ';
            if (Console.KeyAvailable)
            {
                string tecla = Console.ReadKey(true).Key.ToString();
                switch (tecla)
                {
                    case "LeftArrow":  d = 'l'; break;
                    case "DownArrow":  d = 'd'; break;
                    case "UpArrow":    d = 'u'; break;
                    case "RightArrow": d = 'r'; break;
                    case "Q":case "q": d = 'q'; break;
                    case "Z":case "z": d = 'p'; break;
                    case "S":case "s": d = 's'; break;
                }
            }
            while (Console.KeyAvailable)
                (Console.ReadKey(false)).KeyChar.ToString();
            return d;

        }
        static char Mueve(ref Tablero tab, char dir)
        {
            char c = '.';
            Coor sig = new Coor();
            if (Siguiente(tab.jug, dir, tab, out sig))
            {
                Coor poscaja=sig;
                if((!tab.cas[sig.col, sig.fil].caja || tab.cas[sig.col, sig.fil].caja && Siguiente(sig,dir,tab,out poscaja)) && !(tab.cas[sig.col, sig.fil].caja && tab.cas[poscaja.col, poscaja.fil].caja))
                {
                    tab.jug = sig;
                    bool tmp = tab.cas[sig.col, sig.fil].caja;
                    tab.cas[sig.col, sig.fil].caja = tab.cas[poscaja.col, poscaja.fil].caja;
                    tab.cas[poscaja.col, poscaja.fil].caja = tmp;

                    switch (dir)
                    {
                        case 'l':
                            c = 'R';
                            break;
                        case 'u':
                            c = 'D';
                            break;
                        case 'r':
                            c = 'L';
                            break;
                        case 'd':
                            c = 'U';
                            break;
                    }
                }
             
            }
            return c;
        }
        static void ProcesaInput(ref Tablero tab, char dir, ref string movs)
        {
            switch (dir)
            {
                case 'l':
                case 'u':
                case 'r':
                case 'd':
                    movs += Mueve(ref tab, dir);
                    Dibuja(tab,movs.Length); //asi solo se renderiza cuando sea necesario
                    break;
                case 'z':
                    break;
                
            }
        }

        static void GuardaPartida(int level, Tablero tab,string moves)
        {
            StreamWriter file = new StreamWriter(level.ToString() + ".level");
            if (file != null)
            {
                file.WriteLine("Level " + level);
                int x = tab.cas.GetLength(0);
                int y = tab.cas.GetLength(1);
                file.WriteLine(x + " " + y); //guardamos el tamaño del nivel
                for (int i = 0; i < y; i++)
                {
                    for (int j = 0; j <x; j++) {
                  
                        switch (tab.cas[j, i].tipo)
                        {
                            case TipoCasilla.Libre:
                                if (tab.cas[j, i].caja)
                                {
                                    file.Write("$");
                                }
                                else if(j==tab.jug.col && i == tab.jug.fil)
                                {

                                    file.Write("@");
                                }
                                else
                                {
                                    file.Write(" ");
                                }
                                break;
                            case TipoCasilla.Muro:
                                file.Write("#");
                                break;
                            case TipoCasilla.Destino:
                                if (tab.cas[j, i].caja)
                                {
                                    file.Write("*");
                                }
                                else if (j == tab.jug.col && i == tab.jug.fil)
                                {   
                                    file.Write("+");
                                }
                                else
                                {
                                    file.Write(".");
                                }
                                break;
                        }

                    }
                    file.WriteLine();
                }

                file.WriteLine(moves);
            }
            file.Close();

            Console.WriteLine("guardado");

        }

        static bool CargaPartida(string path,out int level, out Tablero tab, string moves)
        {
            StreamReader file = new StreamReader(path + ".level");
            level = 0;
            tab = new Tablero();
            if (file != null)
            {
                string s = file.ReadLine();
                string[] line = s.Split(' '); 

                if(line.Length==2 && line[0] == "Level")
                {
                    level = int.Parse(line[1]);
                }
                else
                {
                    return false;
                }

                s = file.ReadLine();
                line = s.Split(' ');
                int x = int.Parse(line[0]);
                int y = int.Parse(line[1]);
                tab.cas = new Casilla[x, y];
                for (int i = 0; i < x; i++)
                {
                    s = file.ReadLine();
                    for (int j = 0; j < s.Length -1; j++)
                    {
                        switch (s[j])
                        {
                            //casilla destino
                            case '.':
                                tab.cas[i, j].tipo = TipoCasilla.Destino;
                                tab.cas[i, j].caja = false;
                                break;
                            case '*':
                                tab.cas[i, j].tipo = TipoCasilla.Destino;
                                tab.cas[i, j].caja = true;
                                break;

                            //casilla libre
                            case ' ':
                                tab.cas[i, j].tipo = TipoCasilla.Libre;
                                tab.cas[i, j].caja = false;
                                break;

                            case '$':
                                tab.cas[i, j].tipo = TipoCasilla.Libre;
                                tab.cas[i, j].caja = true;
                                break;

                            //jugador
                            case '@':
                                tab.cas[i, j].tipo = TipoCasilla.Libre;
                                tab.jug.fil = j;
                                tab.jug.col = i;
                                break;
                            case '+':
                                tab.cas[i, j].tipo = TipoCasilla.Destino;
                                tab.jug.fil = j;
                                tab.jug.col = i;
                                break;
                            //muro
                            case '#':
                                tab.cas[i, j].tipo = TipoCasilla.Muro;
                                tab.cas[i, j].caja = false;
                                break;
                        }
                    }
                }
                return true;

            }
            else
            {
                return false;
            }

        }

        static bool Terminado(Tablero tab)
        {
            bool flag = false;
            int i = 0, j = 0;

            while( i < tab.cas.GetLength(0) && !flag){
                j = 0;
                while (j < tab.cas.GetLength(1) && !flag)
                {
                    if(!tab.cas[i,j].caja && tab.cas[i, j].tipo == TipoCasilla.Destino)
                    {
                        flag = true;
                    }
                    j++;
                }
                i++;
            }

            return !flag;
        }

    }
}
