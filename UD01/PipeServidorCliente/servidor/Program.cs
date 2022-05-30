
/*
 
 
 PARTE SERVIDOR
 Creará conexión con el programa cliente mediante un pipe con nombre. Mostrando por consola el momento del establecimiento
de la conexión.
Quedará a la espera de la lectura del nombre del cuento..
Con el nombre abrirá el fichero (nombredecuento.txt ==> cuentoX.txt). Podéis utilizar estos dos ficheros para realizar
pruebas cuento1.txt y cuento2.txt
El programa leerá el fichero, detectará los huecos y solicitará información al programa cliente.
Se quedará a la espera del feedback del usuario, mediante el pipe y cuando lo recoja, rellenará los huecos de la historia. El
resultado se irá registrando en un fichero que llevará por nombre resultado.txt.
Cuando el procesamiento de la historia haya finalizado, se enviará al cliente el contenido de fichero resultados.txt.
El programa servidor deberá mostrar por consola como mínimo los siguientes datos. 
Establecimiento de la conexión.
Que la apertura del  fichero del cuento ha sido satisfactoria.
Recepción de datos desde el programa cliente.
Transmisión de datos desde el programa servidor.
 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;

namespace servidor
{
    class Program
    {

      


        public static void servidor1()
        {

            string cuento;
            string archivo;
            string path = "C:\\Users\\34603\\Desktop\\trabajosDAM\\PSP\\UD01\\PipeServidorCliente\\";
            string lineas;
            int largo;
            string tipo = "";
            string texto = "";
            char c;
            bool sw = false;


            NamedPipeServerStream servidor = new NamedPipeServerStream("servidor");
                servidor.WaitForConnection();
                Console.WriteLine("Conexion a servidor establecida.");
                StreamReader lectorS = new StreamReader(servidor);
                StreamWriter escritorS = new StreamWriter(servidor);
                Console.WriteLine("Pipe Servidor esperando datos.");
                cuento = lectorS.ReadToEnd();
                lectorS.Close();
                Console.WriteLine(cuento);

                Console.Write("Tubo servidor recibiendo datos: ");
                Console.WriteLine($"'N {cuento}'");
                archivo = cuento + ".txt";
                Console.WriteLine("Apertura de fichero:" + archivo);

                if (File.Exists(path + archivo))
                {
                    StreamReader abrircuento = new StreamReader(path + archivo);
                    Console.WriteLine("Fichero abierto. " + archivo);
                    lineas = abrircuento.ReadToEnd();
                    largo = lineas.Length;
                    for (int i = 0; i < largo; ++i)
                    {
                        texto += lineas[i];
                        if (lineas[i] == '<')
                        {
                            sw = true;
                            continue;

                        }
                        else if (lineas[i] == '>')
                        {

                            sw = false;
                            Console.WriteLine("Tubo servidor procesando datos: '" + texto + "'");


                            escritorS.WriteLine(tipo);
                            Console.WriteLine("Tubo servidor emitiendo datos: '" + tipo + "'");


                            Console.ReadKey();

                            tipo = "";



                        }
                        if (sw == true)
                        {
                            tipo += lineas[i];
                        }

                    }


                }
                else
                {
                    Console.WriteLine("El fichero no existe");
                }








                //Environment.Exit(1);

                lectorS.Close();
                escritorS.Close();
                servidor.Close();

                Console.WriteLine("presiona una tecla para cerrar");
                Console.ReadKey();
     

        }
    
        static void Main(string[] args)
        {
          
                servidor1();
            
     
        }
    }
}