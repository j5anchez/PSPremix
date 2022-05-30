using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Cliente0
{

    internal class Cliente
    {
        //ATRIBUTOS

        private Socket sender = null;
        private int port = 13000;
        private IPAddress ipAddress = null;
        //CONSTRUCTOR

        public Cliente()
        {
            //this.port = puerto;
            this.ipAddress = IPAddress.Parse("127.0.0.1");
            this.sender = new Socket(this.ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("Socket Cliente creado.");
        }

        //MÉTODOS
        //establecerConexión establece la conexión con el equipo remoto
        public void establecerConexion()
        {
            IPEndPoint remoteEP = new IPEndPoint(this.ipAddress, this.port);
            this.sender.Connect(remoteEP);
            Console.WriteLine("Buffer de escritura y lectura creados.");
        }

        public void transfiendoInfo(String datos)
        {
            //Console.WriteLine("Cliente transfiriendo datos.");
            byte[] msg = Encoding.ASCII.GetBytes(datos);
            //Console.WriteLine("{0}", datos);
            int bytesSnd = this.sender.Send(msg);
        }

        public string recibiendoInfo()
        {

            object o = new object();
            lock (o)
            {
                //se crea un array de tipo byte donde se irán recibiendo los datos.
                byte[] bytes = new byte[1024];
                //Recibe los datos
                int bytesRec = this.sender.Receive(bytes);
                string datos = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                //Console.ReadKey();
                return datos;

            }
            //Console.WriteLine("Datos recibidos del SERVIDOR: \n\t");
        }
        public void cerrarCliente()
        {
            //Deja de enviar y recibir datos
            this.sender.Shutdown(SocketShutdown.Both);

            //Cierra la conexión de socket.
            this.sender.Close();

        }


    }
}