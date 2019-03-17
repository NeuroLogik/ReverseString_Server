using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    public class TransportIPv4 : ITransport
    {
        private Socket Socket;
        public IPEndPoint EndPoint;
        public EndPoint Sender;

        public TransportIPv4()
        {
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        public byte[] Receive()
        {
            byte[] data = new byte[4096];
            Sender = new IPEndPoint(0, 0);

            int rlen = -1;

            try
            {
                rlen = Socket.ReceiveFrom(data, ref Sender);    // get data length and sender from socket Recv function

                if (rlen <= 0)
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }

            byte[] trueData = new byte[rlen];
            Buffer.BlockCopy(data, 0, trueData, 0, rlen);

            return trueData;
        }

        public bool Send(byte[] data)
        {
            bool successed = false;

            try
            {
                int rlen = Socket.SendTo(data, Sender);

                if (rlen == data.Length)
                {
                    successed = true;
                }
                else
                {
                    successed = false;
                }
            }
            catch
            {
                successed = false;
            }

            return successed;
        }

        public void Bind(string address, int port)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(address), port);
            Socket.Bind(endPoint);
        }
    }
}
