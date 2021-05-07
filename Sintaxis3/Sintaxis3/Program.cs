using System;

namespace Sintaxis3
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (Lenguaje l = new Lenguaje("C:\\Archivos\\suma.cpp"))
                {
                    /*while (!l.FinDeArchivo())
                    {
                        l.NextToken();
                    }*/

                    l.Program();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}