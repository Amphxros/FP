using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoja8
{
    class Program
    {
        static Random rnd = new Random();
        
        //equivalente a una clase pero como aun no hemos dado clases
        struct MatrizCuadrada
        {
            private int n; //tamaño de las filas y columnas
            public int[,] nums; //array bidim

            //constructora
            public MatrizCuadrada(int m)
            {
                n = m;
                nums = new int[n, n];
            }
            //rellena la matriz con el input de consola
            public void FillUp()
            {
                int m = nums.GetLength(0);
                int p = nums.GetLength(1);

                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < p; j++)
                    {
                        Console.Write(" ");
                        int aux = int.Parse(Console.ReadLine());
                        nums[i, j] = aux;

                    }
                    Console.WriteLine();
                }
            }
            //llena la matriz de numeros random
            public void FillUpRandom(int min, int max)
            {
                int m = nums.GetLength(0);
                int p = nums.GetLength(1);

                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < p; j++)
                    {
                        int aux = rnd.Next(min, max);
                        nums[i, j] = aux;

                    }
                    Console.WriteLine();
                }
            }
            //matriz nula
            public void FillUpEmpty()
            {
                int m = nums.GetLength(0);
                int p = nums.GetLength(1);

                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < p; j++)
                    {
                        int aux = 0;
                        nums[i, j] = aux;

                    }
                    Console.WriteLine();
                }
            }
            //escribe en consola la matriz
            public void WriteMat()
            {
                int m = nums.GetLength(0);
                int p = nums.GetLength(1);

                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < p; j++)
                    {
                        Console.Write(nums[i, j] + " ");
                    } 
                    Console.WriteLine();
                }
            }

        }


        struct MatrizNxM
        {
            private int n, m; //tamaño de filas y columnas
            public int[,] nums; //array bidim 

            //constructora
            public MatrizNxM(int n_,int m_)
            {
                n = n_;
                m = m_;

                nums = new int[n, m];
            }
            //rellena con el input 
            public void FillUp()
            {
                int m = nums.GetLength(0);
                int p = nums.GetLength(1);

                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < p; j++)
                    {
                        Console.Write(" ");
                        int aux = int.Parse(Console.ReadLine());
                        nums[i, j] = aux;

                    }
                    Console.WriteLine();
                }
            }
            //rellena con numeros random
            public void FillUpRandom(int min, int max)
            {
                int m = nums.GetLength(0);
                int p = nums.GetLength(1);

                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < p; j++)
                    {
                        int aux = rnd.Next(min, max);
                        nums[i, j] = aux;

                    }
                    Console.WriteLine();
                }
            }
            //rellena con 0
            public void FillUpEmpty()
            {
                int m = nums.GetLength(0);
                int p = nums.GetLength(1);

                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < p; j++)
                    {
                        int aux = 0;
                        nums[i, j] = aux;

                    }
                    Console.WriteLine();
                }
            }
            //escribe por consola la matriz
            public void WriteMat()
            {
                int m = nums.GetLength(0);
                int p = nums.GetLength(1);

                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < p; j++)
                    {
                        Console.Write(nums[i, j] + " ");
                    }
                    Console.WriteLine();
                }
            }
        }
        
        static void Main(string[] args)
        {
            Ejercicio2();
        }
        
        //EJERCICIO 1
        static void Ejercicio1()
        {

            MatrizCuadrada m = new MatrizCuadrada(3);
            m.FillUpRandom(0,5);
            m.WriteMat();

            //para probarlo uso este while
            while (!EsSim(m))
            {
                m.FillUpRandom(0,5);
                m.WriteMat();
            }

            Console.WriteLine("es sim");
            m.WriteMat();
        }

        static bool EsSim(MatrizCuadrada m)
        {
            int cont = 0;
            for(int i=0; i < m.nums.GetLength(0); i++)
            {
                for(int j = 0; j < m.nums.GetLength(1); j++)
                {
                    if (m.nums[i, j] == m.nums[j, i])
                    {
                        cont++;
                    }
                }
            }
            
            return cont == m.nums.GetLength(1)* m.nums.GetLength(0);
        }

        //FIN EJERCICIO 1

        //EJERCICIO 2
        static void Ejercicio2()
        {
            MatrizNxM m = new MatrizNxM(3, 4);
            m.FillUpRandom(1, 100);

            m.WriteMat();
            int[] sumFilas = new int[m.nums.GetLength(0)];
            int[] sumCols = new int[m.nums.GetLength(1)];
            Console.WriteLine();
            for(int i = 0; i < sumFilas.Length; i++)
            {
                for(int j=0;j<m.nums.GetLength(1); j++)
                {
                    sumFilas[i] += m.nums[i, j];
                }

                Console.Write(sumFilas[i] + " ");
            }
            Console.WriteLine();
            for(int i = 0; i < sumCols.Length; i++)
            {
                for(int j=0;j<m.nums.GetLength(0); j++)
                {
                    sumCols[i] += m.nums[j,i];
                }

                Console.Write(sumCols[i] + " ");
            }
        }

        //FIN EJERCICIO 2



    }
}
