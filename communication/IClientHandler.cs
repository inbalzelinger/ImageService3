using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace communication
{
    public interface IClientHandler
    {
        void Close();
        void StartReading();
        void Write(object sender, string command);
    }
}
