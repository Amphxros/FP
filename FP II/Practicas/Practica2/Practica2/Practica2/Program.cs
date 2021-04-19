//Amparo Rubio Bellon

using System;

namespace Practica2
{
    class Program
    {
        static void Main(string[] args)
        {
            Tablero t = new Tablero("Levels/level00.dat");

            t.Dibuja();
            int lap = 2; // retardo para bucle ppal
            char c =' ';
            bool exit = false;
            while (!exit)
            {
                // input de usuario
                LeeInput(ref c);
                // procesamiento del input
                if (c != ' ' && t.CambiaDir(c)) { 
                    c = ' ';
                }
                exit = c == 'q';
                t.MuevePacman();
                // IA de los fantasmas: TODO


                // rederizado
                t.Dibuja();
                // retardo
                System.Threading.Thread.Sleep(10*lap);

            }
            }

            static void LeeInput(ref char dir)
        {
            if (Console.KeyAvailable)
            {
                string tecla = Console.ReadKey(true).Key.ToString();
                switch (tecla)
                {
                    case "LeftArrow": dir = 'l'; break;
                    case "DownArrow": dir = 'd'; break;
                    case "UpArrow": dir = 'u'; break;
                    case "RightArrow": dir = 'r'; break;

                    case "Q": case "q": dir = 'q'; break;
                    
                }
            }
            while (Console.KeyAvailable)
                (Console.ReadKey(false)).KeyChar.ToString();
        }
    }
}
