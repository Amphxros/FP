using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoja1
{
    class Program
    {
        
        const int N = 100; // tamaño de los arrays de monomios
        struct Monomio
        {
            public double coef;
            public int exp;
        }
        struct Polinomio
        {
            public Monomio[] mon; // array de monomios
            public int oc;
        }
       static Random rnd = new Random(); //para insertar randomente polinomios
        static void Main(string[] args)
        {
            Polinomio p = new Polinomio();
            p.mon = new Monomio[N];
            p.oc = 0;

            int d =N;

            for (int i = 0; i< d; i++)
            {
                Monomio m = new Monomio();
                m.coef = i;
                m.exp = rnd.Next(1,N);
                inserta(m, ref p);
            }  
           

            muestraPolinomio(p);

        }
        static void inserta(Monomio m, ref Polinomio p)
        {
            int i = 0;
            bool b = false;
            while(i<p.oc && !b && p.mon[i].exp<m.exp)
            {
                if (m.exp == p.mon[i].exp)
                {
                    b = true;
                }
                else
                {
                    i++;
                }
            }

            //si es mayor significa que no tenemos ningun polinomio de ese grado y lo añadimos
            if (i == p.oc)
            {
                p.oc++;
                p.mon[p.oc - 1] = m;


            }
            //si no se suma al monomio con el mismo exponente
            else if (b)
            {
                p.mon[i].coef += m.coef;
            }
            else
            {
                p.oc++;
                p.mon[p.oc - 1] = m;
                int j = p.oc - 1;

                while (j < 0 && j >= i)
                {
                    for (int k = 0; k < p.oc-1; k++)
                    {
                        if (p.mon[k].exp > p.mon[k + 1].exp)
                        {
                            swap(ref p.mon[j], ref p.mon[j - 1]);
                        }
                    }
                    j--;
                }

            }
           

        }


        static void swap(ref Monomio m1, ref Monomio m2)
        {
            Monomio tmp = m1;
            m1 = m2;
            m2 = tmp;
        }
        static int grado(Polinomio p)
        {
            return p.mon[p.oc-1].exp; // dado que el polinomio esta ordenado de mayor
        }
        static Polinomio suma(Polinomio p1, Polinomio p2)
        {
            Polinomio p= new Polinomio();
            p.oc = 0;
            p.mon = new Monomio[2*N];

            for(int i=0;i<p1.oc; i++)
            {
                inserta(p1.mon[i], ref p);
            }
            for (int j = 0; j < p2.oc; j++)
            {
                inserta(p2.mon[j], ref p);
            }



            return p;
        }
        
        static Polinomio multiplica(Polinomio p1, Polinomio p2)
        {
            Polinomio p= new Polinomio();
            p.oc = 0;
            p.mon = new Monomio[2 * N];

            for(int i = 0; i < p1.oc; i++)
            {
                for(int j = 0; j < p2.oc; j++)
                {
                    Monomio m;
                    m.coef = p.mon[i].coef * p2.mon[j].coef;
                    m.exp= p.mon[i].exp + p2.mon[j].exp;

                    inserta(m,ref p);
                }
            }

            return p;
        }

        static void divide(Polinomio p1, Polinomio p2, Polinomio c, Polinomio r)
        {

        }

        static void muestraPolinomio(Polinomio p)
        {
            for (int i = 0; i < p.oc; i++)
            {
               
                Console.Write(" +"+ p.mon[i].coef + "^" + p.mon[i].exp + " ");
            }
        }
    }
}
