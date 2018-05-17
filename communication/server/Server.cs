using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace communication.server
{
    public class Server : IServer
    {
        private string m_ip;
        private int m_port;
        private TcpListener m_listener;
        private IClientHandler m_ch;
        private IPEndPoint m_ep;

        #region properties
        public IClientHandler ClientHandler { get { return m_ch; } set { this.m_ch = value; } }
        public string IP { get { return m_ip; } set { this.m_ip = value; } }
        public int Port { get { return m_port; } set { this.m_port = value; } }
        public TcpListener Listener { get { return this.m_listener; } set { this.m_listener = value; } }
        #endregion

        public Server(IClientHandler ch)
        {
            this.m_ch = ch;
            ServerConfig();
            m_ep = new IPEndPoint(IPAddress.Parse(m_ip), m_port);
        }

        public void Start()
        {
            m_listener = new TcpListener(m_ep);
            m_listener.Start();
            Console.WriteLine("Waiting for connections...");
            Task task = new Task(() =>//creating a listening thread that keeps running.
            {
                while (true)
                {
                    try
                    {
                        TcpClient client = m_listener.AcceptTcpClient(); //recieve new client
                        Console.WriteLine("Got new connection");
                        m_ch.HandleClient(client); //handle the player through the client handler
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
            });
            task.Start();
        }
        public void Stop()
        {
            m_listener.Stop();
        }

        private void ServerConfig()
        {
            m_port = 8001;
            m_ip = "127.0.0.1";
        }
    }   
}
