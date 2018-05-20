using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private IPEndPoint m_ep;

        #region properties
        public string IP { get { return m_ip; } set { this.m_ip = value; } }
        public int Port { get { return m_port; } set { this.m_port = value; } }
        public TcpListener Listener { get; set; }
        #endregion



        public event EventHandler<string> OnMessageRecived;

        public event EventHandler<string> OnMessageSends;



        public Server()
        {
            ServerConfig();
            m_ep = new IPEndPoint(IPAddress.Parse(m_ip), m_port);
        }

        public void Start()
        {
            Listener = new TcpListener(m_ep);
            Listener.Start();
            Console.WriteLine("Waiting for connections...");
            Task task = new Task(() =>//creating a listening thread that keeps running.
            {
                while (true)
                {
                    try
                    {
                        TcpClient client = Listener.AcceptTcpClient(); //recieve new client
                        Debug.Write("Got new connection");
                        Console.WriteLine("Got new connection");
                        HandleClient(client);

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
            Listener.Stop();
        }

        private void ServerConfig()
        {
            m_port = 8001;
            m_ip = "127.0.0.1";
        }


        private void HandleClient(TcpClient client)
        {
            ClientHandler clientHandler = new ClientHandler(client);
            clientHandler.OnMessageRecived += this.OnMessageRecived;
            this.OnMessageSends += clientHandler.Write;
            clientHandler.StartReading();
        }



        public void Write(string command)
        {
            OnMessageSends?.Invoke(this, command);
        }
    }   
}
