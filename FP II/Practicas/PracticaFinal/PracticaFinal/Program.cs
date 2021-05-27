//Amparo Rubio Bellon

using System;
namespace PracticaFinal
{
    class Program
    {

        enum State {MENU, RUNNING, PAUSE, GAMEWIN, GAMEOVER };

        static string[] levels = {
        "Levels/level00.lvl",
        "Levels/level01.lvl"
       
        //añadir mas niveles aqui
        };
        

        
        static void Main(string[] args){

            Tablero t = new Tablero(levels[0]);
            t.Render();

            bool exit = false;
            bool moved = false;
            while (!exit && !t.GameOver())
            {
                char c = ' ';
                LeeInput(ref c);
                if (c != ' ' && !moved) {
                    t.IniciaBolas();
                    moved = true;
                }
                else { 
                if (c == 'q')
                {
                    exit = true;
                }
                else
                {
                    t.MueveBolas();
                    t.MuevePala(c);
                    t.Render();
                }
                }
            }
            

        }

        static void MenuState()
        {

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
