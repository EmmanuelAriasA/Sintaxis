using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace Sintaxis3
{
    class Sintaxis : Lexico
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
            caracter++;
            //Console.WriteLine(getContenido() + " = " + espera);
            if (espera == getContenido())
            {
                NextToken();
            }
            else
            {
                bitacora.WriteLine("\nError de sintaxis en la linea: " + linea + ", en el caracter: " + caracter + ", se espera un " + espera);
                throw new Exception("Error de sintaxis en la linea: " + linea + ", en el caracter: " + caracter + ", se espera un " + espera);
            }
        }

        protected void match(clasificaciones espera)
        {
            caracter++;
            //Console.WriteLine(getContenido() + " = " + espera);
            if (espera == getClasificacion())
            {
                NextToken();
            }
            else
            {
                bitacora.WriteLine("\nError de sintaxis en la linea: " + linea + ", en el caracter: " + caracter + ", se espera un: " + espera);
                throw new Exception("Error de sintaxis en la linea: " + linea + ", en el caracter: " + caracter + ", se espera un: " + espera);
            }
        }

    }
}
