﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace communication
{
    class ClientHandler : IClientHandler
    {
        public event EventHandler<string> OnMessageRecived;

        public event EventHandler<string> OnMessageSends;


        #region members
        private TcpClient m_client;
        private NetworkStream m_stream;
        private BinaryReader m_reader;
        private BinaryWriter m_writer;
        #endregion


        public ClientHandler(TcpClient client)
        {
            this.m_client = client;
            m_stream = client.GetStream();
            m_reader = new BinaryReader(m_stream);
            m_writer = new BinaryWriter(m_stream);
        }


        public void Close()
        {
            if (m_client != null)
            {
                //  m_client.GetStream().Close();
                m_client.Close();
                m_client = null;
            }
        }


        public void StartReading()
        {
            //wrap with try and catch
            try
            {
                new Task(() =>
               {
                   while (m_client.Connected)
                {
                    string res = m_reader.ReadString();
                    if (res != null)
                    {
                        OnMessageRecived?.Invoke(this, res);
                    }
                    else
                    {
                        Debug.Write("hii prob");
                    }
                }
               }).Start();
            }
            catch { };

        }

        private string ReadData(StreamReader reader)
        {
            StringBuilder str = new StringBuilder();
            char[] buffer = new char[1024];
            int index = 0, readBytes;
            try
            {
                while ((readBytes = reader.Read(buffer, index, buffer.Length)) > 0)
                {
                    str.Append(buffer, 0, readBytes);
                    index += readBytes;

                    if (reader.Peek() <= 0) break;
                }
            }
            catch (Exception e)
            {
                this.Close();
            }
            return str.ToString();
        }

        public void Write(object sender, string command)
        {
            m_writer.Write(command.Trim());
            m_writer.Flush();
        }
    }
}
