using System;
using System.Text;
using System.IO.Pipes;
namespace PipeCliente
{
    class Program
    {
        public static void callBack(NamedPipeServerStream pipe)
        {
            while (true)
            {
                pipe.WaitForConnection();
                StreamReader sr = new StreamReader(pipe);
                Console.WriteLine(sr.ReadToEnd());
                pipe.Disconnect();
            }
        }
        public static void Main(string[] args)
        {
            NamedPipeServerStream s = new NamedPipeServerStream("p", PipeDirection.In);
            Action<NamedPipeServerStream> a = callBack;
            a.BeginInvoke(s, ar => { }, null);
            


    
        }
  

    }
}