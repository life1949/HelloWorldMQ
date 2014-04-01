using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Seduce
{
    public class Server
    {
        public Server(string ip, int port, IHandle handler)
        {
            _ip = ip;
            _port = port;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));            
            _handler = handler;
        }

        public void Accept()
        {
            _socket.Listen(100);
            var client = _socket.Accept();
            _waiter = new Waiter(client);
            _waiter.Completed += new Waiter.CompletedHandle(Completed);
        }

        public void ReceiveAsync()
        {
            _waiter.ReceiveAsync();
        }

        private void Completed(object sender, SocketAsyncEventArgs e)
        {
            if (e.LastOperation == SocketAsyncOperation.Send)
                _handler.SendHandler("发送完成");
            else if (e.LastOperation == SocketAsyncOperation.Receive)
                _handler.ReceiveHandler("接收完成", e.BytesTransferred, e.Buffer);            
        }

        string _ip;
        int _port;
        Socket _socket;
        Waiter _waiter;
        IHandle _handler;
    }
}
