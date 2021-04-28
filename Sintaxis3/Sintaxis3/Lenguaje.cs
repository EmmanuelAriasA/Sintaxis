using System;
using System.Collections.Generic;
using System.Text;

//  Requerimiento 1: Ajustar el constructor Lexico(String) para que substraiga el nombre del archivo
//  y el directorio.
//  Requerimiento 2: Validar en le constructor Lexico(String) que la extension del archivo deba ser
//  cpp y levantar una excepcion en caso contrario
//  Requerimiento 3: Identificar errores sintacticos con linea y caracter, y grabarlos en el log
//  Requerimiento 4: Agregar el token flujo de entrada (<<) y flujo de salida(>>) en el analisis lexico.
//  Requerimiento 5: Implementar el if.

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
            if(getContenido() == "if")
            {
                If();
            }
            else if(getContenido() == "cin")
            {
                match("cin");
                match(clasificaciones.flujoEntrada);
                match(clasificaciones.identificador);
                match(clasificaciones.finSentencia);
            }
            else if(getContenido() == "cout")
            {
                match("cout");
                ListaFlujoSalida();
                match(clasificaciones.finSentencia);
            }
            else if(getContenido() == "const")
            {
                Constante();
            }
            else if (getClasificacion() == clasificaciones.tipoDato)
            {
                Variables();
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

        // ListaFlujoSalida -> << cadena | identificador | numero (ListaFlujoSalida)?

        private void ListaFlujoSalida()
        {
            match(clasificaciones.flujoSalida);

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
            
            if(getClasificacion() == clasificaciones.flujoSalida)
            {
                ListaFlujoSalida();
            }

        }

        //if -> if(condicion) BloqueInstrucciones (else bloqueInstrucciones)?
        private void If()
        {
            match("if");
            match("(");
            Condicion();
            match(")");
            BloqueInstrucciones();

            if(getContenido() == "else")
            {
                match("else");
                BloqueInstrucciones();
            }
        }

        //Condicion -> identificador operadorRelacional identificador
        private void Condicion()
        {
            match(clasificaciones.identificador);
            match(clasificaciones.operadorRelacional);
            match(clasificaciones.identificador);
        }
    }
}
