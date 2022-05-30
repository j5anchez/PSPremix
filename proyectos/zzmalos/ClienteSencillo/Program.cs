using System;
using System.Text;
using System.IO.Pipes;

namespace ClienteBueno

{
    class Program
    {
        public static void enviar()
        {
            Console.WriteLine("Estableciendo conexion con el servidor\n");

            while (true)
            {

                var cliente = new NamedPipeClientStream("pipe");
                cliente.Connect();
               



                    Console.WriteLine("Indica el nombre del cuento elegido:\n");
                    string line = Console.ReadLine();

                    byte[] buffer = ASCIIEncoding.ASCII.GetBytes(line);
                    cliente.Write(buffer, 0, buffer.Length);
                    Console.WriteLine("Tubo cliente procesando datos: 'N {0}'",line );
                    cliente.Close();
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

        public static void recibir()
        {
            Console.WriteLine("Conectando a servidor");

            while (true)
            {
                var cliente = new NamedPipeClientStream("pipe");
                cliente.Connect();
                byte[] buffer = new byte[255];
                cliente.Read(buffer, 0, 255);
                string request = ASCIIEncoding.ASCII.GetString(buffer);
                Console.WriteLine(request);
                request = request.Trim('\0');

                cliente.Close();
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


        static void Main(string[] args)
        {

            enviar();
            //recibir();


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