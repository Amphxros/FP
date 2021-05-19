//Amparo Rubio Bellon

using System;
namespace PracticaFinal
{
    class Program
    {
        static void Main(string[] args)
        {
            Tablero t = new Tablero(20, 10);
            t.Render();
            bool exit_= false;

            while (!exit_)
            {
                char c = ' ';
                LeeInput(ref c);
                exit_ = c == 'q';
                
                t.MuevePala(c);
                t.Render();
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
                    case "RightArrow": dir = 'r'; break;

                    case "Q": case "q": dir = 'q'; break;

                }
            }
            while (Console.KeyAvailable)
                (Console.ReadKey(false)).KeyChar.ToString();
        }
    }
}
