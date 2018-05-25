using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace communication.server
{
    public interface IServer
    {
        event EventHandler<string> OnMessageRecived;

        event EventHandler<string> OnMessageSends;


        int Port { get; set; }
        string IP { get; set; }

        TcpListener Listener { get; set; }

        void Write(string command);
        void Start();
        void Stop();

    }

}
