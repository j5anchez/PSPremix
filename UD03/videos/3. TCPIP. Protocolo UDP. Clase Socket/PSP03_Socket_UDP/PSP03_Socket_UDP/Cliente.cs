

/*
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PSP03_Socket_UDP
{
    internal class Cliente
    {
        public static int Main(String[] args)
        {

            Cliente cliente = new Cliente();
            cliente.FuncionCliente();

            Console.WriteLine("Pulse intro para continuar");
            Console.ReadLine();

            return 0;
        }
        private void FuncionCliente()
        {
            Socket socketServidor = null;

            try
            {
                int port = 2000;
                int numDatos = 0;
                string data = null;

                byte[] bytesRecibidos = new byte[1024];
                byte[] bytesEnviados = new byte[1024];

                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[3];
                socketServidor = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                Console.WriteLine("Programa cliente UDP iniciando.");

                //Conexión de socket al servidor
                IPEndPoint iPEndPointSrv = new IPEndPoint(ipAddress, port);

                IPEndPoint receptor = new IPEndPoint(ipAddress, 0);
                EndPoint emisor = (EndPoint)(receptor);


                while (true)
                {

                    data = String.Empty;
                    data = Console.ReadLine();
                    bytesEnviados = Encoding.ASCII.GetBytes(data);
                    socketServidor.SendTo(bytesEnviados, bytesEnviados.Length, SocketFlags.None, iPEndPointSrv);


                    if (data.Equals("Agur"))
                        break;



                    data = String.Empty;
                    numDatos = socketServidor.ReceiveFrom(bytesRecibidos, ref emisor); // bloqueante
                    Console.WriteLine("Datos recibidor del cliente: {0}", emisor.ToString());
                    data = Encoding.ASCII.GetString(bytesRecibidos, 0, numDatos);

                    Console.WriteLine(data);
                }



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                socketServidor.Close();
            }
        }
    }
}

*/