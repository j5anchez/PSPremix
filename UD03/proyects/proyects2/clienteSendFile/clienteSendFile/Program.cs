using System;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Net;

namespace ClientSocketApp
{
    class Program
    {

        static void Main(string[] args)
        {
        connection:
            try
            {
                // Establish the local endpoint for the socket.
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddr = IPAddress.Parse("127.0.0.1");
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 13000);

                // Create a TCP socket.
                Socket client = new Socket(AddressFamily.InterNetwork,
                        SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint.
                client.Connect(ipEndPoint);

                // There is a text file test.txt located in the root directory.
                string fileName = "C:\\test.txt";
                // Create the preBuffer data.
                string string1 = String.Format("This is text data that precedes the file.{0}", Environment.NewLine);
                byte[] preBuf = Encoding.ASCII.GetBytes(string1);

                // Create the postBuffer data.
                string string2 = String.Format("This is text data that will follow the file.{0}", Environment.NewLine);
                byte[] postBuf = Encoding.ASCII.GetBytes(string2);

                //Send file fileName with buffers and default flags to the remote device.
                Console.WriteLine("Sending {0} with buffers to the host.{1}", fileName, Environment.NewLine);
                client.SendFile(fileName, preBuf, postBuf, TransmitFileOptions.UseDefaultWorkerThread);
          

                // Release the socket.
                client.Shutdown(SocketShutdown.Both);
                client.Close();
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine("failed to connect...");
                goto connection;
            }
        }
    }
}



