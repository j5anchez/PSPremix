using System;
using System.Text;
using System.IO.Pipes;
namespace PipeCliente
{
    class Program
    {
        public static void enviar()
        {
            Console.WriteLine("Conectando a servidor");
            while (true)
            {
                var ClienteStream = new NamedPipeClientStream("pipes");
                ClienteStream.Connect();
                Console.WriteLine("Escribe un Mensaje");
                string line = Console.ReadLine();
                byte[] buffer = ASCIIEncoding.ASCII.GetBytes(line);
                ClienteStream.Write(buffer, 0, buffer.Length);
                if (line.ToLower() == "salir")
                    return;

                if (line.ToLower() == "recibir")
                    recibir();
                ClienteStream.Close();


            }
        }
        public static void recibir()
        {
            Console.WriteLine("Conectando a servidor");
            while (true)
            {
                var ClienteStream = new NamedPipeClientStream("pipes");
                ClienteStream.Connect();
                byte[] buffer = new byte[255];
                ClienteStream.Read(buffer, 0, 255);
                string request = ASCIIEncoding.ASCII.GetString(buffer);
                Console.WriteLine(request);
                request = request.Trim('\0');
                ClienteStream.Close();
                if (request.ToLower() == "recibir")
                    enviar();

                if (request.ToLower() == "salir")
                    return;



            }
        }
        static void Main(string[] args)
        {
            enviar();
        }
    }
}