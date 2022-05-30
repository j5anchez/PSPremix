using System;
using System.Text;
using System.IO.Pipes;

namespace ServidorBueno


{
    class Program
    {


        public static void recibir()
        {
            Console.WriteLine("Esperando conexión");

            string cuento;
            string archivo;
            string path = "C:\\Users\\34603\\Desktop\\trabajosDAM\\PSP\\UD01\\PipeServidorCliente\\";
            string lineas;
            int largo;
            string tipo = "";
            string texto = "";
            char c;
            bool sw = false;
            while (true)
            {
                var namedPipeServerStream = new NamedPipeServerStream("pipe");
                namedPipeServerStream.WaitForConnection();
                Console.WriteLine("Conexion a servidor establecida.");

                byte[] buffer = new byte[255];
                Console.WriteLine("Pipe Servidor esperando datos.");

                namedPipeServerStream.Read(buffer, 0, 255);
                string request = ASCIIEncoding.ASCII.GetString(buffer);

                request = request.Trim('\0');

                Console.WriteLine("Tubo servidor recibiendo datos: 'N {0}'", request);

                request += ".txt";

                /*
                if (File.Exists(path + request))
                {
                    Console.WriteLine("Apertura de fichero:" + request);

                    StreamReader abrircuento = new StreamReader(path + request);
                    Console.WriteLine("Fichero abierto. " + request);
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
                }*/
                namedPipeServerStream.Close();

                if (request.ToLower() == "recibir")
                {
                    enviar();
                }
                if (request.ToLower() == "salir")
                {
                    return;
                }
            }
            Console.WriteLine("Fin");
            Console.ReadLine();



        }

        public static void enviar()
        {
            Console.WriteLine("Esperando conexión");

            while (true)
            {

                var namedPipeServerStream = new NamedPipeServerStream("pipe");
                namedPipeServerStream.WaitForConnection();
                Console.WriteLine("Escribe un Mensaje");
                string line = Console.ReadLine();

                byte[] buffer = ASCIIEncoding.ASCII.GetBytes(line);
                namedPipeServerStream.Write(buffer, 0, buffer.Length);
                namedPipeServerStream.Close();
                if (line.ToLower() == "recibir")
                {
                    recibir();
                }
                if (line.ToLower() == "salir")
                {
                    return;
                }
            }
        }
        static void Main(string[] args)
        {
            recibir();
            //enviar();
        }
    }
}


/*while (true)
{
    try
    {
        _pipeServer.WaitForConnection();
        break;
    }
    catch (IOException)
    {
        _pipeServer.Disconnect();
        continue;
    }            
 }*/