using System;
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
            Tablero t = LeeNivel("levels", 1);
            Dibuja(t, 0);

            while(Terminado(t)){
                Dibuja(t, 0);
                ProcesaInput(ref t, LeeInput(),"");

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
                
                string[] tmp = new string[15]; //array temporal

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
                        if (pals[0] == "" || pals[0]=="Level")
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
                            case '.':
                                tab.cas[i, j].tipo = TipoCasilla.Destino;
                                tab.cas[i, j].caja = false;
                                break;

                            case ' ':
                                tab.cas[i, j].tipo = TipoCasilla.Libre;
                                tab.cas[i, j].caja = false;
                                break;

                            case '$':
                                tab.cas[i, j].tipo = TipoCasilla.Libre;
                                tab.cas[i, j].caja = true;
                                break;
                            case '@':
                                tab.cas[i, j].tipo = TipoCasilla.Libre;
                                tab.jug.fil = i;
                                tab.jug.col = j;
                                break;
                            default:
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

                              break;
                          case TipoCasilla.Libre:
                            if (tab.cas[i, j].caja)
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.Write("[]");
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
                              break;
                
                      }

                    Console.BackgroundColor = ConsoleColor.Black;
                }

            }

            Console.SetCursorPosition(2 * tab.jug.fil, tab.jug.col);
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write("ºº");
            
            Console.SetCursorPosition(20,20);
        }

        static bool Siguiente(Coor pos, char dir, ref Tablero tab, Coor sig)
        {
            Coor aux = tab.jug;
           
            if(tab.cas[tab.jug.fil, tab.jug.col].caja)
            {

            }

            return false;
        }
        static char LeeInput()
        {
            char d = ' ';
            if (Console.KeyAvailable)
            {
                string tecla = Console.ReadKey(true).Key.ToString();
                tecla = tecla.ToUpper(); // conversion a mayúsculas
                switch (tecla)
                {
                    case "LeftArrow": d = 'l'; break;
                    case "UpArrow": d = 'u'; break;
                    case "RightArrow": d = 'r'; break;
                    case "DownArrow": d = 'd'; break;
                }
            }
            while (Console.KeyAvailable)
                (Console.ReadKey(false)).KeyChar.ToString();
            return d;

        }
        static char Mueve(Tablero tab, char dir)
        {
            switch (dir)
            {
                case 'l':
                    if (tab.jug.fil - 1 < 0 && tab.cas[tab.jug.fil - 1, tab.jug.col].tipo != TipoCasilla.Muro)
                    {
                        tab.jug.fil--;

                    }
                    break;

                case 'u':
                    if (tab.jug.col - 1 < 0 && tab.cas[tab.jug.fil, tab.jug.col - 1].tipo != TipoCasilla.Muro)
                    {
                        tab.jug.col--;
                    }
                    break;

                case 'r':
                    if (tab.jug.fil + 1 > tab.cas.GetLength(0) && tab.cas[tab.jug.fil + 1, tab.jug.col].tipo != TipoCasilla.Muro)
                    {
                        tab.jug.fil++;
                    }
                    break;

                case 'd':
                    if (tab.jug.col + 1 > tab.cas.GetLength(1) && tab.cas[tab.jug.fil, tab.jug.col + 1].tipo != TipoCasilla.Muro)
                    {
                        tab.jug.col++;
                    }
                    break;
            }
            return dir;
        }
        static void ProcesaInput(Tablero tab, char dir, string movs)
        {
            switch (dir)
            {
                case 'l':
                case 'u':
                case 'r':
                case 'd':
                    Mueve(tab, dir);
                    break;

            }
        }

        static bool Terminado(Tablero tab)
        {
            bool flag = false;
            int i = 0, j = 0;

            while(i<tab.cas.GetLength(0) && !flag){
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
