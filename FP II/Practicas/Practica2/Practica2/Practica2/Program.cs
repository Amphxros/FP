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
        //añadir mas niveles aqui
        };

        static void Main(string[] args)
        {
            int num = -1;
            
           //pedimos el nivel a jugar
           do{
                Console.Clear();
                Console.Write("nivel a jugar: ");

                num = int.Parse(Console.ReadLine());
                
            } while (num < 0 || num >= levels.Length);

             int lap = 20; // retardo para bucle ppal
             char c =' '; //char del input
             bool exit = false; //booleano para quitar e juego con el input


            Tablero t = new Tablero(levels[num]); //creamos tablero
             t.Dibuja(); //dibujamos el inicio del tablero

            // bucle principal
            while (!exit && !t.Captura() && !t.finNivel())
            {
                // input de usuario
                LeeInput(ref c);
                // procesamiento del input
                if (c != ' ' && t.CambiaDir(c)) { 
                    t.MuevePacman();
                }
                 exit = c == 'q';
                 // IA de los fantasmas   
                 t.MueveFantasmas(lap);
                 // renderizado
                    t.Dibuja();
                 // retardo
                 System.Threading.Thread.Sleep(lap);
            
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
