

using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PSP03_SocketClass_TCP_Cliente
{

    internal class Cliente1
    {
        //ATRIBUTOS

        private readonly Socket client = null;
        private readonly int port = 13000;
        private readonly IPAddress ipAddress = null;
        //CONSTRUCTOR

        public Cliente1()
        {
            //this.port = puerto;
            ipAddress = IPAddress.Parse("127.0.0.1");
            client = new Socket(AddressFamily.InterNetwork,
                        SocketType.Stream, ProtocolType.Tcp);

            //Socket client = new Socket(AddressFamily.InterNetwork,
            //SocketType.Stream, ProtocolType.Tcp);


        }

        //MÉTODOS
        //establecerConexión establece la conexión con el equipo remoto
        public void establecerConexion()
        {
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);
            client.Connect(ipEndPoint);
        }

        public void transfiendoInfo(string datos)
        {
            //string fileName = "C:\\test.txt";

            ///Console.WriteLine("Sending {0} to the host.", fileName);
            //client.SendFile(fileName);
            Console.WriteLine("Cliente transfiriendo datos.");
            byte[] msg = Encoding.ASCII.GetBytes(datos);
            Console.WriteLine("{0}", datos);
            int bytesSnd = client.Send(msg);
        }

        public string recibiendoInfo()
        {

            object o = new object();
            lock (o)
            {
                //se crea un array de tipo byte donde se irán recibiendo los datos.
                byte[] bytes = new byte[1024];
                //Recibe los datos
                int bytesRec = client.Receive(bytes);
                string datos = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                //Console.ReadKey();
                return datos;

            }
            //Console.WriteLine("Datos recibidos del SERVIDOR: \n\t");
        }
        public void cerrarCliente()
        {
            //Deja de enviar y recibir datos
            client.Shutdown(SocketShutdown.Both);

            //Cierra la conexión de socket.
            client.Close();

        }


    }
}
