using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoja2
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static void Ejercicio1()
        {
            int h, min, sec;
            Console.Write("Tiempo en horas");
            h = int.Parse(Console.ReadLine());
            
            Console.Write("Tiempo en min");
            min = int.Parse(Console.ReadLine());
            
            Console.Write("Tiempo en sec");
            sec = int.Parse(Console.ReadLine());

            sec += (min * 60) + (h * 3600);

            Console.WriteLine(sec);
        }

        static void Ejercicio2()
        {
            int edad1, edad2, edad3;
            Console.Write("edad1");
            edad1 = int.Parse(Console.ReadLine());

            Console.Write("edad2");
            edad2 = int.Parse(Console.ReadLine());

            Console.Write("edad3");
            edad3 = int.Parse(Console.ReadLine());

            double med = (edad1 + edad2 + edad3) / 3;

            Console.WriteLine(med);
        }

        static void Ejercicio3()
        {

        }
        static void Ejercicio4()
        {

        }
        static void Ejercicio5()
        {

        }
        static void Ejercicio6()
        {

        }
        static void Ejercicio7()
        {

        }

    }
}
