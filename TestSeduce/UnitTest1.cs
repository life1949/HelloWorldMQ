using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Seduce;
using System.Threading;
using System.Text;

namespace TestSeduce
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSend()
        {
            
            TestHandle handle = new TestHandle();

            Thread thread = new Thread(clientSend);
            thread.Start();

            Server server = new Server("127.0.0.1", 1010,handle);
            server.Accept();
            server.ReceiveAsync();

            while (true)
            {
                Console.WriteLine("现在的状态是{0}，现在的信息是{1}", handle.State, handle.Message);
                Thread.Sleep(2000);
                server.ReceiveAsync();
            }

            //Assert.AreEqual("求勾搭", value);
        }


        private void clientSend()
        {
            TestHandle handle = new TestHandle();
            Client client = new Client("127.0.0.1", 1010,handle);
            client.Connect();
            client.SendAsync("求勾搭");
            Thread.Sleep(5000);
            client.SendAsync("再色一次");
        }

        [TestMethod]
        public void TestSendAsync()
        {

        }
    }

    public class TestHandle : IHandle
    {

        public void StartHandler(object message)
        {
            _state = message.ToString();

        }

        public void SendHandler(object message)
        {
            _state = message.ToString();
        }

        public void ReceiveHandler(object message, int count, byte[] content)
        {
            _state = message.ToString();
            _message = Encoding.UTF8.GetString(content, 0, count);
        }

        string _state;
        string _message;
        public string State { get { return _state; } }
        public string Message { get { return _message; } }
    }

}
