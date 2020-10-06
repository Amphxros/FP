using System;

namespace Hoja1
{
    class Program
    {
        static void Main(string[] args)
        {
           
        }

        static void Ejercicio1()
        {
            int c = int.Parse(Console.ReadLine());
            double f = 1.8 * c + 32;
            Console.WriteLine(f);
        }
        static void Ejercicio2_3()
        {
            int r = int.Parse(Console.ReadLine());
            double a = r * r * Math.PI;
            Console.WriteLine(r);
        }
        static void Ejercicio4()
        {
            Console.Write("x: ");
            int x = int.Parse(Console.ReadLine());
            Console.Write("y: ");
            int y = int.Parse(Console.ReadLine());

            Console.WriteLine("x: " + x + " " + "y: " + y);
            int aux = x;
            x = y;
            y = aux;
            Console.WriteLine("x: " + x + " " + "y: " + y);
        }
        static void Ejercicio5()
        {
            Console.Write("dividendo: ");
            int x = int.Parse(Console.ReadLine());
            Console.Write("divisor: ");
            int y = int.Parse(Console.ReadLine());

            int c = x / y;
            int r = x % y;

            Console.WriteLine("cociente: " + c);
            Console.WriteLine("resto: " + r);

        }

        static void Ejercicio6()
        {
            Console.Write("sec: ");
            int x = int.Parse(Console.ReadLine());

            int h=x/3600;
            int min = h / 60;
            int sec= min%60;
          
            Console.WriteLine("h: " + h + " min: "+ min + " sec: "+ sec);

        }
        static void Ejercicio7()
        {
            Console.Write("eu: ");
            int x = int.Parse(Console.ReadLine());
            Console.Write("cen: ");
            int y = int.Parse(Console.ReadLine());
            Console.Write("IVA: ");
            int iva = int.Parse(Console.ReadLine());

            x += x * iva / 100;
            y += y * iva / 100;

            Console.WriteLine("precio final: " + x + "," + y);

        }
    }
}

