using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructsPolinomicos
{
    public struct Monomio
    {
        public int exp_;
        public double base_;
    }
    public struct Polinomio
    {
        public Monomio[] mon;
        public int tam;
    }

    class Program
    {
        const int N =3;
        static void Main(string[] args)
        {
            Polinomio p;
            leePolinomio(out p);
            MuestraPol(p);

        }

        static void Inserta(ref Polinomio p)
        {
            Monomio m;
            leeMonomio(out m);
            int i = 0;
            while(i<p.tam && p.mon[i].exp_ != m.exp_)
            {
                i++;
            }

        }

        static void leePolinomio(out Polinomio p)
        {
            p.mon = new Monomio[N];
            Console.Write("Número de monomios: ");
            p.tam = int.Parse(Console.ReadLine());
            Console.WriteLine("Introduce monomios");
            for (int i = 0; i < p.tam; i++)
                leeMonomio(out p.mon[i]);
        }

        static void leeMonomio(out Monomio m)
        {
            Console.Write("Coeficiente: ");
            m.base_ = double.Parse(Console.ReadLine());
            Console.Write("Exponente: ");
            m.exp_ = int.Parse(Console.ReadLine());
        }
        static void MuestraPol(Polinomio pol)
        {
            for(int i=0;i<pol.tam; i++)
            {
                Console.Write(pol.mon[i].base_ + "x^" + pol.mon[i].exp_+ " ");
            }
        }
        
    }
}
