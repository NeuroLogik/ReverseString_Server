using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TransportIPv4 transport = new TransportIPv4();
            transport.Bind("192.168.0.101", 9999);
            Server server = new Server(transport);
            server.Run();
        }
    }
}
