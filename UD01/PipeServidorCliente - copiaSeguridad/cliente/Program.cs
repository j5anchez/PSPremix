using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;

namespace cliente
{
    class Program
    {
        private static void iniciarcliente()
        {
            NamedPipeClientStream cliente = new NamedPipeClientStream("servidor");
            Console.WriteLine("Cliente iniciado, buscando servidores...");
            cliente.Connect();
            Console.WriteLine("Servidor encontrado, esperando mensaje...");
            StreamReader lector = new StreamReader(cliente);

            Console.Write("mensaje recibido: ");

            Console.WriteLine(lector.ReadLine()); 

            lector.Close();
            Console.WriteLine("presione una tecla para terminar.");
            Console.ReadKey();


            

        }
     
        static void Main(string[] args)
        {
            iniciarcliente();        }
    }
}