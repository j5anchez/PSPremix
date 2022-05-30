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
   
      
        static void Main(string[] args)
        {
            string mensaje;

            NamedPipeServerStream servidor = new NamedPipeServerStream("servidor");
            Console.WriteLine("Servidor iniciado, esperando un cliente...");
            StreamWriter escritor = new StreamWriter(servidor);

            NamedPipeClientStream cliente = new NamedPipeClientStream("servidor");
            Console.WriteLine("Cliente iniciado, buscando servidores...");
            StreamReader lector = new StreamReader(cliente);

            servidor.WaitForConnectionAsync();//async
            Console.WriteLine("Esperando conexion");

            cliente.Connect();
            Console.WriteLine("Cliente conectado");
            Console.WriteLine("Escriba su mensaje");
            escritor.WriteLineAsync("hola pepe");
            Console.WriteLine("Mensaje enviado");

            mensaje = escritor.WriteLineAsync().ToString();
            escritor.Flush();

            Console.WriteLine(mensaje);


            Console.WriteLine("Mensaje recibido, presione una tecla para terminar.");
            Console.ReadKey();





        }
    }
}