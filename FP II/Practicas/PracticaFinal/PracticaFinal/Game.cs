
using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;

namespace PracticaFinal
{
    class Game
    {
        enum State { None, Play, Load, Save, Score, Top,Quit };
        public static TextInfo Text = new CultureInfo("en-US", false).TextInfo;
        string[] levels = {
        "Levels/level00.lvl",
        "Levels/level01.lvl",
        "Levels/level02.lvl",
        "Levels/level03.lvl",
        "Levels/level04.lvl"
       
        //añadir mas niveles aqui
        };

       public struct Score
       {
            public string username;
            public int score;

       }

        //nivel a jugar y ultimo nivel desloqueado
        int level = -1, maxLevel = 0;
        int[] scores;
        Score[] maxScores;
        string user;
        public Game() {
            level = -1;
            maxLevel = 0;
            scores = new int[levels.Length];
            maxScores = new Score[levels.Length];
            user = "";
            LoadMaxScores();
        }
        State HandleState()
        {
            State s = State.None;

            Console.Clear();
            Console.SetCursorPosition(10, 2);
            string t = ("= BLOCK DESTROYER = ");
            ConsoleColor[] col = { ConsoleColor.Magenta, ConsoleColor.Yellow, ConsoleColor.Cyan };

            for (int i = 0; i < t.Length; i++)
            {
                Console.ForegroundColor = col[i % col.Length];
                Console.Write(t[i] + " ");
            }
            Console.SetCursorPosition(10, 4);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Actions: ");
            for (int i = 1; i <=(int)State.Quit ; i++)
            {
                Console.SetCursorPosition(13, 2 + 2* (i+1));
                Console.ForegroundColor = col[i % col.Length];
                Console.Write((State)i);
                Console.ResetColor();
            }
        Console.WriteLine();
            Console.WriteLine();
        Console.CursorLeft = 14;
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
                    Console.WriteLine("Please type 'play', 'load', 'save', 'score', 'top' or 'quit' ");
                }
            } while (s == State.None);

