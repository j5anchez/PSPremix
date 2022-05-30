using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PSP03_SocketClass_TCP_Cliente
{

    public class ClienteSincrono
    {

        public static int Main(String[] args)
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[1];
            int port = 11000;


            try
            {
                Cliente2 cliente = new Cliente2();
                cliente.establecerConexion();
                object o = new object();
                int x = 100;
                while (x > 0)
                {
                    string cadena = "soy el CLIENTE 2, faltan " + x;
                    cliente.transfiriendoInfo(cadena);
                    string msg = cliente.recibiendoInfo();
                    Console.WriteLine(msg);
                    x--;
                    while (x < 70 && x > 30)
                    {
                        lock (o)
                        {
                            Console.WriteLine("bloqueoo0 en cliente" + x);
                            x--;
                        }
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