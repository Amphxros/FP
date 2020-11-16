using System;

namespace CambiodeBase // Ejercicio 8 Hoja 5
{
    class Program
    {
        static void Main(string[] args)
        {

            int n, b; //n=numero en base 10, b= base a la que se va a pasar
           
            do /// bucle para pedir el num (el do while es para que al menos entre 1 vez)
            {
                Console.Write("Introduce un num: ");
                n = int.Parse(Console.ReadLine());
            } while (n < 0);
           
            do/// bucle para pedir la base (el do while es para que al menos entre 1 vez)
            {
                Console.Write("Introduce una base: ");
                b = int.Parse(Console.ReadLine());
            } while (b < 0); 
            
            string sol =""; //cadena donde almacenaremos la solucion
            
            while (n > 0)
            {
                int r = n % b; //pillamos el resto
                n = n/ b;  //dividimos
              
                sol = r + sol ; // lo colocamos en su potencia conveniente
          
            }
            
            Console.WriteLine(sol);
        }
    }
}