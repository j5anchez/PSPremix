using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;




namespace PSP05_TE_GestorPass
{
    public partial class Form1 : Form
    {
        criptado.Criptado cc = new criptado.Criptado();
        string textoPlano = string.Empty;



        public Form1()
        {
            InitializeComponent();
        }

        private void boton_Registrar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreUsuario = tb_UsuarioRegistrado.Text;
                string curFile = @"..\..\bbdd\" + nombreUsuario + ".txt";
                if (File.Exists(curFile))
                {
                    check_BorrarPass.Enabled = true;
                    check_RegistrarPass.Enabled = true;
                    check_VisualizarPass.Enabled = true;
                }
                else
                {
                    MessageBox.Show("El usuario no existe, debes registrarlo.");
                    gb_RegistroUsuario.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }

        }

        private void boton_RegistroAceptar_Click(object sender, EventArgs e)
        {
            bool emailCorrecto = false;
            char c;
            bool arroba = false;
            for (int i = 0; i < tb_Email.Text.Length; ++i)
            {
                if (i == tb_Email.Text.Length - 1 && arroba == true) { emailCorrecto = true; }

                c = tb_Email.Text[i];
                if ((c > 44 && c < 58) || (c > 64 && c < 91) || (c > 96 && c < 123))
                {
                    continue;
                }
                else if (c == '@' && arroba == false)
                {
                    arroba = true;
                }
                else
                {
                    break;
                }
            }
            if (radio_RegistrarYes.Checked && emailCorrecto)
            {

                cc.generarClaves(tb_UsuarioRegistrado.Text);
                using (StreamWriter sr = new StreamWriter(@"..\..\bbdd\" + tb_UsuarioRegistrado.Text + ".txt"))
                {
                    sr.WriteLine("");
                }
                /* string pathPrivate = @"..\..\privatekeys\" + tb_Usuario.Text + "_private.xml";
                 string pathBBDD = @"..\..\bbdd\" + tb_Usuario.Text + ".txt";

                 try
                 {
                     using (FileStream fs = File.Create(pathPrivate))
                     {
                         byte[] info = new UTF8Encoding(true).GetBytes("");
                         fs.Write(info, 0, info.Length);
                     }
                     using (FileStream fs = File.Create(pathBBDD))
                     {
                         byte[] info = new UTF8Encoding(true).GetBytes("");
                         fs.Write(info, 0, info.Length);
                     }

                 }

                 catch (Exception ex)
                 {
                     Console.WriteLine("Exception: " + ex.Message);
                 }
                */
                gb_RegistroUsuario.Enabled = false;
                while (!File.Exists(@"..\..\privatekeys\" + tb_UsuarioRegistrado.Text + "_private.xml"))
                {
                    Thread.Sleep(200);
                }
                Correos.envioCorreos enviarCorreo = new Correos.envioCorreos();

                enviarCorreo.envioEmail(tb_UsuarioRegistrado.Text.ToString(), tb_Email.Text.ToString());

                MessageBox.Show("Registro Creado, puedes encontrar tu fichero en ..\\..\\privatekeys\\" + tb_UsuarioRegistrado.Text + "_private.xml");
            }
        }


        private void boton_Guardar_Click(object sender, EventArgs e)
        {
            string pathPublic = @"..\..\publickeys\" + tb_UsuarioRegistrado.Text + "_public.xml";
            byte[] bytextoCifrado = cc.encriptar(pathPublic, Encoding.UTF8.GetBytes(this.tb_RegistrarDescripcion.Text + ", " + tb_RegistrarPassword.Text));

            using (StreamWriter sw = File.AppendText(@"..\..\bbdd\" + tb_UsuarioRegistrado.Text + ".txt"))
            {
                sw.WriteLine(Encoding.UTF8.GetString(bytextoCifrado).ToLower().Replace("-", ""));
            }
            /*
            if (tb_RegistrarDescripcion.Text.Length > 0 && tb_RegistrarPassword.Text.Length > 0)
            {
                string nuevaDescripcion = string.Empty;
                char c;
                bool mayus = false;
                bool minus = false;
                bool numero = false;
                bool longitud = false;
                bool caracter = false;
                string caracteres = "!@#&()–[{}:',?/*~$^+=<>";
                for (int i = 0; i < tb_RegistrarDescripcion.Text.Length; ++i)
                {
                    c = tb_RegistrarDescripcion.Text[i];
                    if (c == 32) { c = '_'; }
                    nuevaDescripcion += c;
                }
                if (tb_RegistrarPassword.Text.Length > 7 && tb_RegistrarPassword.Text.Length < 11)
                {
                    longitud = true;
                    for (int i = 0; i < tb_RegistrarPassword.Text.Length; ++i)
                    {
                        c = tb_RegistrarPassword.Text[i];
                        if (c >= 'A' && c <= 'Z') { mayus = true; }
                        else if (c >= 'a' && c <= 'z') { minus = true; }
                        else if (c >= '0' && c <= '9') { numero = true; }
                        else if (caracteres.Contains(c)) { caracter = true; }
                    }
                }
                if (!longitud || !mayus || !minus || !numero || !caracter)
                {

                    MessageBox.Show("La contraseña al menos tiene que contener\n1 mayúscula\n1 minúscula\n1 número\n8-10 caracteres de longitud\n1 caracter: !@#&()–[{}:',?/*~$^+=<>");
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(@"..\..\bbdd\" + tb_Usuario.Text + ".txt"))
                    {
                        sw.WriteLine(tb_RegistrarDescripcion.Text + " " + tb_RegistrarPassword.Text + "\n");

                    }
                }
            }*/
        }

        private void check_VisualizarPass_CheckedChanged(object sender, EventArgs e)
        {
            if (check_VisualizarPass.Checked)
            {
                gb_Visualizar.Enabled = true;
                string pathPrivate = @"..\..\privatekeys\" + tb_UsuarioRegistrado.Text + "_private.xml";
                string pathBBDD = @"..\..\bbdd\" + tb_UsuarioRegistrado.Text + ".txt";
                string lineConcatenado = File.ReadAllText(pathBBDD);
            
                byte[] textoEncriptado = Encoding.UTF8.GetBytes(lineConcatenado);
                byte[] textoDesencriptado = cc.Desencriptar(pathPrivate, textoEncriptado);
                //string textoDesencriptadoString = Encoding.UTF8.GetString(textoDesencriptado, 0, textoDesencriptado.Length);
                string textoDesencriptadoString = Encoding.UTF8.GetString(textoDesencriptado).ToLower().Replace("-", "");

                MessageBox.Show(textoDesencriptadoString);

                /*
                List<string> lineas = new List<string>();
                List<string> passwords = new List<string>();
                try
                {
                    using (StreamReader sr = new StreamReader(@"..\..\bbdd\" + tb_Usuario.Text + ".txt"))
                    {
                        while (true)
                        {
                            string line = sr.ReadLine();
                            if (line == null) break;
                            bool sw = false;
                            string mostrarDescripcion = string.Empty;
                            string mostrarPassword = string.Empty;
                            for (int i = 0; i < line.Length; ++i)
                            {
                                if (line[i] == ' ') { sw = true; continue; }

                                if (!sw)
                                    mostrarDescripcion += line[i];
                                else
                                    mostrarPassword += line[i];
                            }

                            lineas.Add(mostrarDescripcion);
                            passwords.Add(mostrarPassword);

                        }
                    }

                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                cb_VisualizarDescripcion.DataSource = lineas;
                */
            }
            else { gb_Visualizar.Enabled = false; }
        }

        private void check_RegistrarPass_CheckedChanged(object sender, EventArgs e)
        {
            if (check_RegistrarPass.Checked) { gb_Registrar.Enabled = true; }
            else { gb_Registrar.Enabled = false; }
        }

        private void check_BorrarPass_CheckedChanged(object sender, EventArgs e)
        {
            if (check_BorrarPass.Checked) { gb_Borrar.Enabled = true; }
            else { gb_Borrar.Enabled = false; }
        }


    }
}

