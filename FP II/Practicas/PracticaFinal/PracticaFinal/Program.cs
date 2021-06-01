﻿//Amparo Rubio Bellon

using System;
using System.Text.RegularExpressions;
using System.Globalization;
namespace PracticaFinal
{
    class Program
    {


        static string[] levels = {
        "Levels/level00.lvl",
        "Levels/level01.lvl"
       
        //añadir mas niveles aqui
        };
        private static State s;
        public static TextInfo Text = new CultureInfo("en-US", false).TextInfo;

        static void Main(string[] args){

            bool exit = false;
            while (!exit)
            {
               
                int id=0,level = -1, minLevel = 0, maxLevel = 1;
                MenuState( out id,out level, minLevel, maxLevel);
                exit = id == (int)(State.Quit);
                if (id==(int)State.Play)
                {
                    Tablero t = new Tablero(levels[level]);
                    RunningState(t);
                }
                else if(id==(int)State.Load)
                {
                    //load a file

                }
            }
        }


        enum State {None,Play, Load,Quit };
        static void MenuState(out int id, out int level, int levelMin, int levelMax)
        {
            level = -1;
            State s= State.None;
            id = 0;
             Console.Clear();
                    Console.SetCursorPosition(10, 2);
                    Console.WriteLine("= B L O C K  D E S T R O Y E R= ");
                    Console.SetCursorPosition(10, 7);
                   

            do
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Actions: Play, load, quit");
                    Console.SetCursorPosition(10, 9);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("what you want to do >  ");
                    Console.ForegroundColor = ConsoleColor.White;
                    string l = Console.ReadLine();
                    
                    s = (State)Enum.Parse(typeof(State), Text.ToTitleCase(l));
                    
                }
                catch
                {

                }
                } while (s == State.None);

           

            switch (s)
            {
                case State.Play:
                    id = (int)State.Play;
                    do
                    {
                        try
                        {
                            Console.Clear();
                            Console.SetCursorPosition(10, 2);
                            Console.WriteLine("= B L O C K  D E S T R O Y E R= ");
                            Console.SetCursorPosition(10, 7);
                            Console.WriteLine("Controls:");
                            Console.WriteLine("<- & -> to move the paddle");
                            Console.WriteLine("P to pause the game");
                            Console.SetCursorPosition(10, 14);
                            Console.Write("which level do you want to play? > ");

                            level = int.Parse(Console.ReadLine());


                            if (level > levelMax)
                            {
                                Console.WriteLine("unavailable");
                            }



                        }
                        catch
                        {

                        }

                    } while (level < levelMin || level > levelMax);

                    break;

                case State.Load:

                    id = (int)State.Load;
                    //cargar partida existente
                    break;
                case State.Quit:
                    id = (int)State.Quit;
                    break;

            }

           


        }

        static void RunningState(Tablero t)
        {
            t.Render();

            bool moved = false;
            bool goToMenu = false;
            bool pause = false;
            while (!goToMenu && !t.GameOver())
            {
                char c = ' ';
                LeeInput(ref c);
                if (c == 'p')
                {
                    pause = !pause;
                }

                else if (c == 'q')
                {
                    goToMenu = true;
                }

                if (!pause)
                {
                    if (c != ' ' && c != 'p' && !moved)
                    {
                        t.IniciaBolas();
                        moved = true;
                    }
                    else
                    {

                        t.MueveBolas();
                        t.MuevePala(c);
                        t.Render();

                    }
                }
                else
                {
                    PauseState();
                   
                }
            }

            if (t.GameOver())
            {
                Console.SetCursorPosition(10, 10);
                Console.WriteLine("G A M E O V E R");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Press any key to go to the main menu"); ;
                Console.ReadKey();
            }

        }


        static void PauseState()
        {
            Console.SetCursorPosition(10, 10);
            Console.WriteLine("P A U S E D");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Press P to continue the game");

            Console.ForegroundColor = ConsoleColor.White;
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
                    case "P": case "p": dir = 'p'; break;

                }
            }
            while (Console.KeyAvailable)
                (Console.ReadKey(false)).KeyChar.ToString();
        }
    }
}
