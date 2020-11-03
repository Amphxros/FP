using System;

namespace FactorialA
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = -1;        //num negativo para que entre en el bucle
            do  
            {
                Console.Write("Introduce un num: ");
                n = int.Parse(Console.ReadLine());
            } while (n < 0);    //do-while para que al menos entre 1 vez

            int fac = 1;        //1 ya que es el elem. neutro del producto y ademas fac(0)=1
            for (int m = n; m > 1; m--)//hasta 1 ya que 1 es el elemento neutro del producto (A*1 =A)
            {               
                 fac = fac * m;
            }

            Console.WriteLine(fac);
        }
    }
}