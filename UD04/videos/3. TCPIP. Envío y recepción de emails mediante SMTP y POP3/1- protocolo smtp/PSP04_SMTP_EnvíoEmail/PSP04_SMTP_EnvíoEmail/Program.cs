using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace FTP
{
    class Program
    {
        public static void Main(String[] args)
        {
            Random rnd = new Random();
            int ms;
            //Dirección de cuenta desde la cual se quiere enviar un correo electrónico
            MailAddress origen = new MailAddress("pepitopiscinascooperativa@gmail.com", "desde la casa de tu puta madre");

                //Dirección de cuenta a la cual se quiere enviar un correo electrónico
                MailAddress destino = new MailAddress("t0k3r0@gmail.com", "t0k3r0");

                //Se especifica información del servidor, protocolo, credenciales, ...de la conexión que se va a realizar
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    Credentials = new NetworkCredential(origen.Address, "lamasputamagda"),
                    EnableSsl = true

                };
            for (int i = 0; i < 50; i++)
            {

                ms=rnd.Next(1000,3000);
                Thread.Sleep(ms);

                //Se escribe el mensage que vamos a enviar indicando cual será el receptor y el emisor
                using (MailMessage mensaje = new MailMessage(origen, destino)
                {
                    Subject = "PSP04",
                    Body = i+"Hola, espero que este sea el primer mensaje de muchos"+i
                })
                
                    //Se ejecuta el envío del mensaje.
                    try
                    {


                        smtp.Send(mensaje);

                    }
                    catch (Exception ex)
                    {
                        //En caso de error se muestra por la consola.
                        Console.WriteLine(ex.ToString());
                    }

            }
        }
    }
}
