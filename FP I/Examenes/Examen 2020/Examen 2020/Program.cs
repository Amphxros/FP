///Amparo Rubio Bellon

//Ampar Rubio Bellon
using System;

namespace Examen_2020
{
    class Program
    {
        static void Main(string[] args)
        {

            int[,] tab; // tablero con contenido de casillas
            tab = new int[5, 5]
            {{ 0,-1, 0,-1, 5},
            {-1, 3,-1, 0,-1},
            { 6,-1,-1,-1, 0},
            {-1, 0,-1, 6,-1},
            { 5,-1, 4,-1, 0}};
            int fil = 0, col = 0; // posición de la casilla activa
            int[] pend; // dígitos pendientes de colocar
            pend = new int[6] { 4, 5, 6, 4, 5, 6 };

            bool exit_ = false;
            dibuja(tab, pend, fil, col);

            while (!exit_)
            {
                ProcesaInput(tab, leeInput(), ref fil, ref col, pend);
                dibuja(tab, pend, fil, col);
                System.Threading.Thread.Sleep(150);
                FinJuego(pend, exit_);
            }

        }

        static void dibuja(int[,] tab, int[] pend, int fil, int col)
        {
            Console.Clear();
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    Console.SetCursorPosition(2 * i, j);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    switch (tab[i, j])
                    {
                        case -1:
                            Console.Write(" ");
                            break;
                        case 0:

                            Console.Write("·");
                            break;
                        default:

                            if (tab[i, j] >= 10)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write(tab[i, j] % 10);
                            }
                            else
                            {
                                Console.Write(tab[i, j]);
                            }
                            break;
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.SetCursorPosition(0, tab.GetLength(1) + 2);
            Console.Write("Pends: ");
            for (int i = 0; i < pend.Length; i++)
            {
                Console.SetCursorPosition(2 * (i + 3), tab.GetLength(1) + 2);
                Console.Write(pend[i] + " ");
            }
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(2 * fil, col);
        }

        static void MueveCursor(int[,] tab, char c, ref int fil, ref int col)
        {
            switch (c)
            {
                case 'u':
                    if (col > 0)
                    {
                        col--;
                    }
                    break;
                case 'l':
                    if (fil > 0)
                    {
                        fil--;
                    }
                    break;
                case 'd':
                    if (col < tab.GetLength(0) - 1)
                    {
                        col++;
                    }
                    break;
                case 'r':
                    if (fil < tab.GetLength(1) - 1)
                    {
                        fil++;
                    }
                    break;
            }

        }
        static bool NumViable(int[,] tab, int fil, int col, int val)
        {
            int l = col - 1;

            while (l > 0 &&tab[fil, l] != val )
            {
                l--;
            }

            int r = col + 1;
            while (r < tab.GetLength(0) && tab[fil, r] != val )
            {
                r++;
            }

            int u = fil - 1;
            while ( u > 0 && tab[u, col] != val )
            {
                u--;
            }

            int d = fil + 1;
            while (d < tab.GetLength(1) && tab[d, col] != val)
            {
                d++;
            }

            return (l <= 0 && r >= tab.GetLength(0) &&
                u <= 0 && d >= tab.GetLength(1));
          
        }

        static void PonNumero(int[,] tab, int fil, int col, int[] pend, int val)
        {

            if (NumViable(tab, fil, col, val) && tab[fil, col] == 0)
            {
                tab[fil, col] = val + 10;


                int aux = 0;
                while (aux < pend.Length && pend[aux] != val)
                {
                    aux++;
                }
                if (aux < pend.Length)
                    pend[aux] = 0;
            }
        }

        static void FinJuego(int[] pend, bool fin)
        {
            int p = 0;
            while (pend[p] == 0 && p < pend.Length)
                p++;

            if (p >= pend.Length)
                fin = true;
            else
                fin = false;
        }
        static void QuitaNumero(int[,] tab, int fil, int col, int[] pend)
        {

            int n = tab[fil, col];
            //si podemos quitarlo
            if (n > 10)
            {
                tab[fil, col] = 0;

                int aux = 0;
                while (pend[aux] != 0 && aux < pend.Length)
                {
                    aux++;
                }
                pend[aux] = n %10;

            }
        }
        static void ProcesaInput(int[,] tab, char c, ref int fil, ref int col, int[] pend)
        {
            if (c == 's')
            {
                QuitaNumero(tab, fil, col, pend);
            }
            else if (c == 'l' || c == 'u' || c == 'r' || c == 'd')
            {
                MueveCursor(tab, c, ref fil, ref col);
            }
            else if (c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9')
            {
                int v = (int)(c - '0');
                PonNumero(tab, fil, col, pend, v);
            }

        }
        static char leeInput()
        {
            char d = ' ';
            if (Console.KeyAvailable)
            {
                string tecla = Console.ReadKey(true).Key.ToString();
                switch (tecla)
                {
                    case "LeftArrow": d = 'l'; break;
                    case "UpArrow": d = 'u'; break;
                    case "RightArrow": d = 'r'; break;
                    case "DownArrow": d = 'd'; break;
                    case "Spacebar":
                    case "S": d = 's'; break; // borrar num
                    case "Escape":
                    case "Q": d = 'q'; break; // salir
                    case "P": d = 'p'; break;
                    // lectura de dígito
                    default:
                        if (tecla.Length == 2 && tecla[0] == 'D' && tecla[1] >= '0' && tecla[1] <= '9') d = tecla[1];
                        else d = ' ';
                        break;
                }
                while (Console.KeyAvailable)
                    Console.ReadKey().Key.ToString();
            }
            return d;

        }
    }
}
