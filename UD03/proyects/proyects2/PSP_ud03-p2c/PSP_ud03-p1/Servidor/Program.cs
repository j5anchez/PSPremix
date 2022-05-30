using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Servidor
{
    public class Program
    {
        private readonly TcpListener listener;
        private bool listening;
        private readonly Random rdm = new Random();

        public static int Main(string[] args)
        {
            int port = 13000;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            Program servidor = new Program(localAddr, port);

            ///string msg = "Quiero que me conviertas a mayúscula este texto";
            servidor.juegoLoteria();

            return 0;
        }
        public Program(IPAddress address, int port)
        {
            listener = new TcpListener(address, port);
            juegoLoteria();
        }

        //SocketInformationOptions
        public bool Listening => listening;

        private readonly int nmax = 1000000000;
        private readonly int nmin = 500000000;

        private void juegoLoteria()
        {
            int numeroSecreto = rdm.Next(nmin, nmax);

            listener.Start();
            listening = true;
            bool finPartida = false;
            int idGanador = 0;
            int idPerdedor = 0;

            object o = new object();
            lock (o)
            {
                try
                {
                    while (true)
                    {
                        Task.Run(async () =>
                        {
                            int id = (int)Task.CurrentId;
                            string mensaje = "Identificador de cliente: " + id + "\nIntenta adivinar mi numero:";

                            TcpClient cliente =  listener.AcceptTcpClient();
                            using (NetworkStream? networkStream = cliente.GetStream())
                            {
                                Console.WriteLine("Conexion con cliente establecida.");

                                byte[]? buffer = new byte[4096];
                                Console.WriteLine("Buffer de entrada y salida creados.\nIdentificadorCliente: {0}", id);

                                while (true)
                                {
                                    byte[]? bytes = File.ReadAllBytes("C:\\test1.txt");
                                    networkStream.Write(bytes, 0, bytes.Length);

                                    /*
                                    int byteCount = await networkStream.ReadAsync(buffer, 0, buffer.Length);
                                    string request = Encoding.UTF8.GetString(buffer, 0, byteCount);

                                  
                                    byte[] ServerResponseBytes = Encoding.UTF8.GetBytes(request);
                                    networkStream.Write(ServerResponseBytes, 0, ServerResponseBytes.Length);*/

                                }
                            }

                        });
                    }
                }


                catch (Exception e)
                {
                    Console.WriteLine("Exception: {0}", e);
                }
                finally
                {
                    listening = false;
                }
            }
        }
    }
}