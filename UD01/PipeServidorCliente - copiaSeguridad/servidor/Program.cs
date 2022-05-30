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

        private static void iniciarservidor()
        {
            string mensaje = "hola pepe";

            NamedPipeServerStream servidor = new NamedPipeServerStream("servidor");
            Console.WriteLine("Servidor iniciado, esperando un cliente...");
            servidor.WaitForConnection();
            Console.WriteLine("Cliente conectado");
            StreamWriter escritor = new StreamWriter(servidor);

            escritor.WriteLineAsync(mensaje);
            Console.Write("Mensaje enviado: ");
            Console.WriteLine(mensaje);

            escritor.Close();
            Console.WriteLine("presiona una tecla para cerrar");
            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            iniciarservidor();
        }
    }
}