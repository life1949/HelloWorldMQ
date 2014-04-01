using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Seduce
{
    public interface IHuman
    {
        event NoticeHandler Started;
        
        event NoticeHandler Sended;

        event ContentHandler Received;

        SocketAsyncEventArgs Args { get; }
    }
    public delegate void NoticeHandler(object message);
    public delegate void ContentHandler(byte[] content);
}
