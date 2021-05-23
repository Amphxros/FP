//Amparo Rubio Bellon

using System;
namespace PracticaFinal
{
    class Program
    {

        static string[] levels = {
        "Levels/level00.lvl",
        "Levels/level01.lvl"
       
        //añadir mas niveles aqui
        };


        static void Main(string[] args){
            Tablero t = new Tablero(levels[1]);
            t.Render();
            

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
