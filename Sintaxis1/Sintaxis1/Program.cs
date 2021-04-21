using System;

namespace Sintaxis1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (Lenguaje l = new Lenguaje("C:\\Archivos\\suma.txt"))
                {
                    /*while (!l.FinDeArchivo())
                    {
                        l.NextToken();
                    }
                    
                    l.match("#");
                    l.match("include");
                    l.match("<");
                    l.match(Token.clasificaciones.identificador);
                    l.match(".");
                    l.match("h");
                    l.match(">"); 
                     */

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