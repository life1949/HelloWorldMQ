using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Seduce
{
    public class Client
    {
        public Client(string ip, int port, IHandle handler)
        {
            _ip = ip;
            _port = port;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _handler = handler;
        }

        public void Connect()
        {
            var args = new SocketAsyncEventArgs();
            args.RemoteEndPoint = new IPEndPoint(IPAddress.Parse(_ip), _port);
            args.UserToken = _socket;
            bool result = _socket.ConnectAsync(args);

            _waiter = new Waiter(_socket);
            _waiter.Completed += new Waiter.CompletedHandle(Completed);
        }


        public void SendAsync(string value)
        {
            _waiter.SendAsync(value);
        }

        private void Completed(object sender, SocketAsyncEventArgs e)
        {
            if (e.LastOperation == SocketAsyncOperation.Send)
                _handler.SendHandler("发送完成");
            else if (e.LastOperation == SocketAsyncOperation.Receive)
                _handler.ReceiveHandler("接收完成", e.BytesTransferred, e.Buffer);
        }


        Socket _socket;
        Waiter _waiter;
        string _ip;
        int _port;
        IHandle _handler;
    }
}
