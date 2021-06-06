using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaFinal
{
    class Tablero
    {
        //Variables
        Paddle player;
        
        ListaBolas balls;
        ListaBloques bloques;
        ListaPremios premios;
        Random rnd = new Random();
        
        int width, height, tamBloques, priceProbability, points;

        ConsoleColor[] col = {ConsoleColor.Cyan, ConsoleColor.Yellow, ConsoleColor.Magenta };
        bool gameOver = false;
        

        public int Score
        {
            get { return points; }
        }

        //metodos

        //constructora
        public Tablero(string file)
        {
            StreamReader read = new StreamReader(file);
            try
            {
                if (File.Exists(file))
                {
                    char[,] tempTab= new char[200,200];

                    int h_ = 0;
                    int w_ = 0;
                    int tamPaddle = 0;
                    tamBloques = 0;
                    //Read the whole file and save it temporaly
                    while (!read.EndOfStream)
                    {
                        string s= read.ReadLine();
                       

                        
                        w_ = s.Length;
                        for (int i = 0; i < w_; i++)
                        {
                            tempTab[i, h_] = s[i];
                            if (tempTab[i, h_] == 'A')
                            {
                                tamBloques++;
                            }
                            else if (tempTab[i, h_] == 'T')
                            {
                                tamPaddle++;
                            }

                           
                        }
                        h_++;
                    }
                    width = w_; 
                    height = h_;
                    //transfer the temporaly to the real tab

                    balls = new ListaBolas();
                    bloques = new ListaBloques();
                    premios = new ListaPremios();
                    int nBloques=0;
                    priceProbability = 10;
                    points = 0;
                    bool paddleCreated = false;
                    for(int i=0 ; i<tempTab.GetLength(0); i++)
                    {
                        for (int j = 0; j < tempTab.GetLength(1); j++)
                        {
                            switch (tempTab[i, j])
                            {
                                case 'A':
                                    Vector2D p = new Vector2D(i, j + 1);
                                    Bloque bl = new Bloque(p, 1, col[j % col.Length], 4);
                                    bloques.InsertaFin(bl);
                                    nBloques++;
                                    break;

                                case 'B':

                                    Vector2D b = new Vector2D(i, j + 1);
                                    balls.InsertaFin(new Ball(b));
                                    break;
                                case 'T':
                                    if (!paddleCreated)
                                    {
                                        Vector2D t = new Vector2D(i, j + 1);
                                        player = new Paddle(t, 3, tamPaddle);
                                        
                                        paddleCreated = true;
                                    }
                                    break;
                                case 'P':
                                    Vector2D pos = new Vector2D(i, j);
                                    Reward r = new Reward(pos, new Vector2D(0, -1), 2, RewardID.AddWidth);
                                    premios.InsertaFin(r);
                                    break;
                                case 'O':
                                    Vector2D ps = new Vector2D(i, j);
                                    Reward re = new Reward(ps, new Vector2D(0, -1), 2, RewardID.AddBalls);
                                    premios.InsertaFin(re);
                                    break;
                                case 'I':
                                    Vector2D p_ = new Vector2D(i, j);
                                    Reward re_ = new Reward(p_, new Vector2D(0, -1), 2, RewardID.NextLevel);
                                    premios.InsertaFin(re_);
                                    break;

                            }
                        }
                    }
                   
                }
                else
                {
                    throw new Exception("The file " + file + " doesn't exist");
                }

            }
            catch(Exception e)
            {

            }


        }
        
        //si no hemos empezado la partida iniciamos las bolas
        public void IniciaBolas()
        {
            for(int i=0; i<balls.NumElems; i++)
            {
                balls.getnEsimo(i).Init();
            }
        }
       
        //renderiza los elementos del tablero
        public void Render()
        {
            Console.Clear();
            Console.SetCursorPosition(width +5, 0);
            Console.Write("Lifes: ");
            for(int i=0;i< player.Vida; i++)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Write("  ");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" ");
            }

            Console.SetCursorPosition(width + 5, 2);
            Console.Write("Score: " + points);

            for (int i=0;i<bloques.NumElems; i++)
            {
                bloques.getnEsimo(i).Render();
            }
            Console.BackgroundColor = ConsoleColor.White;
            
            for(int i=0; i< balls.NumElems; i++)
            {
                balls.getnEsimo(i).Render();
            }

            for (int i = 0; i < premios.NumElems; i++)
            {
                premios.getnEsimo(i).Render();
            }


            player.Render();

            Console.WriteLine();

        }
        
        //mueve al jugador en base al input
        public void MuevePala(char c)
        {
            player.handleInput(c);

            int sig = player.Position.getX() + player.Direction.getX();
            if (sig > 0 && sig < width)
            {
                player.Update();
            }

        } 

        //mueve las bolas y comprueba sus colisiones
        public bool MueveBolas()
        {
            bool rst = false;

            for(int i=0;i<balls.NumElems; i++)
            {
                //colisiones entre bola y player
                if (balls.getnEsimo(i).Position.getY() >= player.Position.getY()-1)
                {
                    if (balls.getnEsimo(i).Position.getX() >= player.Position.getX()-1 &&
                        balls.getnEsimo(i).Position.getX() <= player.Position.getX() + player.Width)
                    {
                        balls.getnEsimo(i).ChangeY();
                       
                    }
                }

                balls.getnEsimo(i).MoveBall();
                for (int j = 0; j < bloques.NumElems; j++)
                {
                    if (balls.getnEsimo(i).Position.getX()  >=bloques.getnEsimo(j).Position.getX() && balls.getnEsimo(i).Position.getX() <= bloques.getnEsimo(j).Position.getX() + bloques.getnEsimo(j).Width
                        && balls.getnEsimo(i).Position.getY() == bloques.getnEsimo(j).Position.getY())
                    {
                        
                        balls.getnEsimo(i).ChangeY();
                        if (bloques.getnEsimo(j).OnCollision())
                        {
                            tamBloques--;
                            int p = rnd.Next(0, 100);
                            if (p < priceProbability)
                            {
                                int rewardID = rnd.Next(0, (int)RewardID.lastID);
                                Reward r = new Reward(bloques.getnEsimo(j).Position, new Vector2D(0, 1), 2, (RewardID)rewardID);
                                premios.InsertaFin(r);
                            }
                            bloques.borraElto(bloques.getnEsimo(j));
                            points += 10;
                          
                        }
                    }
               
                }

                
                //colisiones entre los limites de los lados
                if (balls.getnEsimo(i).Position.getX() + balls.getnEsimo(i).Direction.getX() < 0 ||
                    balls.getnEsimo(i).Position.getX() + balls.getnEsimo(i).Direction.getX() >width)
                {
                    balls.getnEsimo(i).ChangeX();
                   
                }
                
                else if (balls.getnEsimo(i).Position.getY() + balls.getnEsimo(i).Direction.getY() <= 0)
                {
                    balls.getnEsimo(i).ChangeY();
                    
                }

                if(balls.getnEsimo(i).Position.getY() + balls.getnEsimo(i).Direction.getY() > height)
                {
                    balls.borraElto(balls.getnEsimo(i));
                    if (balls.NumElems <= 0)
                    {
                        player.Vida--;
                        if (player.isDead())
                        {
                            gameOver = true;
                        }
                        else
                        {
                            Ball b = new Ball(new Vector2D(width / 2, 3* height / 4));
                            balls.InsertaFin(b);
                            player.Position.setX(width / 2 - player.Width / 2);
                            rst = true;
                        }
                        
                    }
                }
               
            }

            return rst;

        }

        public void MuevePremios()
        {
            for(int i = 0; i < premios.NumElems; i++)
            {
                bool hasTodelete = false;
               
               premios.getnEsimo(i).Move();

                //colisiones entre bola y player
                if (premios.getnEsimo(i).Position.getY() + premios.getnEsimo(i).Direction.getY() >= player.Position.getY())
                {
                    if (premios.getnEsimo(i).Position.getX() >= player.Position.getX() &&
                        premios.getnEsimo(i).Position.getX() <= player.Position.getX() + player.Width)
                    {
                        points += 50;
                        switch (premios.getnEsimo(i).ID)
                        {
                            case RewardID.AddBalls:

                                Ball b = new Ball(new Vector2D(width / 2, height / 2));
                                balls.InsertaFin(b);
                                b.Init();
                                break;
                            case RewardID.AddWidth:
                                player.Width += 2;
                                
                                break;
                            case RewardID.LessWidth:
                                if (player.Width > 4)
                                {
                                    player.Width -= 1;
                                
                                }
                                break;
                            case RewardID.LessBlocks:
                                int r = rnd.Next(0, priceProbability);
                                for(int a = 0; a < r; a++)
                                {
                                    int bl = rnd.Next(0, bloques.NumElems);
                                    bloques.borraElto(bloques.getnEsimo(bl));
                                }
                                break;
                            case RewardID.MorePrices:
                                priceProbability += 2;
                                break;
                            case RewardID.NextLevel:
                                tamBloques = 0;
                                break;
                        }

                        hasTodelete = true;
                    }
                }

                if (premios.getnEsimo(i).Position.getY() > height)
                {
                    hasTodelete = true;
                }

                if (hasTodelete)
                {
                    premios.borraElto(premios.getnEsimo(i));
                }

            }
        }

       

     
        //es true si no quedan bolas en el tablero
        public bool GameOver()
        {
            return gameOver;
        }

        // es true si no quedan bloques en el tablero
        public bool GameWin()
        {
            return tamBloques <= 0;
        }

    }

}
