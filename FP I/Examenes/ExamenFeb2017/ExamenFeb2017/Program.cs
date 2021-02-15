using System;
namespace ExamenFeb2017
{
    class Program
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            const int MAX_FALLOS = 10;

            string pal="CELESTINA";
            bool[] descubiertas = new bool[pal.Length];
            int fallos = 0;

            for(int i = 0; i < descubiertas.Length; i++)
            {
                descubiertas[i] = rnd.Next(-1, 2) == 0;
            }
            Muestra(pal, descubiertas, 0);

            while(!PalabraAcertada(descubiertas)&& fallos < MAX_FALLOS)
            {
                char c = LeeLetra();
                bool failed=false;
                DescubreLetras(pal, descubiertas, c, ref failed);
                if (!failed)
                {
                    fallos++;
                }
                Muestra(pal, descubiertas, fallos);
            }
        }

        static void Muestra(string pal, bool [] descubiertas, int fallos)
        {
            Console.Clear();
            for(int i = 0; i < descubiertas.Length; i++)
            {
                Console.SetCursorPosition(2 * (i + 3), 4);
                if (descubiertas[i])
                {
                    Console.Write(pal[i]);
                }
                else
                {
                    Console.Write("- ");
                }
            }

            Console.SetCursorPosition(3, 8);
            Console.Write("fallos " + fallos);


            Console.WriteLine();
            Console.SetCursorPosition(5, 10);
            Console.Write("Escribe letra: ");
        }
        static bool PalabraAcertada(bool [] descubiertas)
        {
            int i = 0;
            while (i < descubiertas.Length && descubiertas[i])
                i++;
            return i>=descubiertas.Length;
        }

        static char LeeLetra()
        {
            char c= char.Parse(Console.ReadLine());
            c = char.ToUpper(c);
            
            return c;
        }
        static void DescubreLetras(string pal, bool[] descubiertas, char c, ref bool acierto)
        {
            for(int i = 0; i < descubiertas.Length; i++)
            {
                if(pal[i]==c && !descubiertas[i])
                {
                    descubiertas[i] = true;
                    acierto = true;
                }
            }
          
        }
    }
}
