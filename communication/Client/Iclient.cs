using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace communication.Client
{
    public interface IClient
    {
        event EventHandler<string> OnMessageRecived;

        bool Connection { get; }

        void Write(string command);
        void StartReading();

        void Disconnect();
    }
}
