using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace communication.Client
{
    public class Client : IClient
    {
        private static Mutex mut = new Mutex();

        public event EventHandler<string> OnMessageRecived;

        #region members
        private TcpClient m_client;
        private static Client m_clientInstance = null;
        private IPEndPoint m_ep;
        #endregion



        public bool Connection
        {
            get
            {
                return m_client.Connected;
            }
        }


        public static Client ClientInstance
        {
            get
            {
                if (m_clientInstance == null)
                {
                    m_clientInstance = new Client(8001, "127.0.0.1");
                }
                return m_clientInstance;
            }
        }


        private Client(int port, string IP)
        {
            this.m_client = new TcpClient();
            this.m_ep = new IPEndPoint(IPAddress.Parse(IP), port);

            try
            {
                Connente(IP, port);
            }
            catch (Exception e)
            {
                Console.Write("cannot connect");
                throw e;
            }
            StartReading();
        }



        private void Connente(string IP, int port)
        {
            try
            {
                m_client.Connect(this.m_ep);

            }
            catch (Exception e)
            {
                Console.Write("cannot connect");
                throw e;
            }
            Console.WriteLine("You are connected");
        }


        public void Disconnect()
        {
            if (m_client != null)
            {
                m_client.GetStream().Close();
                m_client.Close();
                m_client = null;
            }
        }

        public void StartReading()
        {
            //wrap with try and catch
            new Task(() =>
            {
                while (Connection)
                {
                    NetworkStream stream = m_client.GetStream();
                    StreamReader reader = new StreamReader(stream);
                    string res = ReadData(reader);
                    if (res != null)
                    {
                        OnMessageRecived?.Invoke(this, res);
                    }
                }
            }).Start();
        }

        private string ReadData(StreamReader reader)
        {
            StringBuilder str = new StringBuilder();
            char[] buffer = new char[1024];
            int index = 0, readBytes;
            while ((readBytes = reader.Read(buffer, index, buffer.Length)) > 0)
            {
                str.Append(buffer, 0, readBytes);
                index += readBytes;

                if (reader.Peek() <= 0) break;
            }
            return str.ToString();
        }


        public void Write(string command)
        {
            mut.WaitOne();
            // System.Threading.Thread.Sleep(100);
            try
            {
                NetworkStream stream = m_client.GetStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(command.Trim());
                writer.Flush();
            }
            catch { }
            mut.ReleaseMutex();
        }
    }
}
