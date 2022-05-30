﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace FileTransfer
{
    public class Recibir
    {

        #region Fields and properties

        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.tcpclient.sendbuffersize?view=netframework-4.7.2
        /// </summary>
        private int _bufferSize = 8192;
        public int BufferSize
        {
            get { return _bufferSize; }
            set
            {
                lock (_lockReceiving)
                {
                    _bufferSize = value;
                }
            }
        }

        /// <summary>
        /// For progressbar in UI
        /// </summary>
        private double _progress;

        /// <summary>
        /// Locking listener
        /// </summary>
        private readonly object _lockReceiving = new object();

        #endregion

        #region Events

        public delegate void StatusMessageEventHandler(object sender, string message);
        public event StatusMessageEventHandler StatusMessage;

        public delegate void ProgressPercentEventHandler(object sender, double progress);
        public event ProgressPercentEventHandler ProgressPercent;

        public delegate void CleanupEventHandler(object sender, EventArgs args);
        public event CleanupEventHandler Cleanup;

        public delegate void NewFileReceivedEventHandler(object sender, string fileName);
        public event NewFileReceivedEventHandler NewFileReceived;

        public delegate void ChecksumErrorEventHandler(object sender, string message);
        public event ChecksumErrorEventHandler ChecksumError;

        #endregion

        /// <summary>
        /// Receiving file on specified ip and port. It will start listening for a connection
        /// and will stop listening after the transfer.
        /// </summary>
        /// <param name="ip">Local ip address to listen on</param>
        /// <param name="port">Local port to listen on</param>
        /// <param name="destinationFolder">Folder where the recieved file is going to be saved to</param>
        public void ReceiveFile(IPAddress ip, int port, string destinationFolder)
        {
            // lock to prevent bad things in case buffer size is changed
            lock (_lockReceiving)
            {
                try
                {
                    // Reset progress and update status
                    _progress = 0;
                    ProgressPercent?.Invoke(this, _progress);

                    // Listen for incoming coonnections
                    var listener = new TcpListener(ip, port);
                    listener.Start();

                    // Update status
                    StatusMessage?.Invoke(this, $"Listening on {ip}:{port}");

                    try
                    {
                        while (true)
                        {
                            using (var client = listener.AcceptTcpClient())
                            using (var stream = client.GetStream())
                            {
                                client.ReceiveBufferSize = BufferSize;
                                StatusMessage?.Invoke(this, "Receiving file...");

                                byte[] buffer = new byte[BufferSize];

                                // Get file info from first packet
                                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                                // Header: 4 bytes for file length (int) + 4 bytes for file name length (int) + fileName + 20 bytes checksum
                                int headerOffset = 0;
                                int fileLen = BitConverter.ToInt32(buffer, headerOffset);
                                headerOffset += sizeof(int);
                                int fileNameLen = BitConverter.ToInt32(buffer, headerOffset);
                                headerOffset += sizeof(int);
                                string fileName = Encoding.ASCII.GetString(buffer, headerOffset, fileNameLen);
                                headerOffset += fileNameLen;
                                byte[] checksumByte = new byte[20];
                                Array.Copy(buffer, headerOffset, checksumByte, 0, checksumByte.Length);
                                headerOffset += checksumByte.Length;
                                bool checksumAvailable = !checksumByte.All(singleByte => singleByte == 0);

                                // Update status and progress and calculate progress percent increment
                                StatusMessage?.Invoke(this, $"Receiving {fileName} on {ip}:{port} from {client.Client.RemoteEndPoint}");
                                double progressBarIncrement = 100.0 / (Convert.ToDouble(fileLen) / BufferSize);
                                int progressSingleIncrement = 0;
                                _progress += progressBarIncrement;
                                ProgressPercent?.Invoke(this, _progress);

                                // Create file to safe incoming data to
                                using (var output = File.Create(Path.Combine(destinationFolder, fileName)))
                                {
                                    output.Write(buffer, headerOffset, bytesRead - headerOffset);
                                    if (bytesRead >= buffer.Length)
                                    {
                                        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                                        {
                                            output.Write(buffer, 0, bytesRead);

                                            // Update progress only every percent
                                            _progress += progressBarIncrement;
                                            if (!(_progress > progressSingleIncrement + 1)) continue;
                                            progressSingleIncrement = Convert.ToInt32(_progress);
                                            ProgressPercent?.Invoke(this, progressSingleIncrement);
                                        }
                                    }
                                }

                                StatusMessage?.Invoke(this, "File received");

                                if (checksumAvailable)
                                {
                                    StatusMessage?.Invoke(this, "File received - Calculating checksum...");
                                    using (var fileStream = new BufferedStream(File.OpenRead(Path.Combine(destinationFolder, fileName)), 32768))
                                    {
                                        SHA1Managed sha = new SHA1Managed();
                                        byte[] checksumByteCalc = sha.ComputeHash(fileStream);
                                        string checksumCalc = BitConverter.ToString(checksumByteCalc).Replace("-", String.Empty).ToLower();
                                        string checksumSent = BitConverter.ToString(checksumByte).Replace("-", String.Empty).ToLower();
                                        Debug.WriteLine($"sent: {checksumSent} | calc: {checksumCalc}");

                                        if (!Enumerable.SequenceEqual(checksumByte, checksumByteCalc))
                                        {
                                            ChecksumError?.Invoke(this, $"sent: {checksumSent} != calc: {checksumCalc}");
                                            StatusMessage?.Invoke(this, $"Checksum missmatch\nSent: {checksumSent}\nCalc: {checksumCalc}");
                                        }
                                        else
                                        {
                                            StatusMessage?.Invoke(this, $"Checksum match\nSent: {checksumSent}\nCalc: {checksumCalc}");
                                        }
                                    }
                                }

                                // Raise new file received event
                                NewFileReceived?.Invoke(this, fileName);
                                ProgressPercent?.Invoke(this, 100);
                            }

                            break;
                        }
                    }
                    finally
                    {
                        // Stop listening
                        listener.Stop();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    StatusMessage?.Invoke(this, ex.Message);
                }
                finally
                {
                    // Send cleanup event, e.g. usefull to enable buttens again
                    Cleanup?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
