﻿/*

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using Utils;

namespace Client
{
    public partial class Form1 : Form
    {
        private readonly TcpClient _client = new TcpClient();
        private TreeNode _tree;
        private List<FilesInfo> _files = new List<FilesInfo>();
        private static FilesInfo _selectedFile;
        private NetworkStream _serverStream;
        private byte[] inStream = new byte[10025];
        private bool downloadEnabled = false;
        private SaveFileDialog dialog;
        private Thread thread;

        public Form1()
        {
            InitializeComponent();

            try
            {
                IPAddress ip = IPAddress.Parse("127.0.0.1");
                IPEndPoint ep = new IPEndPoint(ip, 13000);
                _client.Connect(ep);
                if (_client.Available != 0)
                    throw new Exception("Destino no disponible");
                if (_client.Connected) ConnectBtn.Text = @"Conectado";
                //thread = new Thread(ReadFromServer);
                //thread.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            if (_client.Connected)
            {
                _client.Close();
                ConnectBtn.Text = @"Conectando";
            }
            try
            {
                IPAddress ip = IPAddress.Parse(IpTb.Text);
                IPEndPoint ep = new IPEndPoint(ip, Convert.ToInt32(PortTb.Text));
                _client.Connect(ep);
                if (_client.Available != 0)
                    throw new Exception("Destino no disponible");


                if (_client.Connected) ConnectBtn.Text = @"Conectado";
                thread = new Thread(ReadFromServer);
                thread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

        private void ReadFromServer()
        {
            _serverStream = _client.GetStream();
            inStream = new byte[_client.ReceiveBufferSize];

            try
            {
               bool fileDirectoryFlag = false;
                while (_client.Connected)
                {
                    if (!fileDirectoryFlag)
                    {
                       fileDirectoryFlag = GetFileDirectory();
                    }

                    if (downloadEnabled)
                    {
                        inStream = new byte[_client.ReceiveBufferSize];
                        int i = _serverStream.Read(inStream, 0, _client.ReceiveBufferSize);

                        if (DialogResult.OK == (new Invoker(dialog).Invoke()))
                        {
                            Stream fileStream = dialog.OpenFile();
                            fileStream.Write(inStream, 0, i);
                            string fs = ((System.IO.FileStream)fileStream).Name;


                            fileStream.Close();
                            downloadEnabled = false;
                            MessageBox.Show("Archivo descargado en " + fs);

                        }
                        else
                        {
                            MessageBox.Show("Descarga cancelada.");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        
        private bool GetFileDirectory()
        {
            try
            {
                inStream = new byte[_client.ReceiveBufferSize];
                _serverStream.Read(inStream, 0, (int)_client.ReceiveBufferSize);
                string returndata = System.Text.Encoding.ASCII.GetString(inStream);
                _files = JsonConvert.DeserializeObject<List<FilesInfo>>(returndata);

                if (FilesTree.InvokeRequired)
                {
                    FilesTree.Invoke(new MethodInvoker(delegate
                    {
                        foreach (FilesInfo file in _files)
                        {
                            _tree.Nodes.Add(new TreeNode(file.FileName));
                        }

                    }));
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
        private void FilesTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var a = FilesTree.SelectedNode.Index;
            _selectedFile = _files[a];
        }
        
        private void DownloadBtn_Click(object sender, EventArgs e)
        {
            FileRequest fileRequest = new FileRequest
            {
                Path = _selectedFile.AbsolutePath
            };
            string requestedFile = JsonConvert.SerializeObject(fileRequest);

            byte[] outsteam = Encoding.ASCII.GetBytes(requestedFile);
            _serverStream.Write(outsteam, 0, outsteam.Length);
            _serverStream.Flush();
            downloadEnabled = true;

            dialog = new SaveFileDialog();
            dialog.DefaultExt = _selectedFile.Extension;


        }

        private void IpTb_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void btn_fotomonte_Click(object sender, EventArgs e)
        {
            FileRequest fileRequest = new FileRequest
            {
                Path = @"..\..\fotos\FotoMonte.jpg"
            };
            string requestedFile = JsonConvert.SerializeObject(fileRequest);

            byte[] outsteam = Encoding.ASCII.GetBytes(requestedFile);
            _serverStream.Write(outsteam, 0, outsteam.Length);
            _serverStream.Flush();
            downloadEnabled = true;

            dialog = new SaveFileDialog();
            dialog.DefaultExt = ".jpg";

        }

        private void btn_fotociudad_Click(object sender, EventArgs e)
        {
            FileRequest fileRequest = new FileRequest
            {
                Path = @"..\..\fotos\FotoCiudad.jpg"
            };
            string requestedFile = JsonConvert.SerializeObject(fileRequest);

            byte[] outsteam = Encoding.ASCII.GetBytes(requestedFile);
            _serverStream.Write(outsteam, 0, outsteam.Length);
            _serverStream.Flush();
            downloadEnabled = true;

            dialog = new SaveFileDialog();
            dialog.DefaultExt = ".jpg";
        }

        private void btn_fotoplaya_Click(object sender, EventArgs e)
        {
            FileRequest fileRequest = new FileRequest
            {
                Path = @"..\..\fotos\FotoPlaya.jpg"
            };
            string requestedFile = JsonConvert.SerializeObject(fileRequest);

            byte[] outsteam = Encoding.ASCII.GetBytes(requestedFile);
            _serverStream.Write(outsteam, 0, outsteam.Length);
            _serverStream.Flush();
            downloadEnabled = true;

            dialog = new SaveFileDialog();
            dialog.DefaultExt = ".jpg";
        }
    }

}

*/



