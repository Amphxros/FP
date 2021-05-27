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
        
        Paddle player;
        ListaBolas balls;
        ListaBloques bloques;
        Random rnd = new Random();
        int width, height;
        int tamBloques;
        ConsoleColor[] col = { ConsoleColor.Red, ConsoleColor.Cyan, ConsoleColor.Yellow, ConsoleColor.Magenta, ConsoleColor.White,ConsoleColor.Green };
        bool gameOver = false;
        public Tablero(int width, int height)
        {
          
        }
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

                            Console.SetCursorPosition(i, h_);
                            Console.Write(tempTab[i, h_].ToString());
                        }
                        h_++;
                    }
                    width = w_; 
                    height = h_;
                    //transfer the temporaly to the real tab

                    balls = new ListaBolas();
                    bloques = new ListaBloques();
                    int nBloques=0;
                    bool paddleCreated = false;
                    for(int i=0 ; i<tempTab.GetLength(0); i++)
                    {
                        for (int j = 0; j < tempTab.GetLength(1); j++)
                        {
                            switch (tempTab[i, j])
                            {
                                case 'A':
                                    Vector2D p = new Vector2D(i, j);
                                    Bloque bl = new Bloque(p, 1, col[j % col.Length], 4);
                                    bloques.InsertaFin(bl);
                                    nBloques++;
                                    break;
                                case 'B':

                                    Vector2D b = new Vector2D(i, j );
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
                Console.WriteLine(e.Message);
            }


        }

        public void Render()
        {
            Console.Clear();
            for(int i=0;i<bloques.NumElems; i++)
            {
                bloques.getnEsimo(i).Render();
            }
            Console.BackgroundColor = ConsoleColor.White;
            
            for(int i=0; i< balls.NumElems; i++)
            {
                balls.getnEsimo(i).Render();
            }

            player.Render();
            Console.WriteLine();

        }
        public void MuevePala(char c)
        {
            player.handleInput(c);

            int sig = player.Position.getX() + player.Direction.getX();
            if (sig > 0 && sig < width)
            {
                player.Update();
            }

        } 

        public void IniciaBolas()
        {
            for(int i=0; i<balls.NumElems; i++)
            {
                balls.getnEsimo(i).Init();
            }
        }

        public void MueveBolas()
        {

            for(int i=0;i<balls.NumElems; i++)
            {
                //colisiones entre bola y player
                if (balls.getnEsimo(i).Position.getY() + balls.getnEsimo(i).Direction.getY() >= player.Position.getY())
                {
                    if (balls.getnEsimo(i).Position.getX() >= player.Position.getX() &&
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
                            bloques.borraElto(bloques.getnEsimo(j));
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

                //
                if(balls.getnEsimo(i).Position.getY() + balls.getnEsimo(i).Direction.getY() > height)
                {
                    balls.borraElto(balls.getnEsimo(i));
                    if (balls.NumElems <= 0)
                    {
                        gameOver = true;
                    }
                }
               
            }
        }

        public bool GameOver()
        {
            return gameOver;
        }

        public bool GameWin()
        {
            return tamBloques <= 0;
        }


    }

}
