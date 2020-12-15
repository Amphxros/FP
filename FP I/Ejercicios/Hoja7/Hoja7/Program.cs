using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoja7
{
    class Program
    {
        static void Main(string[] args)
        {
        }


        static void Ejercicio1()
        {
            int length = PideInt("introoduce un num", 1, 10);

            int[] v = new int[length];
            for(int i = 0; i < v.Length; i++)
            {
                v[i] = PideInt(" ", -100, 100);
            }
            
            int[] vp = new int[length];
            for(int i = 0; i < vp.Length; i++)
            {
                vp[i] = PideInt(" ", -100, 100);
            }

            float[] w = new float[length];
            float sum = 0;
            for (int i = 0; i < w.Length; i++)
            {
                 w[i] = (v[i] * vp[i]);
                sum = sum + w[i];
            }
        }

        static void Ejercicio2()
        {
            int length = PideInt("introduce un num", 1, 10);

            int[] v = new int[length];
            for (int i = 0; i < v.Length; i++)
            {
                v[i] = PideInt(" ", -100, 100);
            }
            int[] pos = new int[length];
            int[] neg = new int[length];
            int ind = 0;
            int ind2 = 0;
            for (int i = 0; i < v.Length; i++)
            {
                if (v[i] > 0)
                {
                    pos[ind] = v[i];
                    ind++;
                }
                else
                {
                    neg[ind2] = v[i];
                    ind2++;
                }
               
            }

        }

        static int PideInt(string msg, int min, int max)
        {
            int i = min - 1;
            while (i < min || i > max)
            {
                Console.Write(msg);
                i = int.Parse(Console.ReadLine());
            }
            return i;
        }
    }
}
