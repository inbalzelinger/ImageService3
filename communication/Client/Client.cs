using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace communication.Client
{
   public class Client : IClient
    {
        public event EventHandler<string> MessageRecived;

        #region members
        private TcpClient m_client;
        private IPEndPoint m_endPoint;
        private static Client m_clientInstance = null;
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
                if(m_clientInstance == null)
                {
                    m_clientInstance = new Client(8000, "127.0.0.1");
                }
                return m_clientInstance;
            }
        }


        private Client(int port , string IP)
        {
            this.m_client = new TcpClient();
            try
            {
              Connent(IP, port);
            } catch(Exception e)
            {
               Console.Write("cannot connect");
               throw e;
            }
            Read();
        }



         public void Connent(string IP, int port)
         {
          IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IP), port);
            try
            {
                m_client.Connect(ep);
            } catch(Exception e)
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

        public void Read()
        {
            //wrap with try and catch
            new Task(() =>
            {
                while (Connection)
                {
                    NetworkStream stream = m_client.GetStream();
                    BinaryReader reader = new BinaryReader(stream);
                    string res = reader.ReadString();
                    if (res != null)
                    {
                        MessageRecived?.Invoke(this, res);
                    }
                }
            }).Start();
        }


        public void Write(string command)
        {
            new Task(() =>
            {
                NetworkStream stream = m_client.GetStream();
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(command);
                writer.Flush();
            }).Start();
        }
    }
}
