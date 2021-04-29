//Amparo Rubio Bellon

using System;

namespace Practica2
{
    class Program
    {
       static string[] levels = {
        "Levels/level00.dat",
        "Levels/level01.dat",
        "Levels/level02.dat",
        "Levels/level03.dat",
        "Levels/level04.dat",
        "Levels/level05.dat",
        "Levels/level06.dat",
        "Levels/level07.dat",
        "Levels/level08.dat",
        "Levels/level09.dat"

        };

        static void Main(string[] args)
        {
            int num = -1;
            do
            {
                Console.Clear();
                Console.Write("nivel a jugar: ");
                try
                {
                    num = int.Parse(Console.ReadLine());
                }
                catch(Exception e)
                {
                    
                }
            } while (num < 0 || num > levels.Length);


            Tablero t = new Tablero(levels[num]);

             t.Dibuja();
             int lap = 20; // retardo para bucle ppal
             char c =' ';
             bool exit = false;
          while (!exit && !t.Captura() && !t.finNivel())
            {
                // input de usuario
                LeeInput(ref c);
                // procesamiento del input
                if (c != ' ' && t.CambiaDir(c)) { 
                    t.MuevePacman();
                }
                 exit = c == 'q';
//                 // IA de los fantasmas: TODO
//   
                 t.MueveFantasmas(lap);
//                 // renderizado
                    t.Dibuja();
//                 // retardo
                 System.Threading.Thread.Sleep(lap);
//            
             }
        }

        static void LeeInput(ref char dir){
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
