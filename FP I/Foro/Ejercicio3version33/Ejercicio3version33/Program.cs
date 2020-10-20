using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio3version33
{
    class Program
    {
        /// Determinar si un número de 4 cifras y tiene 2 cifras consecutivas que son 3.
        static void Main(string[] args)
        {
            Console.Write("Introduce un num ");
            int n = int.Parse(Console.ReadLine()); // tenemos un numero de 4 cifras abcd

            int i = n % 100; // nos quedamos con los numeros cd
            bool a = i == 33; //comprobamos que cd es 33

            n = n / 10; //descartamos la d quedandonos con abc

            int j = n % 100; // cogemos los dos ultimos, es decir bc
            bool c = j == 33;// volvemos a comprobar que es 33
            n = n / 10; // y descartamos c quedando ab


            bool b = n == 33;// y por ultimo comprobamos que ab es 33

            Console.WriteLine( a || b || c); //la solucion final es cualquiera de estos 3 casos
        }
    }
}
