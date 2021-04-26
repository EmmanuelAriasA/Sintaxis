using System;
using System.Collections.Generic;
using System.Text;

//  Requerimiento 1: Ajustar el constructor Lexico(String) para que substraiga el nombre del archivo
//  y el directorio.
//Requerimiento 2: Validar en le constructor Lexico(String) que la extension del archivo deba ser
//cpp y levantar una excepcion en caso contrario
// Requerimiento 3: Identificar errores sintacticos con linea y caracter, y grabarlos en el log
namespace Sintaxis3
{
    class Lenguaje : Sintaxis
    {

        public Lenguaje()
        {
            Console.WriteLine("Iniciando analisis gramatical.");
        }

        public Lenguaje(string nombreArch) : base(nombreArch)
        {
            Console.WriteLine("Iniciando analisis gramatical.");
        }

        // Programa -> Libreria Main
        public void Program()
        {
            Libreria();
            Main();
        }
        // Libreria -> (#include<identificador.h> Libreria) ?
        private void Libreria()
        {
            if (getContenido() == "#")
            {
                match("#");
                match("include");
                match("<");
                match(Token.clasificaciones.identificador);
                if (getContenido() == ".")
                {
                    match(".");
                    match("h");
                }
                match(">");

                Libreria();
            }
        }

        // Libreria -> #include<identificador(.h)?> Libreria ?

        // Main -> void main(){ (Variables)? Instrucciones }
        private void Main()
        {
            match(clasificaciones.tipoDato);
            match("main");
            match("(");
            match(")");

            BloqueInstrucciones();
        }

        //BloqueInstrucciones -> { Instrucciones }

        private void BloqueInstrucciones()
        {
            match(clasificaciones.inicioBloque);

            Instrucciones();

            match(clasificaciones.finBloque);
        }

        //Lista_IDs -> identificador (,Lista_IDs)?
        private void Lista_IDs()
        {
            match(clasificaciones.identificador);

            if (getContenido() == ",")
            {
                match(",");
                Lista_IDs();
            }
        }

        //Variables -> tipoDato List_IDs;
        private void Variables()
        {
            match(clasificaciones.tipoDato);
            Lista_IDs();
            match(clasificaciones.finSentencia);

        }

        //Instruccion -> (inicializacion | printf(identificador | cadena | numero)) ;

        private void Instruccion()
        {
            if(getContenido() == "const")
            {
                Constante();
            }
            else if (getClasificacion() == clasificaciones.tipoDato)
            {
                Variables();
            }
            else if (getContenido() == "printf")
            {
                match("printf");
                match("(");

                if (getClasificacion() == clasificaciones.numero)
                {

                    match(clasificaciones.numero);
                }
                else if (getClasificacion() == clasificaciones.cadena)
                {
                    match(clasificaciones.cadena);
                }
                else
                {
                    match(clasificaciones.identificador);
                }
                match(")");
                match(clasificaciones.finSentencia);

            }
            else
            {
                match(clasificaciones.identificador);
                match(clasificaciones.asignacion);

                if (getClasificacion() == clasificaciones.numero)
                {

                    match(clasificaciones.numero);
                }
                else if (getClasificacion() == clasificaciones.cadena)
                {
                    match(clasificaciones.cadena);
                }
                else
                {
                    match(clasificaciones.identificador);
                }
                    match(clasificaciones.finSentencia);
            }
        }

        //Instrucciones -> Instruccion Instrucciones?
        private void Instrucciones()
        {
            Instruccion();

            if (getClasificacion() != clasificaciones.finBloque)
            {
                Instrucciones();
            }
        }

        //Constante -> const TipoDato identificador = numero | cadena;
        private void Constante()
        {
            match("const");
            match(clasificaciones.tipoDato);
            match(clasificaciones.identificador);
            match(clasificaciones.asignacion);

            if (getClasificacion() == clasificaciones.numero)
            {
                match(clasificaciones.numero);
            }
            else
            {
                match(clasificaciones.cadena);
            }

            match(clasificaciones.finSentencia);
        }
    }
}
