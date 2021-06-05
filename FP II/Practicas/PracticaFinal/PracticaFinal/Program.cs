//Amparo Rubio Bellon

using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;

namespace PracticaFinal
{
    class Program
    {
        static string[] levels = {
        "Levels/level00.lvl",
        "Levels/level01.lvl",
        "Levels/level02.lvl",
        "Levels/level03.lvl",
        "Levels/level04.lvl"
       
        //añadir mas niveles aqui
        };
        enum State {None,Play,Load,Save,Quit };
        public static TextInfo Text = new CultureInfo("en-US", false).TextInfo;

        static int level = -1, maxLevel = 0; 
        static void Main(string[] args){

           
            bool exit = false;
            while (!exit)
            {
               
                State s = HandleState();

                switch (s)
                {
                    case State.Play:
                        //nueva partida
                        Console.Clear();
                        PlayState();
                        Tablero t = new Tablero(levels[level]);
                        RunningState(t);
                        break;

                    case State.Load:
                        Console.Clear();
                        //cargar partida existente
                        LoadState();
                        break;

                    case State.Save:
                        Console.SetCursorPosition(10, 14);
                        Console.Write("insert your username for saving the levels you passed> ");
                        string username = Console.ReadLine();
                        SaveUser(username);
                        break;
                    case State.Quit:
                        exit = true;
                        break;

                }

            }
        }

        static State HandleState()
        {
            State s = State.None;

            Console.Clear();
            Console.SetCursorPosition(10, 2);
            string t=("= BLOCK DESTROYER = ");
            ConsoleColor[] col = {ConsoleColor.Magenta, ConsoleColor.Yellow, ConsoleColor.Cyan };

            for(int i = 0; i < t.Length; i++)
            {
                Console.ForegroundColor = col[i % col.Length];
                Console.Write(t[i] + " ");
            }


            Console.SetCursorPosition(10, 7);
            Console.SetCursorPosition(10, 9);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Actions: Play, load, save and quit");

            do
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("what you want to do >  ");
                    Console.ForegroundColor = ConsoleColor.White;
                    string l = Console.ReadLine();

                    s = (State)Enum.Parse(typeof(State), Text.ToTitleCase(l));

                }
                catch
                {
                    Console.WriteLine("Please type 'play', 'load', 'save' or 'quit' ");
                }
            } while (s == State.None);

            return s;



        }

        static void PlayState()
        {
            Console.SetCursorPosition(10, 2);
            Console.WriteLine("= B L O C K  D E S T R O Y E R= ");
            
            //ponemos los niveles que se pueden superar
            for(int i = 0; i < levels.Length; i++)
            {
                Console.SetCursorPosition(10 + 3 * i, 4);
                if ( i <= maxLevel)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;

                }
                Console.Write("  ");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(10 + 3 * i, 5);

                Console.Write(i);
            }
            
            Console.SetCursorPosition(10, 7);
            Console.WriteLine("Controls:");
            Console.WriteLine("<- & -> to move the paddle");
            Console.WriteLine("P to pause the game");
            Console.SetCursorPosition(10, 14);
          
            do
            {
                Console.Write("which level do you want to play? > ");

                level = int.Parse(Console.ReadLine());

                if (level > maxLevel)
                {
                    Console.WriteLine("unavailable");
                }
            } while (level < 0 || level > maxLevel);
        
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
                    if (c != ' ' && c != 'p' && c!='s' && !moved)
                    {
                        moved = true;
                        t.IniciaBolas();
                    }
                    else
                    {
                        if (t.MueveBolas())
                        {
                            moved = false;
                        }
                        t.MuevePremios();
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
            else
            {
                Console.SetCursorPosition(10, 10);
                Console.WriteLine("G A M E W I N");
                Console.WriteLine();
                Console.WriteLine("Congratulations!! Score:" +  t.Score);
                Console.ForegroundColor = ConsoleColor.Yellow;
               
                if (level == maxLevel)
                    maxLevel++;

                Console.WriteLine("Press any key to go to the main menu"); ;
                Console.ReadKey();
            }

        }
        static void LoadState()
        {
            string username = "";
            Console.SetCursorPosition(10, 2);
            Console.WriteLine("= B L O C K  D E S T R O Y E R= ");

            Console.SetCursorPosition(10, 7);
            Console.WriteLine("Controls:");
            Console.WriteLine("<- & -> to move the paddle");
            Console.WriteLine("P to pause the game");

            Console.SetCursorPosition(10, 14);
            Console.Write("insert your username > ");
            username = Console.ReadLine();
            LoadUser(username);
            Console.WriteLine("loaded user " + username);

        }
        static void PauseState()
        {
            Console.SetCursorPosition(10, 10);
            Console.WriteLine("P A U S E D");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Press P to continue the game");

            Console.ForegroundColor = ConsoleColor
                .White;
        }
        static void SaveUser(string name)
        {
            StreamWriter w = new StreamWriter(name + ".usr");
            try
            {
                w.WriteLine(maxLevel + " "); // ultimo nivel desbloqueable               
                w.Close();
            }
            catch
            {
                throw new Exception("couldn't save" + name + ".usr");
            }
            
        }
        static void LoadUser(string name)
        {
            try
            {
                StreamReader r = new StreamReader(name + ".usr");
                if (File.Exists(name + ".usr"))
                {
                    string s = r.ReadLine();
                    string[] line =s.Split(' ');
                    maxLevel = int.Parse(line[0]);                   
                }
                else
                {
                    throw new Exception("Couldn't load username " + name);
                }
            }
            catch
            {
                Console.WriteLine("Couldn't load username " + name);
                 Console.ReadKey();
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
                    case "P": case "p": dir = 'p'; break;
                    case "S": case "s": dir = 's'; break;

                }
            }
            while (Console.KeyAvailable)
                (Console.ReadKey(false)).KeyChar.ToString();
        }
    }
}