using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Utils;

namespace Cliente
{
    public partial class Form1 : Form
    {
        private readonly TcpClient _client = new TcpClient();
        private TreeNode _tree;
        //private List<FilesInfo> _files = new List<FilesInfo>();
        //private static FilesInfo _selectedFile;
        private NetworkStream _serverStream;
        private byte[] inStream = new byte[10025];
        private bool downloadEnabled = false;
        private SaveFileDialog dialog;
        private Thread thread;

        public Form1()
        {
            InitializeComponent();
            //_tree = new TreeNode("Servidor");

            //FilesTree.Nodes.Add(_tree);

            try
            {
                IPAddress ip = IPAddress.Parse("127.0.0.1");
                IPEndPoint ep = new IPEndPoint(ip, 13000);
                _client.Connect(ep);
                if (_client.Available != 0)
                    throw new Exception("Destino no disponible");
                if (_client.Connected) ConnectBtn.Text = @"Conectado";
                thread = new Thread(ReadFromServer);
                thread.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /*
        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            if (_client.Connected)
            {
                _client.Close();
                ConnectBtn.Text = @"Conectar";
            }
            try
            {
                IPAddress ip = IPAddress.Parse("127.0.0.1");
                IPEndPoint ep = new IPEndPoint(ip, Convert.ToInt32("13000"));
                _client.Connect(ep);
                if (_client.Available != 0)
                    throw new Exception("Destino no disponible");


                if (_client.Connected) ConnectBtn.Text = @"Desconectar";
                thread = new Thread(ReadFromServer);
                thread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        */

        private void ReadFromServer()
        {
            _serverStream = _client.GetStream();
            inStream = new byte[_client.ReceiveBufferSize];

            try
            {
                bool fileDirectoryFlag = false;
                while (_client.Connected)
                {
                    if (!fileDirectoryFlag)
                    {
                        fileDirectoryFlag = GetFileDirectory();
                    }

                    if (downloadEnabled)
                    {
                        inStream = new byte[_client.ReceiveBufferSize];
                        int i = _serverStream.Read(inStream, 0, _client.ReceiveBufferSize);

                        if (DialogResult.OK == (new Invoker(dialog).Invoke()))
                        {
                            Stream fileStream = dialog.OpenFile();
                            fileStream.Write(inStream, 0, i);
                            string fs = ((System.IO.FileStream)fileStream).Name;


                            fileStream.Close();
                            downloadEnabled = false;
                            MessageBox.Show("Archivo descargado en " + fs);

                        }
                        else
                        {
                            MessageBox.Show("Descarga cancelada.");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private bool GetFileDirectory()
        {
            try
            {
                inStream = new byte[_client.ReceiveBufferSize];
                _serverStream.Read(inStream, 0, (int)_client.ReceiveBufferSize);
                string returndata = System.Text.Encoding.ASCII.GetString(inStream);
                //_files = JsonConvert.DeserializeObject<List<FilesInfo>>(returndata);
                /*
                if (FilesTree.InvokeRequired)
                {
                    FilesTree.Invoke(new MethodInvoker(delegate
                    {
                        foreach (FilesInfo file in _files)
                        {
                            _tree.Nodes.Add(new TreeNode(file.FileName));
                        }

                    }));
                    return true;
                }*/
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                return false;
            }
        }
        /*
        private void FilesTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var a = FilesTree.SelectedNode.Index;
            //Info.Text = _files[a].AbsolutePath;
            _selectedFile = _files[a];
        }
        
        private void DownloadBtn_Click(object sender, EventArgs e)
        {
            FileRequest fileRequest = new FileRequest
            {
                Path = _selectedFile.AbsolutePath
            };
            string requestedFile = JsonConvert.SerializeObject(fileRequest);

            byte[] outsteam = Encoding.ASCII.GetBytes(requestedFile);
            _serverStream.Write(outsteam, 0, outsteam.Length);
            _serverStream.Flush();
            downloadEnabled = true;

            dialog = new SaveFileDialog();
            dialog.DefaultExt = _selectedFile.Extension;
        }

        */
        private void btn_fotomonte_Click(object sender, EventArgs e)
        {
            //FileRequest fileRequest = new FileRequest
            //{
            //    Path = @"..\..\fotos\FotoMonte.jpg"
            //};
            string requestedFile = @"..\..\fotos\FotoMonte.jpg";// JsonConvert.SerializeObject(fileRequest);

            byte[] outsteam = Encoding.ASCII.GetBytes(requestedFile);
            _serverStream.Write(outsteam, 0, outsteam.Length);
            _serverStream.Flush();
            downloadEnabled = true;

            dialog = new SaveFileDialog();
            dialog.DefaultExt = ".jpg";

        }
        
        private void btn_fotociudad_Click(object sender, EventArgs e)
        {
            /*
               _serverStream.Flush();
            FileRequest fileRequest = new FileRequest { Path = @"..\\..\\fotos\\FotoCiudad.jpg" };
            string requestedFile = JsonConvert.SerializeObject(fileRequest);
            Console.WriteLine(requestedFile);
            byte[] outsteam = Encoding.ASCII.GetBytes(requestedFile);
            _serverStream.Write(outsteam, 0, outsteam.Length);
            Console.WriteLine(outsteam);
            Console.WriteLine(outsteam.Length);


            downloadEnabled = true;

            dialog = new SaveFileDialog();
            dialog.DefaultExt = ".jpg"; */
        }

        private void btn_fotoplaya_Click(object sender, EventArgs e)
        {
            /*
               FileRequest fileRequest = new FileRequest
            {
                Path = @"..\..\fotos\FotoPlaya.jpg"
            };
            string requestedFile = JsonConvert.SerializeObject(fileRequest);

            byte[] outsteam = Encoding.ASCII.GetBytes(requestedFile);
            _serverStream.Write(outsteam, 0, outsteam.Length);
            _serverStream.Flush();
            downloadEnabled = true;

            dialog = new SaveFileDialog();
            dialog.DefaultExt = ".jpg"; */
        }

        private void ConnectBtn_Click(object sender, EventArgs e)
        {

        }
    }

}
