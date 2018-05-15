using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace communication.Client
{
    public interface IClient
    {
        event EventHandler<string> MessageRecived;


        bool Connection { get; }
        void Connent(string IP, int port);
        void Write(string command);
        void Read();
        void Disconnect();
    }
}
