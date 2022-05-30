using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace criptado
{
    public class Criptado
    {
        public void generarClaves(string username)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                string pathPublic = @"..\..\publickeys\" + username + "_public.xml";
                string pathPrivate = @"..\..\privatekeys\" + username + "_private.xml";
                rsa.PersistKeyInCsp = false;

                if (File.Exists(pathPublic))
                    File.Delete(pathPublic);
                if (File.Exists(pathPrivate))
                    File.Delete(pathPrivate);


                string publicKey = rsa.ToXmlString(false);
                File.WriteAllText(pathPublic, publicKey);

                string privateKey = rsa.ToXmlString(true);
                File.WriteAllText(pathPrivate, privateKey);

            }
        }

        public byte[] encriptar(string publicKF, byte[] textoPlano)
        {
            byte[] encriptado;

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = true;

                string publicKey = File.ReadAllText(publicKF);

                //FromXmlString(publicKey): Inicializa un objeto RSA de la información de clave de una cadena XML.
                rsa.FromXmlString(publicKey);

                //Cifra los datos con el algoritmo RSA.
                //@textoPlano: datos que se van a cifrar
                //@Booleano: true para realizar el cifrado RSA directo mediante el relleno de OAEP (solo disponible en equipos con Windows XP o versiones posteriores como en nuestro caso); de lo contrario, false para usar el relleno PKCS#1 v1.5.
                encriptado = rsa.Encrypt(textoPlano, true);

            }

            //Valor que se devuelve
            return encriptado;

        }



        public byte[] Desencriptar(string privateKF, byte[] textoEncriptado)
        {

            byte[] desencriptado;
            //Se crea un objeto de tipo RSACryptoServiceProvider para poder hacer uso de sus métodos.
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                //Le indicamos el valor a false porque no queremos que esté en ningún proveedor de servicios.
                rsa.PersistKeyInCsp = false;


                //Lee el contenido del fichero y lo guarda en un string
                string privateKey = File.ReadAllText(privateKF);

                //FromXmlString(false): Inicializa un objeto RSA de la información de clave de una cadena XML.
                //En este caso clave privada ya que la utilizaremos para descifrar
                rsa.FromXmlString(privateKey);

                //Descifra los datos que se cifraron anteriormente.
                //@textoEncriptado: Datos que se van a descifrar.
                //@Booleano: true para realizar el cifrado RSA directo mediante el relleno de OAEP (solo disponible en equipos con Windows XP o versiones posteriores como en nuestro caso); de lo contrario, false 
                desencriptado = rsa.Decrypt(textoEncriptado, true);

            }
            return (desencriptado);

        }

    }
}
