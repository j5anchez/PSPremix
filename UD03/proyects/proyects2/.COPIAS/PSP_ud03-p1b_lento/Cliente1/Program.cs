using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PSP03_SocketClass_TCP_Cliente
{

    public class Program
    {

        public static int Main(String[] args)
        {
            Thread.Sleep(1000);
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            int port = 13000;
            Random rdm = new Random();
            var nmax = 1000000001;
            var nmin = 1;
            Random randomTime = new Random();
            var maxSleep = 1000;
            var minSleep = 1;
            try
            {
                Cliente1 cliente = new Cliente1();
                cliente.establecerConexion();
                cliente.transfiendoInfo("cliente");
                string msg = cliente.recibiendoInfo();
                Console.WriteLine("Identificador de cliente: " + msg.ToString() + "\nIntenta adivinar mi numero:");
                int njugadas = 0;
                string findelapartida = "*****************\nFin de la partida.\n*****************\nFin del juego";
                Console.ReadKey();
                while (true)
                {
                    Thread.Sleep(randomTime.Next(minSleep, maxSleep));

                    string cadena = rdm.Next(nmin, nmax).ToString();
                    cliente.transfiendoInfo(cadena);
                    Console.WriteLine(cadena);
                    njugadas++;
                    msg = cliente.recibiendoInfo();
                    Console.WriteLine(msg);
                    if (msg.Equals("Has acertado!!Zorionak!") || cadena.Equals("salir"))
                    {
                        Console.WriteLine("Numero de jugadas realizadas por ti: {0}", njugadas);
                        Console.WriteLine(findelapartida);
                        break;
                    }
                    else if (msg.Substring(0, 13).Equals("El ganador es"))
                    {
                        Console.WriteLine(findelapartida);
                        break;
                    }
                    else if (msg.Equals("El numero es mayor."))
                    {
                        nmin = Int32.Parse(cadena);
                    }
                    else if (msg.Equals("El numero es menor."))
                    {
                        nmax = Int32.Parse(cadena);

                    }
                }
                cliente.cerrarCliente();

            }
            catch (SocketException se)
            {
                Console.WriteLine("Cliente ha tenido problemas  de SocketException : {0}", se.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cliente ha tenido problemas al establecer la conexión con el servidor");
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("\nPresiona intro para continuar...");
            Console.Read();


            //Si todo ha ido bien devuelve un 0.
            return 0;
        }
    }

}