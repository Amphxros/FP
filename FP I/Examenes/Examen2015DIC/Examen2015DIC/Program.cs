using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen2015DIC
{
    class Program
    {
      static Random rnd = new Random();
        static void Main(string[] args)
        {
            int ancho_ = pideInt(1, 10, "Ancho de la pasarela: ");
            int largo_ = pideInt(1, 100, "Largo de la pasarela: ");
            int pos= ancho_/2;
            int pasos=0;

            bool choff = false;

            draw(ancho_, pos, choff);

            while ((pasos < largo_) && !choff)
            {
                int j = rnd.Next(-1, 2);
                pos += j;
                

                if(pos < 0 || pos > ancho_)
                {
                    choff = true;
             
                }
                draw(ancho_, pos, choff);


                pasos++;
            }


        }

        //equivalente a pideEntero
        static int pideInt(int min,int max,string s)
        {
            int i = min - 1;
            do
            {
                Console.Write(s);
                i = int.Parse(Console.ReadLine());

            } while (i < min || i > max);

            return i;
        }

        static void draw(int ancho, int pos, bool choff)
        {
            if (choff == false)
            {
                for (int i = 0; i <= ancho; i++)
                {
                   Console.SetCursorPosition(2 * i, Console.CursorTop);
                   if (i == pos)
                   {
                       Console.Write("ºº");
                   }
                   else
                   {
                       Console.Write(" -");
                   }

                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("choff");
            }
        }

    }
}
