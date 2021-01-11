// Amparo Rubio Bellon
using System;

namespace Practica2
{
    class Program
    {
        //variables globales
        static Random rnd = new Random(); // generador de números aleatorios
   
        //constantes
        const int ANCHO = 30, ALTO = 18, MAX_BOMBAS = 10, COMBUSTIBLE = 15;
        const int RETARDO = 150;
        const bool DEBUG = false;

        const int ALTURA_EDIFICIO = 5; //altura del edificio en modo depuracion


        static void Main(string[] args)
        {
            int ancho=ANCHO; //ancho de la pantalla de juego , nº de edificios y de bombas
            int avionX, avionY; //coordenadas del avion

            int puntos; //puntuacion
            int combustible;//combustible

            if (DEBUG)
            {
                do
                {
                    Console.Write("Introduce ancho: ");
                    ancho = int.Parse(Console.ReadLine());
                } while (ancho <= 0 || ancho >= ANCHO);
            }
           
            int[] edificios = new int[ancho]; //array de edificios
            int[] bombas = new int[ancho];  //array de bombas
                      
            bool exit_ = false; //booleano de salida
            bool paused = false;//booleano de pausado
            bool col_ = false; //booleano de colision

            Inicializa(edificios, bombas, out avionX, out avionY, out puntos, out combustible);
            Renderiza(edificios, bombas, avionX, avionY, puntos, combustible);

            //bucle principal
            while (!exit_ &&!col_ && FinPartida(edificios)){
                
                char c = leeInput();
            
                if (c == 'p'){
                paused = !paused;
                }

                if (!paused)
                {
                    if (c == 'q')
                    {
                        exit_ = true;
                    }
                    else
                    {
                        //todo lo relacionado con actualizar el juego
                        if (c == 'b')
                        {
                            LanzamientoBomba(bombas, avionX, avionY);
                        }
                       
                        MueveAvion(c, ref avionX, ref avionY, ancho, ref combustible);
                        ColisionBombas(edificios, bombas, ref puntos);
                        MueveBombas(bombas);
                        
                        //renderizado
                        Renderiza(edificios, bombas, avionX, avionY, puntos, combustible);

                        col_ = ColisionAvion(edificios, avionX, avionY); //o impacto avion-edificio
                        System.Threading.Thread.Sleep(RETARDO);

                    }
                }
            }
            
            if (col_) //si la salida ha sido por un impacto dibujamos el punto de impacto
            {
                Console.SetCursorPosition(2 * avionX, ALTO - avionY);
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Write("><");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("boom");
                Console.SetCursorPosition(0, ALTO + 7);
            }
            else if (!FinPartida(edificios))
            {
                Console.SetCursorPosition(0, ALTO + 7);
                Console.WriteLine("Has eliminado todos los edificios");
            }

        }

        //inicializa el contenido de edificios y bombas, posiciona el avion y inicia el combustible
        static void Inicializa(int[] edificios, int[] bombas, out int avionX, out int avionY, out int puntos, out int combustible)
        {
            for (int i = 0; i < edificios.Length; i++)
            {
                if (DEBUG)
                {
                    edificios[i] = ALTURA_EDIFICIO; //altura fija dada por una constante
                }
                else
                {
                    edificios[i] = rnd.Next(1, ALTO - 3); //altura random 
                }
                bombas[i] = -1;
            }
            avionX = edificios.Length-1;
            avionY = ALTO-1;
            puntos = 0;
            combustible = COMBUSTIBLE;
        }

        //renderizado del juego, dibuja edificios, bombas si hay en pantalla, el avion y la informacion de puntos y combustible. 
        //Si esta en depuracion añade informacion como el nº de bombas en pantalla, la pos del avion y la altura de cada edificio
        static void Renderiza(int[] edificios, int[] bombas, int avionX, int avionY, int puntos, int combustible)
        {
            Console.Clear();
            
            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor)); //array de colores
            int indColor = 0; //color actual
         
            for (int i = 0; i < edificios.Length; i++)
            {
                indColor++;
                
                if (indColor >= colors.Length) //si supera la dimension del array volvemos al principio del array(1 para evitar el negro ya que no se veria)
                {
                    indColor = 1;
                }
                else if (indColor == 4 ||indColor==11) //si es igual a rojo oscuro(bombas) o cyan (avion descartamos ese color)
                {
                    indColor=indColor++ % colors.Length;
                }
               
                Console.BackgroundColor = colors[indColor]; 
               

                for (int j = 0; j < edificios[i]; j++)//dibuja los edificios
                {
                    Console.SetCursorPosition(2 * i, ALTO - j);
                    Console.Write("  ");
                }

                Console.BackgroundColor = colors[0]; //si estamos en depuracion dibuja la altura de cada edificio
                if (DEBUG)
                {
                    Console.SetCursorPosition(2 * i, ALTO + 1);
                    Console.Write(" "+edificios[i]);
                }

                if (bombas[i] > 0) //puesto que el tamaño del array de bombay= tam del array de edificios se puede dibujar en el mismo bucle
                {
                    Console.SetCursorPosition(2 * i, ALTO-bombas[i]);
                    Console.BackgroundColor = colors[4]; //pillamos el rojo
                    Console.Write("**");
                }
            }

