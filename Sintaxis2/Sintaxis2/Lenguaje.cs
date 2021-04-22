using System;
using System.Collections.Generic;
using System.Text;

//  Requerimiento 1: Ajustar el constructor Lexico(String) para que substraiga el nombre del archivo
//  y el directorio.

namespace Sintaxis2
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
        private void Libreria2()
        {
            match("#");
            match("include");
            match("<");
            match(Token.clasificaciones.identificador);
            if(getContenido() == ".")
            {
                match(".");
                match("h");
                match(">");
            }

            if (getContenido() == "#")
            {
                Libreria2();
            }
        }

        // Main -> void main(){ (Variables)? Instrucciones }
        private void Main()
        {
            match("void");
            match("main");
            match("(");
            match(")");
            match(clasificaciones.inicioBloque);

            if (getClasificacion() == clasificaciones.tipoDato)
            {
                Variables();
            }

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

        //Variables -> tipoDato List_IDs; (Variables)?
        private void Variables()
        {
            match(clasificaciones.tipoDato);
            Lista_IDs();
            match(clasificaciones.finSentencia);

            if (getClasificacion() == clasificaciones.tipoDato)
            {
                Variables();
            }
        }

        //Instruccion -> (inicializacion | printf(identificador | cadena | numero)) ;

        private void Instruccion()
        {
            if (getContenido() == "printf")
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
            }
            else
            {
                match(clasificaciones.identificador);
                match(clasificaciones.inicializacion);

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
            }
                match(clasificaciones.finSentencia);
        }

        //Instrucciones -> Instruccion Instrucciones?
        private void Instrucciones()
        {
            Instruccion();

            if (getClasificacion() == clasificaciones.identificador)
            {
                Instrucciones();
            }
            else
            {

            }
        }
 
    }
}
