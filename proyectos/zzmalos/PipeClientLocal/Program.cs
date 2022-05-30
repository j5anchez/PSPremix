using System;
using System.IO;
using System.IO.Pipes;

class PipeClient
{
    static void Main(string[] args)
    {
        using (NamedPipeClientStream pipeClient =
            new NamedPipeClientStream("testpipe"))
        {
            string tipo;
            Console.WriteLine("Estableciendo conexion con el servidor\n");
            pipeClient.Connect();

            using (StreamWriter sw = new StreamWriter(pipeClient))
            {

                sw.AutoFlush = true;
                Console.WriteLine("Indica el nombre del cuento elegido:\n");
                string cuento = "cuento1"; // = Console.ReadLine();
                sw.WriteLine(cuento);
                Console.WriteLine("Tubo cliente procesando datos: 'N " + cuento + "'");

                pipeClient.Flush();
                //pipeClient.Close();


                /*string tipo;
                    while ((tipo = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(tipo + ":");
                    }*/
                Console.ReadKey();
            
                Console.WriteLine("hola");



                
            }




        }


       

           
    }
}