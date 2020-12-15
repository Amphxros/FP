//Amparo Rubio Bellon
using System;

namespace Practica2
{
    class Program
    {
        //variables globales
        static Random rnd = new Random(); // generador de números aleatorios
        const int ANCHO = 30, ALTO = 18, MAX_BOMBAS = 40, COMBUSTIBLE = 15;
        const int RETARDO = 150;
        const bool DEBUG = true;
        static void Main(string[] args)
        {
            int[] edificios = new int[ANCHO];
            int[] bombas = new int[ANCHO];
            int avionX, avionY;
            int puntos;
            int combustible;
            bool exit_ = false;
            Inicializa(edificios, bombas, out avionX, out avionY, out puntos, out combustible);
            Renderiza(edificios, bombas, avionX, avionY, puntos, combustible);
            while (!exit_)
            {
                char c = leeInput();

                exit_ = FinPartida(edificios); //o impacto avion-edificio

                Renderiza(edificios, bombas, avionX, avionY, puntos, combustible);
                System.Threading.Thread.Sleep(RETARDO);
            }
        }

        static void Inicializa(int[] edificios, int[] bombas, out int avionX, out int avionY, out int puntos, out int combustible)
        {
            for (int i = 0; i < edificios.Length; i++)
                edificios[i] = rnd.Next(1, ALTO - 3);

            for (int i = 0; i < bombas.Length; i++)
                bombas[i] = -1;
            avionX = ANCHO;
            avionY = 0;
            puntos = 0;
            combustible = COMBUSTIBLE;
        }

        static void Renderiza(int[] edificios, int[] bombas, int avionX, int avionY, int puntos, int combustible)
        {
            Console.Clear();
            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));

            for (int i = 0; i < edificios.Length; i++)
            {
                for (int j = 0; j < edificios[i]; j++)
                {
                    Console.SetCursorPosition(2 * i, ALTO - j);
                    Console.BackgroundColor = colors[i % colors.Length]; //hay que quitar el negro de alguna forma
                    if (i % colors.Length == 0) //esto quita el negro de la paleta de colores(lo cambia por otro color de la paleta)
                    {
                        Console.BackgroundColor = colors[(i + 5) % colors.Length];

                    }
                    Console.Write("  ");
                }
            }

            for (int i = 0; i < bombas.Length; i++)
            {
                if (bombas[i] >= 0)
                {
                    Console.SetCursorPosition(2 * i, bombas[i]);
                    Console.BackgroundColor = colors[4]; //pillamos el rojo
                    Console.Write("**");
                }
            }

            Console.SetCursorPosition(2 * avionX, avionY);
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Write("<-");

            Console.BackgroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(0, ALTO + 3);
            Console.Write("puntos " + puntos);

            Console.SetCursorPosition(0, ALTO + 5);
            Console.Write("combustible " + combustible + " ");

            //comprobar el combustible para el color
            Console.BackgroundColor = ConsoleColor.Green;
            if (combustible < 2 * COMBUSTIBLE / 3)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
            }
            else if (combustible < COMBUSTIBLE / 3)
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }

            for (int i = 0; i < combustible; i++)
            {
                Console.Write(" ");
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }

        static char leeInput()
        {
            char d = ' ';
            if (Console.KeyAvailable)
            {
                string tecla = Console.ReadKey(true).Key.ToString();
                tecla = tecla.ToUpper(); // conversion a mayúsculas
                switch (tecla)
                {
                    case "A": d = 'a'; break;
                    case "S": d = 's'; break;
                    case "W": d = 'w'; break;
                    case "D": d = 'd'; break;
                    case "B": d = 'b'; break;
                    case "P": d = 'p'; break;
                    case "Q": d = 'q'; break;
                }
            }
            while (Console.KeyAvailable) Console.ReadKey().Key.ToString();
            return d;

        }

        static bool FinPartida(int[] edificios)
        {
            int cont = 0;
            while (edificios[cont] <= 0)
                cont++;

            return cont <= edificios.Length;
        }


    }
}
