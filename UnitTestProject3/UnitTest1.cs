using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using communication.Client;
using communication.server;

namespace UnitTestProject3
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            IClient client = Client.ClientInstance;
            client.Connente("127.0.0.1", 8001);
           
        }
    }
}
