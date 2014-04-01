using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Producer
{
    public class Producer
    {

        public Producer(string ip, int port)
        {
            _ip = ip;
            _port = port;
        }

        public void Send(string mes)
        {

        }

        string _ip;
        int _port;


    }
}
