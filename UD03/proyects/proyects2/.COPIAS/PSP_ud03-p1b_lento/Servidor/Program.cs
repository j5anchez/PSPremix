using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Servidor
{
    public class Program
    {
        private readonly TcpListener listener;
        private bool listening;
        Random rdm = new Random();

        public static int Main(string[] args)
        {
            int port = 13000;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            Program servidor = new Program(localAddr, port);

            ///string msg = "Quiero que me conviertas a mayúscula este texto";
            servidor.StartAsync();

            return 0;
        }
        public Program(IPAddress address, int port)
        {
            listener = new TcpListener(address, port);
            StartAsync();
        }

        //SocketInformationOptions
        public bool Listening => listening;
        int nmax = 1000000000;
        int nmin = 500000000;
        private void StartAsync()
        {
            int numeroSecreto = rdm.Next(nmin, nmax);
            Console.WriteLine("El numero aleatorio es: {0}", numeroSecreto);

            listener.Start();
            listening = true;
            Console.WriteLine("Socket lister creado.");
            bool finPartida = false;
            int idGanador = 0;
            int idPerdedor = 0;
            //Console.ReadKey();

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
                            //string findelapartida = "**********\nFin de la partida.\n**********\nFin del juego";
                            string mensaje = "Identificador de cliente: " + id + "\nIntenta adivinar mi numero:";

                            TcpClient cliente = await listener.AcceptTcpClientAsync();
                            using (var networkStream = cliente.GetStream())
                            {
                                Console.WriteLine("Conexion con cliente establecida.");

                                var buffer = new byte[4096];
                                Console.WriteLine("Buffer de entrada y salida creados.\nIdentificadorCliente: {0}", id);

                                while (true)
                                {
                                    var byteCount = await networkStream.ReadAsync(buffer, 0, buffer.Length);
                                    string request = Encoding.UTF8.GetString(buffer, 0, byteCount);
                                    //Console.WriteLine("[Servidor] El cliente ha escrito: {0}", request);

                                    if (finPartida == true && id != idGanador && id != idPerdedor)// && !request.Substring(0, 13).Equals("El ganador es"))

                                    {
                                        idPerdedor = id;
                                        Console.WriteLine(request);
                                        request = "El ganador es: " + idGanador.ToString();
                                        Console.WriteLine("0\nGANADOR\nIdentificador: {0}\nCliente: 0\nHas acertado!!Zorionak!", idGanador);
                                    }
                                    else
                                    {


                                        if (request.Equals("cliente"))
                                            request = id.ToString();

                                        else if (Int32.Parse(request) > numeroSecreto)
                                        {
                                            Console.WriteLine(request);
                                            request = "El numero es menor.";

                                        }
                                        else if (Int32.Parse(request) < numeroSecreto)
                                        {
                                            Console.WriteLine(request);
                                            request = "El numero es mayor.";

                                        }
                                        else if (Int32.Parse(request) == numeroSecreto)
                                        {
                                            Console.WriteLine(request);
                                            request = "Has acertado!!Zorionak!";
                                            Console.WriteLine(request);
                                            finPartida = true;
                                            idGanador = id;

                                        }
                                        else
                                        {
                                            Console.WriteLine("ha saltado el else");
                                        }

                                    }


                                    byte[] ServerResponseBytes = Encoding.UTF8.GetBytes(request);
                                    await networkStream.WriteAsync(ServerResponseBytes, 0, ServerResponseBytes.Length);
                                    //Console.WriteLine("[Servidor] Se ha la enviado respuesta.");
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