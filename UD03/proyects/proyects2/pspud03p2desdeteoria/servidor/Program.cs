using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class SynchronousSocketListener
{

    // Incoming data from the client.  
    public static string data = null;

    public static void StartListening()
    {
        // Data buffer for incoming data.  
        byte[] bytes = new Byte[1024];
        //Buffer para la lectura de datos desde recibidos desde el cliente
              // Establish the local endpoint for the socket.  
              // Dns.GetHostName returns the name of the   
              // host running the application.  
              IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 13000);
        //Datos para EPEndPoint, primera dirección de la lista ipHostInfo y puerto 11000
        // Create a TCP/IP socket.  
        Socket listener = new Socket(ipAddress.AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);
        //El socket se instancia de la misma manera que en el cliente, pasando como parámetro el tipo de dirección IP, el tipo de Socket y el protocolo.
        // Bind the socket to the local endpoint and   
        // listen for incoming connections.  
        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(10);
          //  Asocia al socket el EPEndPoint definido e indica al socket que comience a escuchar peticiones estableciendo la longitud máxima de la cola de conexiones pendientes(10).
            // Start listening for connections.  
            while (true)
            {
                Console.WriteLine("Waiting for a connection...");
                // Program is suspended while waiting for an incoming connection.  
                Socket handler = listener.Accept();
                data = null;
            //    Se crea un nuevo socket a partir del socket que se creo con anterioridad. El socket se creó con una cola de peticiones máximas. Se acepta la petición que primero entre en la cola.
                // An incoming connection needs to be processed.  
                while (true)
                {
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }
              //  Se leen los datos del buffer hasta que se encuenta el token que marca el fin del fichero y en ese momento se sale del bucle
                // Show the data on the console.  
                Console.WriteLine("Text received : {0}", data);

                // Echo the data back to the client.  
                byte[] msg = Encoding.ASCII.GetBytes(data);
                handler.Send(msg);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
                //Se devuelve el mensaje recibido al cliente y se libera el socket que se ha utilizado para leer y enviar datos al cliente, no el socket que se utiliza para escuchar las peticiones de conexión.
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        Console.WriteLine("\nPress ENTER to continue...");
        Console.Read();

    }

    public static int Main(String[] args)
    {
        StartListening();
        return 0;
    }
}
