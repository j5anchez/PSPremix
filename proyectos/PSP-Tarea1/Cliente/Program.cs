using System;
using System.Text;
using System.IO.Pipes;
namespace PipeCliente
{
    class Program
    {
        public static void Main(string[] args)
        {
            Main llamadaMain = new Main();
            llamadaMain.index();
        }
    }
    class Main
    {
        const string carpetas = @"C:\Users\34603\Desktop\trabajos DAM\PSP\proyectos\PSP-Tarea1\";
        string line = "";
        string request = "";
        string palabra = "";
        string resultado = "";
        string contadorstr = "";
        int contadorint = 0;
        public void resetVariables()
        {
            line = "";
            request = "";
            palabra = "";
            resultado = "";
            contadorstr = "";
            contadorint = 0;
        }
        public void enviar()
        {
            Console.WriteLine("Estableciendo conexion con el servidor\n");
            using (var ClienteStream = new NamedPipeClientStream("pipes"))
            {
                line = "";
                ClienteStream.Connect();
                for (int i = 0; i < i + 1; ++i)
                {
                    Console.WriteLine("Indica el nombre del cuento elegido o escribe salir para terminar:\n");
                    line = Console.ReadLine();
                    if (line == "salir")
                    {
                        byte[] bufferinline = ASCIIEncoding.ASCII.GetBytes(line);
                        ClienteStream.Write(bufferinline, 0, bufferinline.Length);
                        ClienteStream.Close();
                        Environment.Exit(0);
                    }
                    else if ((!File.Exists(@"..\..\..\..\Servidor\" + line + ".txt") && (!File.Exists(@"..\Servidor\" + line + ".txt"))))
                    {
                        Console.WriteLine("Archivo inexistente, vuelve a intentarlo");
                        continue;
                    }
                    break;
                }
                byte[] buffer = ASCIIEncoding.ASCII.GetBytes(line);
                ClienteStream.Write(buffer, 0, buffer.Length);
                Console.WriteLine("Tubo cliente procesando datos: 'N {0}'", line);
            }
            return;
        }
        public void recibir()
        {
            using (var ClienteStream = new NamedPipeClientStream("pipes"))
            {
                request = "";
                ClienteStream.Connect();
                byte[] buffer = new byte[255];
                ClienteStream.Read(buffer, 0, 255);
                request = ASCIIEncoding.ASCII.GetString(buffer);
                request = request.Trim('\0');
                Console.WriteLine(request + ":");
            }
        }
        public void recibirPalabra2()
        {
            using (var ClienteStream = new NamedPipeClientStream("pipes"))
            {
                request = "";
                ClienteStream.Connect();
                byte[] buffer = new byte[255];
                ClienteStream.Read(buffer, 0, 255);
                request = ASCIIEncoding.ASCII.GetString(buffer);
                request = request.Trim('\0');
                Console.WriteLine("Tubo Cliente recibiendo datos: 'P {0}'", request);
                Console.WriteLine(request);
            }
        }
        public void enviarPalabra()
        {
            using (var ClienteStream = new NamedPipeClientStream("pipes"))
            {
                palabra = "";
                ClienteStream.Connect();
                palabra = "aaaaaaaaaaaaaa"; // Console.ReadLine();
                byte[] buffer = ASCIIEncoding.ASCII.GetBytes(palabra);
                ClienteStream.Write(buffer, 0, buffer.Length);
                Console.WriteLine("Tubo cliente procesando datos: 'P {0}'", palabra);
            }
        }
        public void Resultado()
        {
            using (var ClienteStream = new NamedPipeClientStream("pipes"))
            {
                resultado = "";
                ClienteStream.Connect();
                byte[] buffer = new byte[255];
                ClienteStream.Read(buffer, 0, 255);
                resultado = ASCIIEncoding.ASCII.GetString(buffer);
                resultado = resultado.Trim('\0');
                Console.WriteLine("*****************\n\n"
                + "El cuento creado es:"
                + "\n\n*****************\n\n" + resultado + "\n\n");
            }
        }
        public void reciboContarPalabras()
        {
            using (var ClienteStream = new NamedPipeClientStream("pipes"))
            {
                try
                {
                    ClienteStream.Connect();
                    byte[] buffer = new byte[255];
                    ClienteStream.Read(buffer, 0, 255);
                    contadorstr = ASCIIEncoding.ASCII.GetString(buffer);
                    contadorstr = contadorstr.Trim('\0');
                    contadorint = Convert.ToInt32(contadorstr);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
        }
        public void index()
        {
            for (int i = 0; i < i + 1; ++i)
            {
                resetVariables();
                enviar();//1
                reciboContarPalabras();//2


                recibir();//3
                enviarPalabra();//4

                // for (int j = 0; j < contadorint-1; ++j)
                // {

                recibirPalabra2();//5
                enviarPalabra();//6
                // }
                Resultado();
            }
        }
    }
}