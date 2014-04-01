using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Seduce
{
    class Waiter
    {
        public Waiter(Socket _socket)
        {
            this._socket = _socket;
        }

        //public void Send(string value)
        //{
        //    try
        //    {
        //        var bytes = new UTF8Encoding().GetBytes(value);
        //        _socket.Send(bytes, bytes.Length, SocketFlags.None);
        //    }
        //    catch (Exception e)
        //    {
        //        string msg = e.Message;
        //    }
        //}

        public void SendAsync(string value)
        {
            try
            {
                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                var bytes = new UTF8Encoding().GetBytes(value);
                args.SetBuffer(bytes, 0, bytes.Length);
                args.Completed += new EventHandler<SocketAsyncEventArgs>(Args_Completed);
                _socket.SendAsync(args);
            }
            catch (Exception e)
            {
                string msg = e.Message;
            }
        }

        //public string Receive()
        //{
        //    try
        //    {
        //        byte[] buffer = new byte[1024];
        //        int count = _socket.Receive(buffer, buffer.Length, 0);
        //        return new UTF8Encoding().GetString(buffer, 0, count);
        //    }
        //    catch (Exception e)
        //    {
        //        string msg = e.Message;
        //        return null;
        //    }
        //}

        public void ReceiveAsync()
        {
            SocketAsyncEventArgs args = new SocketAsyncEventArgs();
            args.SetBuffer(new byte[1024 * 128], 0, 1024 * 128);
            args.Completed += new EventHandler<SocketAsyncEventArgs>(Args_Completed);
            _socket.ReceiveAsync(args);
        }

        private void Args_Completed(object sender, SocketAsyncEventArgs e)
        {
            if (Completed != null)
                Completed(sender, e);
        }


        Socket _socket;

        public delegate void CompletedHandle(object sender, SocketAsyncEventArgs e);
        public event CompletedHandle Completed;
    }

    class WBytes
    {
        public WBytes(int kb)
        {
            _buffer = new byte[kb * 1024];
        }

        byte[] _buffer;
    }
}
