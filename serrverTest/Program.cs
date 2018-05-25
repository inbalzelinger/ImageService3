using communication.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace serrverTest
{
    class Program
    {
           static IServer server = new Server();
        static void Main(string[] args)
        {
            server.OnMessageRecived += Server_OnMessageRecived;


            server.Start();
            Console.ReadLine();
        }

        private static void Server_OnMessageRecived(object sender, string e)
        {
            server.Write("IM the server");

            Console.Write("message1\n");
            Console.WriteLine(e);
        }
    }
}
