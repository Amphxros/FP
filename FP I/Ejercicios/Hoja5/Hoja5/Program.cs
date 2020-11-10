using System;

namespace Hoja5
{
    class Program
    {
        static void Main(string[] args)
        {
            Ejercicio6();
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

            for (int i = 0; i < l / 2; i++)    //para un num aba
            {
                int a = (int)(n % Math.Pow(10, i)); // ab-a  nos quedamos con la primera c
                int b = (int)(n / Math.Pow(10, l - i) % (Math.Pow(10, l - i))); //a-bc  nos quedamos con la ultima a

                a = (int)(a*Math.Pow(10, l - i));
                b = (int)(b * Math.Pow(10, i-1));
              

                rev += a + b;
                n = (int)(n / Math.Pow(10, l - i));
                n = (int)(n / Math.Pow(10, i));
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
            for(int i=0; i < l/2; i++)    //para un num aba
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
            int n;
            do
            {
                Console.Write("Introduce un num: ");
                n = int.Parse(Console.ReadLine());
            } while (n < 0);

            // div entre 2
            int r2=n%10;
            bool b2= (r2 == 0 || r2 == 2 || r2 == 4 || r2 == 6 || r2 == 8);
            if (b2){
                Console.WriteLine("es divisible entre 2");
            }

            // div entre 3
            int d3=n; 
            int r3=0;

            while (d3>9)
            {
                int s= 0;
                while (d3 != 0)
                {
                    s += d3 % 10;
                    d3 /= 10;
                }

                d3 = s;
            }
            bool b3 = d3 % 3 == 0; // o si es 0,3,6 o 9
            if (b3)
                Console.WriteLine("es div entre 3");

            // div entre 5
            
            int r5=r2;
            bool b5 = n == 5 || n == 0;
            if (b5)
            {
                Console.Write("es div entre 5");
            }
            
            //div 6
            if(b3&&b2)
            {
                Console.WriteLine("es div entre 6");
            }

            //div 10
            if(b2 && b5) //tambien se puede comprobar que n%10==0
            {
                Console.WriteLine("es div entre 10");
            }

        

        }
    }
}
