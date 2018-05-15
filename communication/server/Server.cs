using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace communication.server
{
    class Server : IServer
    {
        private string ip;
        private int port;
        private TcpListener listener;
        private IClientHandler ch;

        #region properties
        public IClientHandler ClientHandler { get { return ch; } set { this.ch = value; } }
        public string IP { get { return ip; } set { this.ip = value; } }
        public int Port { get { return port; } set { this.port = value; } }
        public TcpListener Listener { get { return this.listener; } set { this.listener = value; } }
        #endregion

        public Server(IClientHandler ch)
        {
            this.ch = ch;
            ServerConfig();
        }

        public void Start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            listener = new TcpListener(ep);
            listener.Start();
            Console.WriteLine("Waiting for connections...");
            Task task = new Task(() =>//creating a listening thread that keeps running.
            {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient(); //recieve new client
                        Console.WriteLine("Got new connection");
                        ch.HandleClient(client); //handle the player through the client handler
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
            listener.Stop();
        }

        private void ServerConfig()
        {
            Port = 8000;
            IP = "127.0.0.1";
        }
    }





}
}
