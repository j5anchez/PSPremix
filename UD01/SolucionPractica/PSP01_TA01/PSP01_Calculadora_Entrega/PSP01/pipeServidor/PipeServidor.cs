using System;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;

namespace pipeServidor
{
    class PipeServidor
    {
        static void Main(string[] args)
        {
            try
            {
                var server = new NamedPipeServerStream("PSPtarea1");
                server.WaitForConnection();
                Console.WriteLine("Conexión a servidor establecida.");
                Console.WriteLine("Indica el nombre del cuento elegido:");
                StreamReader reader = new StreamReader(server);
                StreamWriter writer = new StreamWriter(server);
                while (true)
                {
                    var line = reader.ReadLine();
                    Console.WriteLine("Pipe Servidor procesando datos: '{0}'",line);
                    string path = line + ".txt";
                    string texto = System.IO.File.ReadAllText(@path);
                    Console.WriteLine(texto);

                    float resultado = ProcesaOperador(line);
                    writer.WriteLine(resultado.ToString());
                    Console.WriteLine("Pipe Servidor datos enviados: '{0}'", resultado);
                    writer.Flush();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}  Apangado servidor por error", e.Message);
            }
        }

        private static float ProcesaOperador(string operador)
        {
            float resultado = 0;
            float op1=float.MaxValue;
            float op2=float.MaxValue;
            string[] datOperador = operador.Split(' ');
            if (!Single.TryParse(datOperador[1], out op1))
            {
                Console.WriteLine("No se puede parsear a numero el operando 1 '{0}'.", op1);
            }
            if (!Single.TryParse(datOperador[2], out op2))
            {
                Console.WriteLine("No se puede parsear a numero el operando 2 '{0}'.", op2);
            }

            Console.WriteLine("Pipe Servidor operación: '{0} {1} {2}'", op1, datOperador[0], op2);
            if ("+".Equals(datOperador[0]))
            {
                resultado = op1 + op2;

            }
            else if ("-".Equals(datOperador[0]))
            {
                resultado = op1 - op2;

            }
            else if ("*".Equals(datOperador[0]))
            {
                resultado = op1 * op2;
            }
            else if ("/".Equals(datOperador[0]))
            {
                resultado = op1 / op2;
            }
            else if ("^".Equals(datOperador[0]))
            {
                resultado = 1;
                for (int i= 0; i < op2;i++)
                    resultado *= op1;
            }
            //Console.WriteLine("Ret: {0}", resultado);
            return resultado;
        }
    }
}
