using communication.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IClient client = Client.ClientInstance;
            client.OnMessageRecived += Client_MessageRecived;
            client.StartReading();
            client.Write("hiiiiiiiiiiiiiiiii\r\nh2\r\nh3");
            client.Write("hiiiiiiiiiiiiiiiii\r\n");
            Console.ReadLine();
        }

        private static void Client_MessageRecived(object sender, string e)
        {
            Console.Write(e);
        }
    }
}
