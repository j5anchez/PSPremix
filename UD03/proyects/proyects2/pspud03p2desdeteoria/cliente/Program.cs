using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class SynchronousSocketClient
{

    public static void StartClient()
    {
        // Data buffer for incoming data.  
        byte[] bytes = new byte[1024];
        //Este array se utiliza como buffer de lectura. Se debe tener en cuenta que en función del tamaño de los datos que se vayan a recibir puede que haya que realizar varias lecturas del buffer.
        // Connect to a remote device.  
        try
        {
            // Establish the remote endpoint for the socket.  
            // This example uses port 11000 on the local computer.  
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 13000);
            //En el ejemplo el servidor esta en la primera de la direcciones IP de la maquina, es decir, el servidor y el cliente están en la misma maquina y se comunican a través de un socket. El puerto que utiliza el servidor para escuchar es el 11000.
            // Create a TCP/IP  socket.  
            Socket sender = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);
            //Se crea el socket especificando el tipo de dirección IP, el tipo de socket y el protocolo. 
            // Connect the socket to the remote endpoint. Catch any errors.  
            try
            {
                sender.Connect(remoteEP);
                //Se establece la conexión con el servidor.El parámetro de entrada a la función son lo datos que se han definido anteriormente primera dirección IP de la máquina y puerto 11000
                Console.WriteLine("Socket connected to {0}",
                    sender.RemoteEndPoint.ToString());

                // Encode the data string into a byte array.  
                byte[] msg = Encoding.ASCII.GetBytes("This is a test");
                //Codificación de los datos que se quieren enviar. 
                // Send the data through the socket.  
                int bytesSent = sender.Send(msg);
                //Envio de los datos codificados al servidor
                // Receive the response from the remote device.  
                int bytesRec = sender.Receive(bytes);
                Console.WriteLine("Echoed test = {0}",
                    Encoding.ASCII.GetString(bytes, 0, bytesRec));
                //Lectura de los datos recibidos desde es servidor a buffer de lectura creado y a continuación se escriben por consola convertidos a string
                // Release the socket.  
                Console.ReadKey();
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
                //Liberación de los recursos del socket. 
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public static int Main(String[] args)
    {
        StartClient();
        return 0;
    }
}

