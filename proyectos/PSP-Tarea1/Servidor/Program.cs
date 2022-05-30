using System.Net.Mime;
using System.Net.Sockets;
using System;
using System.Text;
using System.IO.Pipes;
namespace PipeServer
{
    class Program
    {
        public static void Main(string[] args)
        {
            Main llamadaMain = new Main();
            llamadaMain.index();
        }
    }
    class Main
    {
        const string carpetas = @"C:\Users\34603\Desktop\trabajos DAM\PSP\proyectos\PSP-Tarea1\";
        List<string> listaPalabrasCliente = new List<string>();
        List<string> listaPalabrasServidor = new List<string>();
        string request = "";
        string path = "";
        string lineas = "";
        string texto = "";
        string tipo = "";
        string borrador = "";
        string guardar = "";
        string textoresultado = "";
        string contadorstr = "";
        int contadorint = 0;
        int largo;
        bool sw = false;
        public void resetVariables()
        {
            listaPalabrasCliente.Clear();
            listaPalabrasServidor.Clear();
            request = "";
            path = "";
            lineas = "";
            texto = "";
            tipo = "";
            borrador = "";
            guardar = "";
            textoresultado = "";
            contadorstr = "";
            contadorint = 0;
            sw = false;
        }
        public void manipularFichero()
        {
            Console.WriteLine(path);
            using (StreamReader abrircuento = new StreamReader(path))
            {
                Console.WriteLine("Fichero abierto. {0}", request);
                lineas = abrircuento.ReadToEnd();
                largo = lineas.Length;
                for (int i = 0; i < largo; ++i)
                {
                    texto += lineas[i];
                    if (lineas[i] == '<')
                    {
                        sw = true;
                        continue;
                    }
                    else if (lineas[i] == '>')
                    {
                        sw = false;
                        break;
                    }
                    if (sw == true)
                    {
                        tipo += lineas[i];
                    }
                }
            }
        }
        public void manipularFichero2()
        {
            // using (StreamReader abrircuento = new StreamReader("borrador.txt"))
            // {
            sw = false;
            texto = "";
            tipo = "";
            lineas = borrador;
            largo = lineas.Length;
            for (int i = 0; i < largo; ++i)
            {
                texto += lineas[i];
                if (lineas[i] == '<')
                {
                    sw = true;
                    continue;
                }
                else if (lineas[i] == '>')
                {
                    sw = false;
                    break;
                }
                if (sw == true)
                {
                    tipo += lineas[i];
                }

            }
            // }
            texto = texto.Trim();
            Console.WriteLine("Leyendo datos de fichero para procesar: '{0}'\n", texto);
        }
        public void manipularResultado()
        {
            try
            {

                if (tipo == "")
                {
                    return;
                }
                else
                {

                    textoresultado = lineas.Replace(tipo, request);

                    borrador = lineas.Replace(texto, "");
                    using (StreamWriter escritor = new StreamWriter("resultado.txt"))
                    {

                        escritor.WriteLine(textoresultado);
                    }

                    //byte[] info = new UTF8Encoding(true).GetBytes(textoresultado);
                    //fs.Write(info, 0, info.Length);
                    listaPalabrasCliente.Add(request);
                }


            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        public void recibir()
        {
            Console.WriteLine("Conectando servidor");
            using (var ServidorStream = new NamedPipeServerStream("pipes"))
            {
                ServidorStream.WaitForConnection();
                Console.WriteLine("Pipe servidor esperando datos\n");
                byte[] buffer = new byte[255];
                ServidorStream.Read(buffer, 0, 255);
                request = ASCIIEncoding.ASCII.GetString(buffer);
                request = request.Trim('\0');
                Console.WriteLine(request);
                if (request == "salir")
                {
                    Environment.Exit(0);
                }
                // path = request + ".txt";
            }

            // Console.WriteLine(request);
            path = request + ".txt";
            if (File.Exists(path))
            {
                Console.WriteLine("Tubo servidor recibiendo datos: 'N {0}'\n", request);
                Console.WriteLine("Apertura de fichero:{0}\n", request);

                return;
            }
            else
            {
                Console.WriteLine("No se encuentra el fichero");
            }
        }
        public void enviar()
        {
            using (var ServidorStream = new NamedPipeServerStream("pipes"))
            {
                ServidorStream.WaitForConnection();
                Console.WriteLine("Tubo servidor procesando datos: '" + texto + "'\n");
                byte[] buffer = ASCIIEncoding.ASCII.GetBytes(tipo);
                ServidorStream.Write(buffer, 0, buffer.Length);
                listaPalabrasServidor.Add(tipo);
            }
        }
        public void enviarPalabra2()
        {
            using (var ServidorStream = new NamedPipeServerStream("pipes"))
            {
                ServidorStream.WaitForConnection();
                byte[] buffer = ASCIIEncoding.ASCII.GetBytes(tipo);
                ServidorStream.Write(buffer, 0, buffer.Length);
                listaPalabrasServidor.Add(tipo);
            }
        }
        public void recibirPalabra()
        {
            using (var ServidorStream = new NamedPipeServerStream("pipes"))
            {
                ServidorStream.WaitForConnection();
                Console.WriteLine("Tubo servidor emitiendo datos: '" + tipo + "'\n");
                byte[] buffer = new byte[255];
                ServidorStream.Read(buffer, 0, 255);
                request = ASCIIEncoding.ASCII.GetString(buffer);
                request = request.Trim('\0');
                Console.WriteLine("Tubo Servidor recibiendo datos: 'P {0}'\n", request);
            }
        }
        public void enviarResultado()
        {
            using (var ServidorStream = new NamedPipeServerStream("pipes"))
            {
                int p1 = 0;
                int p2 = 0;
                int lista = 0;
                for (int o = 0; o < guardar.Length - 1; ++o)
                {
                    if (guardar[o] == '<') { p1 = o; }
                    else if (guardar[o] == '>') { p2 = o; }
                    if (p1 > 0 && p2 > 0)
                    {
                        guardar.Remove(p1, p2 - p1);
                        guardar = guardar.Replace("<>", listaPalabrasCliente[lista]);
                        p1 = 0; p2 = 0; lista = 0;

                    }
                }
          
                ServidorStream.WaitForConnection();
                Console.WriteLine("Tubo servidor emitiendo datos cuento");

                using (StreamWriter escritor = new StreamWriter(ServidorStream))
                {

                    escritor.WriteLine(guardar);
                }
                // listaPalabrasCliente.Add(request);



            }
        }
        public void envioContarPalabras()
        {
            using (var ServidorStream = new NamedPipeServerStream("pipes"))
            {
                try
                {
                    string cuentostr = File.ReadAllText(path);
                    for (int i = 0; i < cuentostr.Length; ++i)
                    {
                        if (cuentostr[i] == '<')
                        {
                            contadorint++;
                        }
                    }
                    contadorstr = contadorint.ToString();
                    ServidorStream.WaitForConnection();
                    byte[] buffer = ASCIIEncoding.ASCII.GetBytes(contadorstr);
                    ServidorStream.Write(buffer, 0, buffer.Length);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
        }
        public void index()
        {
            for (int i = 0; i < i + 1; ++i)
            {
                resetVariables();
                recibir();//1
                envioContarPalabras();//2
                manipularFichero();
                guardar = lineas;
                enviar();//3

                recibirPalabra();//4

                manipularResultado();


                manipularFichero2();

                // for (int j = 0; j < contadorint-1; ++j)
                // {
                enviarPalabra2();//5

                recibirPalabra();//6

                manipularResultado();

                manipularFichero2();
                // }

                enviarResultado();
                Console.WriteLine("-----lin: " + lineas);
                Console.WriteLine("-----tex: " + texto);
                Console.WriteLine("-----texres: " + textoresultado);
                Console.WriteLine("-----req: " + request);
                Console.WriteLine("-----pat: " + path);
                Console.WriteLine("-----tip: " + tipo);
                Console.WriteLine("-----bor: " + borrador);
                Console.WriteLine("-----gua: " + guardar);





            }
        }
    }
}
/*
System.Environment.Exit(0);
while (true)
{
    try
    {
        _pipeServer.WaitForConnection();
        break;
    }
    catch (IOException)
    {
        _pipeServer.Disconnect();
        continue;
    }            
 }
               Console.WriteLine("-----lin: " + lineas);
                Console.WriteLine("-----tex: " + texto);
                Console.WriteLine("-----texres: " + textoresultado);
                Console.WriteLine("-----req: " + request);
                Console.WriteLine("-----pat: " + path);
                Console.WriteLine("-----tip: " + tipo);
                Console.WriteLine("-----bor: " + borrador);
                Console.WriteLine("-----gua: " + guardar);
            Thread.Sleep(2000);
 */