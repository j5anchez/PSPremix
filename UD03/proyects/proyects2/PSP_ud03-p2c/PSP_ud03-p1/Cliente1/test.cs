/*
using System.Net;
using System.Net.Sockets;

namespace PSP03_SocketClass_TCP_Cliente
{

    public class test
    {

        public static int Main(string[] args)
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 13000);


            try
            {
                Socket client = new Socket(AddressFamily.InterNetwork,
                       SocketType.Stream, ProtocolType.Tcp);
                client.Connect(ipEndPoint);

                while (true)
                {


                }
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

*/