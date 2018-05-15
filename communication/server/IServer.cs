using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace communication.server
{
    interface IServer
    {
        // IISClientHandler ClientHandler { get; set; }

        int Port { get; set; }
        string IP { get; set; }

        TcpListener Listener { get; set; }

        void Start();
        void Stop();

    }

}
