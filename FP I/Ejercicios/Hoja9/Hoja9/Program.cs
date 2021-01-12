using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoja9
{

    struct Alumno
    {
        public string nombre, ap1, ap2;
        public int tlf;

        public Alumno(string n, string a1,string a2, int t)
        {
            nombre = n;
            ap1 = a1;
            ap2 = a2;
            tlf = t;
        }
    }

    struct Listado
    {
        public int tam;
        public Alumno[] v;
        public Listado(int n)
        {
            tam=n;
            v = new Alumno[tam];
         
            for (int a=0;a<v.Length;a++)
            {
                v[a] = new Alumno();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

        }

        static void crea(Listado lst, int n)
        {
            lst = new Listado(n);
        }

        static int compara(Alumno a, Alumno b)
        {
            int r = 0;
             r= string.Compare(a.nombre,b.nombre);
            if (r == 0)
            {
                r = string.Compare(a.ap1, b.ap1);
                if (r == 0)
                {
                    r = string.Compare(a.ap2, b.ap2);
                }
            }

            return r;
        }
    }
}
