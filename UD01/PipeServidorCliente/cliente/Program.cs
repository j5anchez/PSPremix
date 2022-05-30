
/*Funcionalidad parte de comunicación con el usuario(Parte Cliente):
PSP01 TA01 REALIZAR UN PROGRAMA CON PROCESOS
TAREA EVALUATIVA
Creará conexión con el programa servidor mediante un pipe con nombre. Mostrando por consola el momento del
establecimiento de la conexión.
El programa solicitará el nombre del cuento.
cuentoX donde X es el número de cuento. 
Se enviará el nombre del cuento al programa servidor.
El programa cliente se  queda a la espera de recibir información por el pipe.
El programa cliente comienza a recibir información sobre huecos a rellenar en el cuento. Durante este proceso, no debe
mostrarse el cuento al usuario.
El cliente pedirá al usuario la información solicitada por el servidor. 
Recoge la información del usuario por consola y se la envía al servidor. Lo recogido por consola puede ser una o varias palabras,
incluso que no haya propuesta por parte del usuario y recoja una línea vacía.
Una vez el servidor haya recibido toda la información solicitada y la procese, responde con el resultado del cuento  y el programa
cliente muestra el cuento por consola.
2122_DAM_PSP_EE: PSP01 TE(100%) Programación Multiprocesos https://ikastaroak.birt.eus/mod/assign/view.php?id=183339
1 de 4 04 / 02 / 2022, 0:39
Funcionalidad de cálculos internos (Parte Servidor):
RECURSOS
INDICACIONES DE ENTREGA
Vuelve a repetirse el proceso completo y se solicita nuevamente un nombre de cuento.
El programa cliente durante la ejecución mostrará la información que está enviando al servidor y la que está recibiendo por
consola, para que el usuario o usuaria perciba que todo está funcionando correctamente.*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;

namespace cliente
{
    class Program
    {



		public static void cliente11()
			{

			string cuento = "";
			string palabra2;
			string palabra3;
			string escrita1 = "";
			string escrita2;
			string escrita3;



			NamedPipeClientStream cliente1 = new NamedPipeClientStream("servidor");

			cliente1.Connect();
			StreamWriter escritorC = new StreamWriter(cliente1);
			StreamReader lectorC = new StreamReader(cliente1);
			Console.WriteLine("Estableciendo conexion con el servidor");
			Thread.Sleep(1000);
			Console.WriteLine("\nIndica el nombre del cuento elegido:");


			cuento = "cuento1"; // Console.ReadLine();	
			escritorC.Write(cuento);
			Console.WriteLine("Tubo cliente procesando datos: 'N " + cuento + "'");
			Thread.Sleep(1000);
			escritorC.Close();
			lectorC.Close();
			cliente1.Close();
			}

		public static void cliente12() {

			NamedPipeClientStream cliente2 = new NamedPipeClientStream("servidor");

			cliente2.Connect();
			StreamWriter escritorC = new StreamWriter(cliente2);
			StreamReader lectorC = new StreamReader(cliente2);
			//escrita1=lectorC.ReadToEnd();           
			//	Console.ReadKey();

			//Console.WriteLine(escrita1);
			//Console.ReadKey();


			//	escritorC.Flush();

			//	Console.WriteLine(lectorC.ReadLine()+"'");

			//	palabra1 = "viajar"; // Console.ReadLine();	
			string palabra1 = "";

			escritorC.Dispose();
			escritorC.Write(palabra1);
			Console.WriteLine("Tubo cliente procesando datos: 'P " + palabra1 + "'");
			Console.ReadKey();


		


			Console.WriteLine("presione una tecla para terminar.");
			Console.ReadKey();

			cliente2.Close();

			}

		static void Main(string[] args)
        {

			cliente11();
			//cliente12();

			}
		}
}