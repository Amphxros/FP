using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmosDeOrdenacion
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static void burbuja(int[] v)
        {
            int n = v.Length;
            // damos n vueltas, en cada una v[i] queda en su sitio
            for (int i = 0; i < n; i++)
            {
                // inv: v [0.. i−1] ordenado (en su posición nal )
                // recorremos con j=n−1 hasta i+1 comparando v[j−1] y v[j]
                for (int j = n - 1; j < 0; j--)
                {
                    if (v[j - 1] > v[j])
                    {
                        swap(ref v[j - 1], ref v[j]);
                    }
                }
            }
        }
        static void insercion(int[] v)
        {
            int n = v.Length; // número de eltos del vector
                              // v[0] ya está ordenado, para cada uno desde 1 hasta n:
            for (int i = 1; i < n; i++)
            { // inv: v [0.. i−1] está ordenado
              // insertamos v[i ] ordenadamente en el subvector v[0..i−1]
                int tmp = v[i]; // guardamos v[i]
                int j = i-1;
                // desplazamos eltos a la dcha abriendo hueco para v[i]
                while ((j >= 0) && (v[j] > tmp))
                {
                    v[j + 1] = v[j];
                    j--;
                }
                v[j + 1] = tmp;
            }
        }
       
        static void seleccion(int[] v)
        {
            int n = v.Length; // num de eltos del vector
                              // en cada posición i=0..n−1 ponemos el menor de v[i..n−1]
                              // vamos hasta i=n−2 porque al nal v[n−1] ya está en su sitio
            for (int i = 0; i < n-1; i++) {
                // inv: el subarray 0.. i−1 está ordenado
                // buscamos el menor en v[i..n)
                int min = i;
                for (int j = i + 1; j < n; j++)
                    if (v[j] < v[min]) // con menor estricto es estable
                        min = j;
                // ponemos el menor en v[i] y v[i ] en la pos del menor
                swap(ref v[i], ref v[min]);
            }
        }

        static void swap(ref int x, ref int y)
        {
            int tmp = x;
            x = y;
            y = tmp;
        }

    }
}
