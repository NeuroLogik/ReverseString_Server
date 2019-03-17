using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Server
{
    public interface ITransport
    {
        byte[] Receive();
        bool Send(byte[] data);
        void Bind(string address, int port);
    }
}
