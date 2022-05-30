using System;
using System.IO;
using System.IO.Pipes;

class PipeServer
{
    static void Main()
    {
        using (NamedPipeServerStream pipeServer =
            new NamedPipeServerStream("testpipe"))
        {
            Console.WriteLine("Conexion a servidor establecida.");

            pipeServer.WaitForConnection();
            Console.WriteLine("Pipe Servidor esperando datos.\n");
            

            using (StreamReader sr = new StreamReader(pipeServer))
            {
                string temp;
                string path = "C:\\Users\\34603\\Desktop\\trabajosDAM\\PSP\\UD01\\PipeServidorCliente\\";
                string lineas;
                string texto="";
                string tipo = "";
                bool sw=false;
                int largo;

                // Display the read text to the console
                while ((temp = sr.ReadLine()) != null)
                {
                    Console.WriteLine("Tubo servidor recibiendo datos: 'N {0}'\n", temp);
                    temp += ".txt";
                    Console.WriteLine("Apertura de fichero:{0}\n",temp);
                    if (File.Exists(path + temp))
                    {
                        StreamWriter escritorS = new StreamWriter(pipeServer);

                        StreamReader abrircuento = new StreamReader(path + temp);
                        Console.WriteLine("Fichero abierto. " + temp+"\n");
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
                                Console.WriteLine("Tubo servidor procesando datos: '" + texto + "'\n");

                                //pipeServer.Write(tipo);
                                escritorS.WriteLine(tipo);
                                Console.WriteLine("Tubo servidor emitiendo datos: '" + tipo + "'\n");


                                Console.ReadKey();

                                tipo = "";



                            }
                            if (sw == true)
                            {
                                tipo += lineas[i];
                            }

                        }


                    }
                    else
                    {
                        Console.WriteLine("El fichero no existe");
                    }

                }

            }

           
        }
        Console.ReadKey();



        using (NamedPipeServerStream pipeServer2 =
            new NamedPipeServerStream("conex2"))
        {
            pipeServer2.WaitForConnection();

            using (StreamWriter sw2 = new StreamWriter(pipeServer2))
               {
                   sw2.AutoFlush = true;
                   Console.Write("pepito");
                   sw2.WriteLine(Console.ReadLine());
               }
           


        }

    }
}