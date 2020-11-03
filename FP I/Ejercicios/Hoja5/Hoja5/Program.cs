using System;

namespace Hoja5
{
    class Program
    {
        static void Main(string[] args)
        {
            Ejercicio4();
        }
        static void Ejercicio1()
        {
            int n = -1;
            do
            {
                Console.Write("Introduce un num: ");
                n = int.Parse(Console.ReadLine());
            } while (n < 0);


            bool flag = false;
            while (!flag && n > 0)
            {
               int aux = n % 10;
                n = n / 10;
                if (aux == 3)
                {
                    flag = true;
                }
            }
            Console.WriteLine(flag);
        }


        static void Ejercicio2()
        {
            int n = -1;
            do
            {
                Console.Write("Introduce un num: ");
                n = int.Parse(Console.ReadLine());
            } while (n < 0);

            int cont = 0;

            while (n > 0)
            {
                int aux = n % 10;
                n = n / 10;
                if (aux == 3)
                {
                    cont++;
                }

            }

            Console.WriteLine(cont);
        }

        static void Ejercicio3()
        {
            int n = -1; //num negativo para que entre en el bucle
            do
            {
                Console.Write("Introduce un num: ");
                n = int.Parse(Console.ReadLine());
            } while (n < 0);

            int fac = 1; //1 ya que es el elem. neutro del producto y ademas 0!=1
            for (int m = n; m > 1; m--)
            { //hasta 1 ya que 1 es el elemento neutro del producto
                fac = fac * m;
            }

            Console.WriteLine(fac);
        }

        static void Ejercicio4()
        {
            int n = -1; //num negativo para que entre en el bucle
            do
            {
                Console.Write("Introduce un num: ");
                n = int.Parse(Console.ReadLine());
            } while (n < 0);
            
            int rev = 0;
            int l = n.ToString().Length;
            int cont = 0;
            
            while (cont <=l/2 )
            {
                int izq =(int) (n / Math.Pow(10, l - cont) % Math.Pow(10, l - cont));
                int der =(int) (n % Math.Pow(10, cont));

                der =(int)( der * Math.Pow(10, l - cont));
                izq = (int)(izq * Math.Pow(10, cont));
                rev += der + izq;
                cont++;
            }
            Console.WriteLine(rev);

        }

        static void Ejercicio5()
        {
            int n = -1; //num negativo para que entre en el bucle
            do
            {
                Console.Write("Introduce un num: ");
                n = int.Parse(Console.ReadLine());
            } while (n < 0);

            int l = n.ToString().Length;
            int cont = 0;
            for(int i=0; i<l/2; i++)    //para un num aba
            {
                int a = (int)(n % Math.Pow(10,i)); // ab-a  nos quedamos con la primera a
                int b = (int)(n / Math.Pow(10,l-i)%(Math.Pow(10,l-i))); //a-bc  nos quedamos con la ultima a
                
                if (a == b) //sii a==c sumamos cont
                    cont++;

            }

            if (cont == l / 2) 
                Console.WriteLine("Capicua");

        }

        static void Ejercicio6()
        {

        }
    }
}
