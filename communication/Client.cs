using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Communication
{
    class Client : ITelnetClient
    {
        private TcpClient client;



        public Client()
        {
            client = new TcpClient();
        }

        public bool Connection => throw new NotImplementedException();

        public event EventHandler<string> MessageRecived;

        public void Connent(string IP, int port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IP), port);
            client.Connect(ep);
            Console.WriteLine("You are connected");
        }

        public void Disconnect()
        {
            if(client!=null)
            {
               client.GetStream().Close();
               client.Close();
               client = null;
            }
        }

        public string Read()
        {
            StringBuilder stringBuilder = new StringBuilder();
            char[] buff = new char[1024];
            int readBytes;

            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            {
                do
                {
                    readBytes = reader.Read(buff, 0, 1024);
                    stringBuilder.Append(buff, 0, readBytes);
                } while (readBytes >= 1024);
            }
            return stringBuilder.ToString();
        }



        public void Write(string command)
        {
            using (NetworkStream stream = client.GetStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(command);
            }
        }
    }
}