            return s;

        }

        public void Run()
        {

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
                        Console.Write("insert your username for saving the levels you passed > ");
                        string username = Console.ReadLine();
                        SaveUser(username);
                        break;
                    case State.Score:
                        Console.Clear();
                        ScoreState();
                        break;
                    case State.Top:
                        Console.Clear();
                        TopState();

                        break;
                    case State.Quit:
                        exit = true;
                        break;

                }

            }
        }
        void TopState()
        {
            Console.SetCursorPosition(10, 2);
            string t = "MAX SCORES";
            ConsoleColor[] col = { ConsoleColor.Magenta, ConsoleColor.Yellow, ConsoleColor.Cyan };

            for (int i = 0; i < t.Length; i++)
            {
                Console.ForegroundColor = col[i % col.Length];
                Console.Write(t[i] + " ");
            }
            for (int i = 0; i < maxScores.Length; i++)
            {
                Console.SetCursorPosition(10, 4* (i+1));
                Console.ForegroundColor = col[i % col.Length];
                Console.WriteLine("Level " + i + ": " + maxScores[i].username + " " + maxScores[i].score);
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.ReadKey();
        }
        void PlayState()
        {
            Console.SetCursorPosition(10, 2);
            string t = ("= BLOCK DESTROYER = ");
            ConsoleColor[] col = { ConsoleColor.Magenta, ConsoleColor.Yellow, ConsoleColor.Cyan };

            for (int i = 0; i < t.Length; i++)
            {
                Console.ForegroundColor = col[i % col.Length];
                Console.Write(t[i] + " ");
            }


            //ponemos los niveles que se pueden superar
            for (int i = 0; i < levels.Length; i++)
                {
                    Console.SetCursorPosition(10 + 3 * i, 4);
                    if (i <= maxLevel)
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
                do{
                    Console.Write("which level do you want to play? > ");

                    level = int.Parse(Console.ReadLine());

                    if (level > maxLevel)
                    {
                        Console.WriteLine("unavailable");
                    }
                } while (level < 0 || level > maxLevel);

            }

       
        void RunningState(Tablero t)
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
                    if (c != ' ' && c != 'p' && c != 's' && !moved)
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
                Console.WriteLine("Congratulations!! Score:" + t.Score);
                Console.ForegroundColor = ConsoleColor.Yellow;
              
                if (level == maxLevel)
                    maxLevel++;

                Console.WriteLine("Press any key to go to the main menu"); ;
                Console.ReadKey();
            }
            if (scores[level] < t.Score)
            {
                scores[level] = t.Score;

            }
            if (maxScores[level].score < t.Score)
            {
                if (!String.IsNullOrEmpty(user))
                {
                    maxScores[level].username = user;

                }
                maxScores[level].score = t.Score;
                SaveScores();
            }


        }

        void LoadState()
            {
                string username = "";
                Console.SetCursorPosition(10, 2);
            string t = ("= BLOCK DESTROYER = ");
            ConsoleColor[] col = { ConsoleColor.Magenta, ConsoleColor.Yellow, ConsoleColor.Cyan };

            for (int i = 0; i < t.Length; i++)
            {
                Console.ForegroundColor = col[i % col.Length];
                Console.Write(t[i] + " ");
            }


            Console.SetCursorPosition(10, 7);
                Console.WriteLine("Controls:");
                Console.WriteLine("<- & -> to move the paddle");
                Console.WriteLine("P to pause the game");

                Console.SetCursorPosition(10, 14);
                Console.Write("insert your username > ");
                username = Console.ReadLine();
                LoadUser(username);
                user = username;
                Console.WriteLine("loaded user " + username);

            }

        void PauseState()
            {
            Console.SetCursorPosition(10, 10);   
            string t = "P A U S E D";
            ConsoleColor[] col = { ConsoleColor.Magenta, ConsoleColor.Yellow, ConsoleColor.Cyan };

            for (int i = 0; i < t.Length; i++)
            {
                Console.ForegroundColor = col[i % col.Length];
                Console.Write(t[i] + " ");
            }

            Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Press P to continue the game");

                Console.ForegroundColor = ConsoleColor
                    .White;
            }

        void ScoreState()
            {
                Console.SetCursorPosition(10, 2);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("S C O R E S");

                for (int i = 0; i < scores.Length; i++)
                {
                    Console.SetCursorPosition(14, 7 + 2 * i);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("level " +
                        i + ":  " + scores[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                }


                Console.ReadKey();
            }

        void SaveScores()
            {
                StreamWriter w = new StreamWriter("maxScores.score");

                for (int i = 0; i < maxScores.Length; i++)
                {
                    if (!String.IsNullOrEmpty(maxScores[i].username)) {
                        w.WriteLine(maxScores[i].username + " " + maxScores[i].score);
                    }
                }
                w.Close();

            }

        void SaveUser(string name)
            {
                StreamWriter w = new StreamWriter(name + ".usr");
                try
                {
                    w.WriteLine(maxLevel + " "); // ultimo nivel desbloqueable

                    for (int i = 0; i < scores.Length; i++)
                    {
                        w.WriteLine(scores[i]);
                    }

                    w.Close();
                }
                catch
                {
                    throw new Exception("couldn't save" + name + ".usr");
                }

            }

        void LoadUser(string name)
        {
            try
            {
                StreamReader r = new StreamReader(name + ".usr");
                if (File.Exists(name + ".usr"))
                {
                    string s = r.ReadLine();
                    string[] line = s.Split(' ');
                    maxLevel = int.Parse(line[0]);
                    int i = 0;
                    while (i < levels.Length && !r.EndOfStream)
                    {
                        scores[i] = int.Parse(r.ReadLine());
                        i++;
                    }

                }
                else
                {
                    throw new Exception("Couldn't load username " + name);
                }
                r.Close();
            }
            catch
            {
                Console.WriteLine("Couldn't load username " + name);
                Console.ReadKey();
            }
        }

        void LeeInput(ref char dir)
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

        private void LoadMaxScores()
            {
                try
                {
                    StreamReader s = new StreamReader("maxScores.score");
                    int i = 0;
                    while (i < scores.Length && !s.EndOfStream)
                    {
                        string l = s.ReadLine();
                        string[] line = l.Split(' ');
                        maxScores[i].username = line[0];
                        maxScores[i].score = int.Parse(line[1]);
                        i++;
                    }
                    s.Close();

                }
                catch
                {
                    StreamWriter w = new StreamWriter("maxScores.score");
                    for (int i = 0; i < scores.Length; i++)
                    {
                        w.WriteLine("NaN " + "0");
                    }
                    w.Close();
                }
            }

        }
}
