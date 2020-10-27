using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoja4
{
    class Program
    {
        static void Main(string[] args)
        {
        
        }

        static void Ejercicio1()
        {
            Console.Write("escribe un num");
            double nota=double.Parse(Console.ReadLine());

            if (nota == 10)
            {
                Console.WriteLine("MH");
            }
            else if (nota >= 9)
            {
                Console.WriteLine("SB");
            }
            else if(nota>=7 && nota < 9)
            {
                Console.WriteLine("NT");
            }
            else if(nota>=5 && nota< 7)
            {
                Console.WriteLine("AP");
            }
            else
            {
                Console.WriteLine("SS");
            }
        }

        static void Ejercicio2()
        {
            //pedimos 3 numeros con decimales

            Console.Write("escribe un num");
            double a=double.Parse(Console.ReadLine());
            
            Console.Write("escribe un num");
            double b=double.Parse(Console.ReadLine());
            
            Console.Write("escribe un num");
            double c=double.Parse(Console.ReadLine());
            
            //calculamos el discriminante 
            double d = Math.Pow(b, 2)-4*a*c; //discriminate


            //comprobamos el valor del discriminante
            if (d > 0)     //caso es la raiz positiva (dos soluciones reales)
            {
                double solA = (-b + Math.Sqrt(d)) /(2*a);
                double solB = (-b - Math.Sqrt(d)) /(2*a);

                Console.WriteLine(solA);
                Console.WriteLine(solB);

            }
            
            else if (d == 0) //caso 0 (solucion unica real)
            {
                double sol = (-c/b);
                Console.WriteLine(sol);
            }

            else            //caso imaginario (dos soluciones complejas)
            {
                //num complejo = real + im *i donde i= Math.Sqrt(-1)
               
                double real=-b/(2*a) ,im= Math.Sqrt(-d)/(2*a); 

                Console.WriteLine(real +" + "+ im +"*i");
                Console.WriteLine(real +" - " +im+"*i");
            }

            

        }

       static void Ejercicio3(){
          Console.Write("Introduce un num");
           int a=int.Parse(Console.ReadLine());
           
          Console.Write("Introduce un num");
           int b=int.Parse(Console.ReadLine());

          Console.Write("Introduce un num");
           int c=int.Parse(Console.ReadLine());
         
          Console.Write("Introduce un num");
           int d=int.Parse(Console.ReadLine());

          int max=a;
          if(b<max) max=b;
          if(c<max) max=c;
          if(d<max) max=b;
          
       }

        static void Ejercicio4(){

            Console.Write("Introduce tu consumo en m3: ");
            float c= int.Parse(Console.ReadLine());
            float pre=0.15f;

            if(c>1000){
                pre=0.8f;
            }
            else if(c>=500 && c<1000){

                pre=0.35f;
            }
            
            else if(c>=100 && c<500){
                pre=0.2f;
            }
            c=c*pre;
            Console.Write(c);
        }
    
        static void Ejercicio5(){

        }
    }
}
