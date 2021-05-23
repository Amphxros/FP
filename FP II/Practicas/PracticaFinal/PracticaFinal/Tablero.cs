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
        Bloque[] bloques;
        Paddle player;
        Lista balls;
        Random rnd = new Random();

        ConsoleColor[] col = { ConsoleColor.Red, ConsoleColor.Cyan, ConsoleColor.Yellow, ConsoleColor.Magenta, ConsoleColor.White,ConsoleColor.Green };
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
                    int tamBloques = 0;
                    int tamPaddle = 0;
                    
                    //Read the whole file and save it temporaly
                    while (!read.EndOfStream)
                    {
                        string s= read.ReadLine();
                       

                        string[] line = s.Split(' '); //asi quitamos os espacios de la matriz
                        w_ = line.Length;
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

                    //transfer the temporaly to the real tab

                    bloques = new Bloque[tamBloques];
                    balls = new Lista();
                    int nBloques=0;
                    bool paddleCreated = false;
                    for(int i=0;i<tempTab.GetLength(0); i++)
                    {

                        for (int j = 0; j < tempTab.GetLength(1); j++)
                        {
                            switch (tempTab[i, j])
                            {
                                case 'A':
                                    Vector2D p = new Vector2D(i, j + 1);
                                    bloques[nBloques] = new Bloque(p, 1, ConsoleColor.Gray);
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
                                        player = new Paddle(t, 3);
                                        player.Width = tamPaddle;
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
            for(int i=0;i<bloques.Length; i++)
            {
                bloques[i].Render();
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

            if (player.Position.getX() + player.Direction.getX() > 0 && player.Position.getX() + player.Direction.getX() - 1.5* player.Width < bloques.GetLength(0))
            {
                player.Update();
            }

        } 

    }

}
