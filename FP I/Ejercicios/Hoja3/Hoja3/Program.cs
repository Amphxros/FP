using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoja3
{
    class Program
    {
        static void Main(string[] args)
        {
            Ejercicio32();
        }

        //algun 3
        static void Ejercicio1()
        {
            Console.Write("Introduce un num ");
            int n = int.Parse(Console.ReadLine());

            int i = n % 10;
            n = n / 10;
            bool a = i == 3;
           
            i = n % 10;
            n = n / 10;
            bool b = i == 3;
            
            i = n % 10;
            n = n / 10;
            bool c = i == 3;
            
            i = n % 10;
            n = n / 10;
            bool d = i == 3;
            Console.WriteLine(a||b||c||d);
        }
        
        //2 tres
        static void Ejercicio2()
        {
            Console.Write("Introduce un num ");
            int n = int.Parse(Console.ReadLine());

            int i = n % 10;
            n = n / 10;
            bool b = i == 3;
            Console.WriteLine(b);

            i = n % 10;
            n = n / 10;
            b = i == 3;
            Console.WriteLine(b);

            i = n % 10;
            n = n / 10;
            b = i == 3;
            Console.WriteLine(b);

            i = n % 10;
            n = n / 10;
            b = i == 3;
            Console.WriteLine(b);

        }

        //3 2 veces consecutivas
        static void Ejercicio3()
        {
            Console.Write("Introduce un num ");
            int n = int.Parse(Console.ReadLine());

            int i = n % 10;
            n = n / 10;

            int j = n % 10;
            n = n / 10;
            bool c = j == 3 && i == 3;


            int k = n % 10;
            n = n / 10;
            bool b = (k == 3) && (j == 3);

            int l = n % 10;
            n = n / 10;
            bool a = (l == 3) && (k == 3);
            Console.WriteLine(a||b||c);

        }

        //3 2 veces consecutivas version 2
        static void Ejercicio32()
        {
            Console.Write("Introduce un num ");
            int n = int.Parse(Console.ReadLine()); //abcd

            int i = n % 100; //cd
            bool a = i == 33;
            
            n = n / 10; //abc
         
            int j = n % 100; //bc
            bool c = i == 33;
            n = n / 10; //ab


            bool b = n == 33; 

           Console.WriteLine(a|| b||c);

        }
        //capicua
        static void Ejercicio4()
        {
            Console.Write("Introduce un num ");
            int n = int.Parse(Console.ReadLine());  //wxyz

            int i = n % 10;                         //wxy
            n = n / 10;

            int j = n % 10;                         //wx
            n = n / 10;
         
            int k = n % 10;                         //w
            n = n / 10;
            bool b = (k == j);

            bool a = (i == n);
            Console.WriteLine(a && b); //comprobamos que el 1 y el 4 son iguales y el 2 y 3 

        }

        //binario a decimal
        static void Ejercicio5()
        {
            Console.Write(" introduce un num ");

            int n = int.Parse(Console.ReadLine());

            // troceamos el num en 4 cifras distintas (aprovechamos n al final)
            int i0 = n % 10;
            n /= 10;
           
            int i1 = n % 10;
            n /= 10;
            
            int i2 = n % 10;
            n /= 10;

            //sumamos todo multiplicando cada bit por 2 elevado a la potencia en funcion de su posicion
            n = (i0 * 1) + (i1 * 2 ) +( i2 * 4) + (n * 8);          // 1 = 2^0, 2=2^1, 4=2^2 ...

            Console.WriteLine(n);
        }

        static void Ejercicio6()
        {
            Console.Write(" introduce un num ");
            double n = double.Parse(Console.ReadLine());

            n = n * 100; // quitamos decimales

            int cent=(int)(n%100); //centimos
            int eu = (int)(n / 100); //eu

            int b500 = eu / 500;
            eu %= 500;

            int b200 = eu / 200;
            eu %= 200;

            int b100 = eu / 100;
            eu %= 100;

            int b50 = eu / 50;
            eu %= 50;
            
            int b20 = eu / 20;
            eu %= 20;

            int b10 = eu / 10;
            eu %= 10;

            int b5 = eu / 5;
            eu %= 5;
            
            int b2 = eu / 2;
            eu %= 2;





        }
        static void Ejercicio7()
        {

        }
        static void Ejercicio8()
        {

        }
    }
}
