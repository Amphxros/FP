/// Amparo Rubio Bellón
using System;

/// Practica 1 FP
namespace Practica1
{
    class Program
    {
        const int ANCHO=10, ALTO=20,DELTA=100; 
        static void Main()
        {
            Random rnd = new Random(); // generador de aleatorios
           
            bool exit = false, paused=false;    //booleanos para quitar el juego y pausar el juego
            
            int jugX=ANCHO/2, jugY=ALTO-1;  //posicion del jugador
            int enemX=ANCHO/2, enemY=2; //pos enemigo
           
            int balaX=0, balaY=-1; //proyectil jugador
            int bombaX=-1, bombaY=-1;  //proyectil enemigo

            while (!exit) {     // bucle ppal del juego
                // Input de usuario
                if (Console.KeyAvailable)
                {
                    string dir = (Console.ReadKey(true)).KeyChar.ToString();
                    // procesamiento del input de usuario
                    if (dir == "a")     //movimiento a la izquierda
                    {
                        if (jugX> 0 && !paused)
                            jugX--;
                    }
                    else if (dir == "d")//movimiento a la derecha
                    {
                        if (jugX < ANCHO -1 && !paused)
                            jugX++;
                    }
                    else if (dir == "w" && !paused)    //movimiento hacia arriba
                    {
                        if(jugY>0)
                            jugY--;
                    }
                    else if (dir == "s" && !paused)    //movimiento hacia abajo
                    {
                        if (jugY < ALTO-1)
                            jugY++;
                       
                    }
                    else if (dir == "1" && !paused)    //disparo de una bala
                    {
                        if (balaY < 0)
                        {
                            balaX = jugX;
                            balaY = jugY - 1;
                        }
                    }
                    else if (dir == "p")    //pausado del juego
                    {
                        paused = !paused;
                    }
                    else if (dir == "q")    //quitar juego
                    {
                        exit = true;
                    }

                    //vaciado del buffer
                    while (Console.KeyAvailable)
                        (Console.ReadKey(false)).KeyChar.ToString();

                }

                if (!paused)      //si el juego no esta pausado se moveran los objetos y se re-renderizan (como no se mueve nada no es necesario refrescar la pantalla)
                {
                    // Lógica del juego: movimiento del enemigo, bomba y bala
                    
                    //movimiento del enemigo
                    int aleatorio = enemX + rnd.Next(-1, 2);
                    if (aleatorio >= 0 && aleatorio < ANCHO - 1)     // si está dentro de los limites se asigna la posX
                    {
                        enemX = aleatorio;
                    }

                    aleatorio = enemY + rnd.Next(-1, 2);
                    if (aleatorio < ALTO / 2 && aleatorio > 1)     // si está dentro de los limites se asigna la posY
                    {
                        enemY = aleatorio;
                    }
                    //si no hay bala damos una oprtunidad a disparar
                    if (bombaY >= ALTO || bombaY < 0)
                    {
                        aleatorio = rnd.Next(-1, 2);        //se genera un num entre -1 y 1 y si es 0 el enemigo dispara
                        if (aleatorio == 0)
                        {
                            bombaX = enemX;
                            bombaY = enemY + 1;
                        }
                    }
                    //actualizamos la bala si hay una bala
                    if (balaY >= 0)
                    {
                        balaY--;
                    }
                    //actualizamos la bomba si hay una bomba
                    if (bombaY < ALTO)
                    {
                        bombaY++;
                    }

                    // Control de colisiones
                    if (jugX == bombaX && jugY == bombaY)  //colision jugador-bomba
                    {
                        exit = true;
                    }
                    else if (enemX == balaX && enemY == balaY) //colision enemigo-proyectil
                    {
                        exit = true;
                    }
                    else if (bombaX == balaX && bombaY-1 == balaY && (bombaY<ALTO && balaY>0)) //colision bomba-proyectil
                    {
                        balaY = 0;
                        bombaY = ALTO;
                    }



                    // Renderizado gráfico
                    Console.Clear();
                    //rend borde
                    for (int i = 0; i < ALTO; i++)   //muro horizontal
                    {
                        Console.SetCursorPosition(2 * ANCHO, i);
                        Console.Write("||");
                    }

                    for (int i = 0; i <= ANCHO; i++)  //muro vertical
                    {
                        Console.SetCursorPosition(2 * i, ALTO);
                        Console.Write("--");
                    }
                    //rend jugador
                    Console.SetCursorPosition(2 * jugX, jugY);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("O");
                    Console.ForegroundColor = ConsoleColor.White;

                    //rend enemigo
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(2 * enemX, enemY);
                    Console.Write("~~");
                    Console.ForegroundColor = ConsoleColor.White;

                    //bomba del jugador
                    if (balaY > 0)          //si hay una bala en pantalla se renderiza
                    {
                        Console.SetCursorPosition(2 * balaX, balaY);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("^^");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    //bomba enem
                    if (bombaY < ALTO && bombaX>0)      //si hay una bomba lanzada se renderiza
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.SetCursorPosition(2 * bombaX, bombaY);
                        Console.Write("xx");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.CursorVisible = false;
                }
                // retardo
                System.Threading.Thread.Sleep(DELTA);
            } // fin del bucle
            


        }
    }
}
