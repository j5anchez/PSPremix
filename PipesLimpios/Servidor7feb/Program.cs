using System;
using System.Text;
using System.IO.Pipes;
namespace PipeServer
{
    class Program
    {
        public static void recibir()
        {
            Console.WriteLine("Esperando conexión");
            while (true)
            {
                var namedPipeServerStream = new NamedPipeServerStream("pipes");
                namedPipeServerStream.WaitForConnection();
                byte[] buffer = new byte[255];
                namedPipeServerStream.Read(buffer, 0, 255);
                string request = ASCIIEncoding.ASCII.GetString(buffer);
                Console.WriteLine(request);
                request = request.Trim('\0');
                namedPipeServerStream.Close();
                if (request.ToLower() == "recibir")
                    enviar();
                if (request.ToLower() == "salir")
                    return;




            }
        }
        public static void enviar()
        {
            Console.WriteLine("Esperando conexión");
            while (true)
            {
                var ServidorStream = new NamedPipeServerStream("pipes");
                ServidorStream.WaitForConnection();
                Console.WriteLine("Escribe un Mensaje");
                string line = Console.ReadLine();
                byte[] buffer = ASCIIEncoding.ASCII.GetBytes(line);
                ServidorStream.Write(buffer, 0, buffer.Length);
                ServidorStream.Close();

                if (line.ToLower() == "salir")
                    return;

                if (line.ToLower() == "recibir")
                    recibir();

            }
        }
        static void Main(string[] args)
        {
            recibir();
        }
    }
}