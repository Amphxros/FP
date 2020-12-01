using System;

namespace Hoja6
{
    class Program
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {
           Ejercicio1();
        }

        static void Ejercicio1(){
            int h=PideInt(1,10,"Introduce una altura");
            Console.WriteLine();
            for(int i=1;i<h;i++){
                int r=rnd.Next(-1,2);
                for(int j=h-i-r;j>0;j--){

                    Console.Write(" ");
                }
                for(int k=0; k<i+r ;k++){
                    Console.Write("*");
                }
                Console.WriteLine();          
            }       
        }
    // EJERCICIO 2
        static void Ejercicio2(){
        int fac=PideInt(1,1000, "Escribe un num");
        
        while(fac>0){
            int fact=primFact(fac);
            int e= expFactor(fac,fact);
            fac=fac/(Math.Pow(fact, e));
            }
        }

        int primFact(int n){
            int i=2;
            while(n%i!=0 && n>i)
                i++;
            
            return i; //si n==i -> es un num primo
        }

        int expFactor(int n, int fact){
            int exp=2;
            while(n % Mathf.Pow(fact,exp)==0)
                exp++;
            return exp-1;
        }

    // FIN DE EJERCICIO 2
  
    //EJERCICIO 3
    static void Ejercicio3(){
        int a= PideInt(1,100, "Introduce un num");
        int b= PideInt(1,a, "Introduce un num");
        int c=a; //siendo a%b=c
        while(b!=0){
            c=Resto(a,b); 
            a=b;
            b=c;
        }
        Console.Write(a);

    }
    int Resto(int a, int b){
        return a%b;
    }

    // EJERCICIO 4    
     static void Ejercicio4(){
        int n= PideInt(1,100, "Introduce un num");
        int m= PideInt(1,n, "Introduce un num");
    
        int a=Fact(n);
        int b=Fact(m);
        int c= Fact(n-m);
        a=a/c;
        int r=a/b;
    
    }  
       
        int Fact(int n){
            int f=1;
            for(int i=2;i<n;i++){
                f*=i;
            }
            return f;
        }
  
        static int PideInt(int min, int max, string msg){
            int i=min-1;
            while(i<min && i>max){
                Console.Write(msg);
                i=int.Parse(Console.ReadLine());
            }
            return i;
        }

    }
}