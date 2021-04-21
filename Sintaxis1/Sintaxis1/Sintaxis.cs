using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace Sintaxis1
{
    class Sintaxis: Lexico
    {
        public Sintaxis()
        {
            Console.WriteLine("Iniciando analisis sintactico");
            NextToken();
        }

        public Sintaxis(string nombreArch) : base(nombreArch)
        {
            Console.WriteLine("Iniciando analisis sintactico");
            NextToken();
        }

        protected void match(string espera)
        {
            if(espera == getContenido())
            {
                NextToken();
            }
            else
            {
                throw new Exception("Error de sintaxis: se espera un " + espera);
            }
        }

        protected void match(clasificaciones espera)
        {
            if (espera == getClasificacion())
            {
                NextToken();
            }
            else
            {
                throw new Exception("Error de sintaxis: se espera un " + espera);
            }
        }

    }
}
