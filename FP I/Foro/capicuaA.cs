using System;

namespace CapicuaA
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = -1; //num negativo para que entre en el bucle
            do
            {
                Console.Write("Introduce un num: ");
                n = int.Parse(Console.ReadLine());
            } while (n < 0);

            //para un num abc
            int l = n.ToString().Length;    //pillamos la longitud de la cadena abc (3)
            int cont = 0;
           
            for(int i=0; i<l/2; i++)        //recorremos hasta la mitad ya que vamos comprobando desde los extremos en este caso 
            {
                int a = (int)(n % Math.Pow(10,i)); // ab-c  nos quedamos con la primera c
                int b = (int)(n / Math.Pow(10,l-i) % (Math.Pow(10,l-i))); //a-ba  nos quedamos con la ultima a
                
                if (a == b) //sii a==c sumamos cont
                    cont++;
            }

            if (cont == l / 2) 
                Console.WriteLine("Capicua");

        }
    }
}
    