            //dibujamos el avion
            Console.SetCursorPosition(2 * avionX, ALTO-avionY);
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Write("<-");

            Console.BackgroundColor = ConsoleColor.Black;
            
            //dibujamos los puntos
            Console.SetCursorPosition(0, ALTO + 3);
            Console.Write("puntos " + puntos);

            //dibujamos el combustible
            Console.SetCursorPosition(0, ALTO + 5);
            Console.Write("combustible " + combustible + " ");

            //colores del combustible
            Console.BackgroundColor = ConsoleColor.Green;
            if (combustible < 2 * COMBUSTIBLE / 3)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
            }
            else if (combustible < COMBUSTIBLE / 3)
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }

            for (int i = 0; i < combustible; i++)
            {
                Console.Write(" ");
            }

            //informacion adicional para la depuracion
            Console.BackgroundColor = ConsoleColor.Black;
            if (DEBUG) { 
             
                Console.WriteLine(avionX + " " + avionY + " ");
                Console.WriteLine("bombas en pantalla: "+ CuentaBombas(bombas) + " num maximo de bombas: "+ MAX_BOMBAS);
            }
            
            Console.WriteLine();
        }
        
        //devuelve un char en funcion del input
        static char leeInput()
        {
            char d = ' ';
            if (Console.KeyAvailable)
            {
                string tecla = Console.ReadKey(true).Key.ToString();
                tecla = tecla.ToUpper(); // conversion a mayúsculas
                switch (tecla)
                {
                    case "A": d = 'a'; break;
                    case "S": d = 's'; break;
                    case "W": d = 'w'; break;
                    case "D": d = 'd'; break;
                    case "B": d = 'b'; break;
                    case "P": d = 'p'; break;
                    case "Q": d = 'q'; break;
                }
            }
            while (Console.KeyAvailable)
                (Console.ReadKey(false)).KeyChar.ToString();
            return d;

        }

        //devuelve true si aún quedan edificios en pie
        static bool FinPartida(int[] edificios)
        {
            int cont = 0;
            while (cont < edificios.Length && edificios[cont] <= 0 )
            {
                cont++;
            }
            
            return cont < edificios.Length;
        }

        //Lanza una bomba en la posicion avionX a la altura avionY si es posible (si no ha llegado al maximo de bombas en pantalla y no hay una bomba en avionX)
        static void LanzamientoBomba(int[] bombas, int avionX, int avionY)
        {
            if (CuentaBombas(bombas) < MAX_BOMBAS && bombas[avionX] < 0)
            {
                bombas[avionX] = avionY;
            }
        }
        
        //devuelve el numero de bombas en pantalla
        static int CuentaBombas(int[] bombas)
        {
            int cont = 0;
            for (int i = 0; i < bombas.Length; i++)
            {
                if (bombas[i] > 0)
                    cont++;
            }
            return cont;
        }
        
        //mueve las bombas que esten en pantalla
        static void MueveBombas(int[] bombas)
        {
            for (int i = 0; i < bombas.Length; i++)
            {
                if (bombas[i] > 0)
                {
                    bombas[i]--;
                }
            }

        }

        //mueve el avion acorde a su input y resta combustible cuando es necesario
        static void MueveAvion(char c, ref int avionX, ref int avionY, int ancho, ref int combustible)
        {
            if (combustible > 0)
            {
                switch (c)
                {
                    case 'a':
                        avionX -= 1;
                        combustible--;
                        break;
                    case 'w':
                        if (avionY < ALTO)
                        {
                            avionY++;
                            combustible--;
                        }
                        break;
                    case 's':
                        avionY--;
                        combustible--;
                        break;
                    case 'd':
                        avionX += 1;
                        combustible--;
                        break;
                }
            }
            if (avionX - 1 < 0)
            {
                avionX = ancho-1;
                avionY--;
            }
            else
            {
                avionX--;
            }
        }

        //Comprueba la colision de las bombas y añade puntos en funcion de ello
        static void ColisionBombas(int[] edificios, int[] bombas, ref int puntos)
        {
            for (int i = 0; i < edificios.Length; i++)
            {
                if (bombas[i]>=0 && bombas[i]<= edificios[i])
                {
                    Impacto(edificios, i, ref puntos);
                    edificios[i]--;
                    puntos = puntos + 1;

                    bombas[i] = -1;   
                }
            }
        }

        // recorre el array de edificios desde un punto i por la izq y derecha y resta la altura y añade puntos en funcion de si tienen la misma altura que edificios[i]
        static void Impacto(int [] edificios, int i, ref int puntos)
        {

            int indIzq = i - 1;
            int indDer = i + 1;

            //recorrido por la izquierda
            while (indIzq >= 0 && edificios[i] == edificios[indIzq])
            {
                edificios[indIzq]--;
                indIzq--;
                puntos += 1;
            }

            //recorrido por la derecha
            while (indDer < edificios.Length && edificios[i] == edificios[indDer])
            {
                edificios[indDer]--;
                indDer++;
                puntos += 1;
            }
        }

        //devuelve true si el avion ha impactado con un edificio
        static bool ColisionAvion(int []edificios, int avionX, int avionY)
        {
            return edificios[avionX] > avionY;
        }

    }
}
