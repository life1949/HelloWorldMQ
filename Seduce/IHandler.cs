using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seduce
{
    public interface IHandle
    {
        void StartHandler(object message);
        void SendHandler(object message);
        void ReceiveHandler(object message, int count, byte[] content);
    }
}
