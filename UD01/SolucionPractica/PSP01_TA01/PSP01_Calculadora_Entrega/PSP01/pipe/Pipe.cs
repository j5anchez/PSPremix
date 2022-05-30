using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace pipe
{
    class Pipe
    {
      
        static void Main(string[] args)
        {
            Process p;
            StartServer(out p);
            Task.Delay(1000).Wait();
            string cuento;

            //Preparar conexion del cliente
            var client = new NamedPipeClientStream("PSPtarea1");
            client.Connect();
            StreamReader reader = new StreamReader(client);
            StreamWriter writer = new StreamWriter(client);

            while (true)
            {
                cuento = String.Empty;
                Console.WriteLine(cuento);

                if (cuento != null)
                {
                    if ("E".Equals(cuento))
                    {
                        // Salir del while (true)
                        break;
                    }
                    writer.WriteLine(cuento);
                    writer.Flush();
                    Console.WriteLine("Resultado: {0}", reader.ReadLine());
                }
            }
            //Parar pipes
            if (p != null && !p.HasExited)
            {
                p.Kill();
                p = null;
            }
        }
        
        static Process StartServer(out Process p1)
        {
            // iniciar un proceso con el servidor y devolver
            ProcessStartInfo info = new ProcessStartInfo(@"..\..\..\..\pipeServidor\bin\Release\netcoreapp3.1\win-x64\pipeServidor.exe");
            
            // su valor por defecto el false, si se establece a true no se "crea" ventana
            info.CreateNoWindow = false;
            info.WindowStyle = ProcessWindowStyle.Normal;
            // indica si se utiliza el cmd para lanzar el proceso
            info.UseShellExecute = true;
            p1 = Process.Start(info);
            return p1;
        }


      

        private static bool Validacuento(string cadenaIn, ref string cuento)
        {
            int opcion;
            if (!Int32.TryParse(cadenaIn, out int op))
            {
                Console.WriteLine("Opción {0} no válida.", op);
                //opcion = op;
                cuento = null;
                return false;
            } else
            {
                opcion = op;
            }
            switch (opcion)
            {
                case 1: // Suma
                    cuento = cuento + "+ ";
                    IntroducirOperandos(ref cuento);
                    break;
                
                case 2: // Resta
                    cuento = cuento + "- ";
                    IntroducirOperandos(ref cuento);
                    break;

                case 3: // Multiplicación
                    cuento = cuento + "* ";
                    IntroducirOperandos(ref cuento);
                    break;
                case 4: // División
                    cuento = cuento + "/ ";
                    IntroducirOperandos(ref cuento);
                    break;
                case 5: // Potencia
                    cuento = cuento + "^ ";
                    IntroducirOperandos(ref cuento);
                    break;
                case -1: // Salir
                    cuento = cuento + "E";
                    Console.WriteLine("Exit");
                break;

                default: // Error de opcion
                    Console.WriteLine("Opción {0} no válida.", op);
                    cuento = null;
                break;

            }
            return false;
        }

        private static bool IntroducirOperandos(ref string cuento)
        {
            Console.Write("Introduce el primer operando :");
            string input = Console.ReadLine();
            if (!Single.TryParse(input, out float op))
            {
                Console.WriteLine("El primer operando NO es un número");
                cuento = null;
                return false;
            }
            Console.Write("Introduce el segundo operando :");
            string input2 = Console.ReadLine();
            if (!Single.TryParse(input2, out float op2))
            {
                Console.WriteLine("El segundo operando NO es un número");
                cuento = null;
                return false;
            }
            cuento = cuento + input + " " + input2;
            return true;
        }
    }
}
