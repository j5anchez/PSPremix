﻿using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace PSP03_SocketClass_TCP_Cliente
{

    internal class Cliente2
    {
        


        private Socket sender = null;
        private int port = 11000;
        private IPAddress ipAddress = null;
        //CONSTRUCTOR

        public Cliente2()
        {
            //this.port = puerto;
            this.ipAddress = IPAddress.Parse("127.0.0.1");
            this.sender = new Socket(this.ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("Programa cliente iniciando.");
        }

        //MÉTODOS
        //establecerConexión establece la conexión con el equipo remoto
        public void establecerConexion()
        {
            IPEndPoint remoteEP = new IPEndPoint(this.ipAddress, this.port);
            this.sender.Connect(remoteEP);
            Console.WriteLine("Socket conectado a servidor {0}", this.sender.RemoteEndPoint.ToString());
        }

        public void transfiriendoInfo(String datos)
        {
            //Console.WriteLine("Cliente transfiriendo datos.");
            Console.ReadKey();
            byte[] msg = Encoding.ASCII.GetBytes(datos + "<EOF>");
            Console.WriteLine("{0}", datos);
            int bytesSnd = this.sender.Send(msg);
        }

        public string recibiendoInfo()
        {
            //se crea un array de tipo byte donde se irán recibiendo los datos.
            byte[] bytes = new byte[1024];
            //Recibe los datos
            int bytesRec = this.sender.Receive(bytes);
            string datos = Encoding.ASCII.GetString(bytes, 0, bytesRec);
            //Console.WriteLine("Datos recibidos del SERVIDOR: \n\t");
            return datos;
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