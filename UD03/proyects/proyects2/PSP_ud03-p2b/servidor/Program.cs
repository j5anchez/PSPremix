using System.Net;
using System.Net.Sockets;
using System.Text;

namespace servidor
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
            Console.WriteLine("El numero aleatorio es: {0}", numeroSecreto);

            listener.Start();
            listening = true;
            Console.WriteLine("Socket lister creado.");
            bool finPartida = false;
            int idGanador = 0;
            int idPerdedor = 0;
            string request = string.Empty;
            object o = new object();

            try
            {
                while (true)
                {
                    Task.Run(async () =>
                    {
                        int id = (int)Task.CurrentId;
                        string mensaje = "Identificador de cliente: " + id + "\nIntenta adivinar mi numero:";

                        TcpClient cliente = await listener.AcceptTcpClientAsync();
                        using (NetworkStream? networkStream = cliente.GetStream())
                        {
                            Console.WriteLine("Conexion con cliente establecida.");

                            byte[]? buffer = new byte[4096];
                            Console.WriteLine("Buffer de entrada y salida creados.\nIdentificadorCliente: {0}", id);

                            while (true)
                            {

                                int byteCount = networkStream.Read(buffer, 0, buffer.Length);
                                request = Encoding.UTF8.GetString(buffer, 0, byteCount);

                                if (finPartida == true && id != idGanador && id != idPerdedor)

                                {
                                    idPerdedor = id;
                                    Console.WriteLine(request);
                                    request = "El ganador es: " + idGanador.ToString();
                                    Console.WriteLine("0\nGANADOR\nIdentificador: {0}\nCliente: 0\nHas acertado!!Zorionak!", idGanador);
                                }
                                else
                                {


                                    if (request.Equals("cliente"))
                                    {
                                        request = id.ToString();
                                        //Thread.Sleep(10000);
                                        Console.ReadKey();
                                    }
                                    else if (int.Parse(request) > numeroSecreto)
                                    {
                                        Console.WriteLine(request);
                                        request = "El numero es menor.";

                                    }
                                    else if (int.Parse(request) < numeroSecreto)
                                    {
                                        Console.WriteLine(request);
                                        request = "El numero es mayor.";

                                    }
                                    else if (int.Parse(request) == numeroSecreto)
                                    {
                                        Console.WriteLine(request);
                                        request = "Has acertado!!Zorionak!";
                                        Console.WriteLine(request);
                                        finPartida = true;
                                        idGanador = id;

                                    }
                                    else
                                    {
                                        return;
                                    }

                                }

                                byte[] ServerResponseBytes = Encoding.UTF8.GetBytes(request);
                                networkStream.Write(ServerResponseBytes, 0, ServerResponseBytes.Length);

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