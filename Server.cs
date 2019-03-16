using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    public class Server
    {
        IPEndPoint endPoint;
        EndPoint sender;
        Socket socket;

        public Server()
        {
            endPoint = new IPEndPoint(IPAddress.Any, 9999);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(endPoint);
        }

        public void Run()
        {
            while (true)
            {
                byte[] data = Receive();

                if (data != null)
                {
                    string message = StringFromBytes(data);
                    string reversedMessage = ReverseString(message);
                    byte[] reversedData = BytesFromString(reversedMessage);
                    Send(reversedData, sender);
                }
            }
        }

        byte[] Receive()
        {
            byte[] data = new byte[4096];
            sender = new IPEndPoint(0, 0);

            int rlen = -1;

            try
            {
                rlen = socket.ReceiveFrom(data, ref sender);    // get data length and sender from socket Recv function

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

        bool Send(byte[] data, EndPoint address)
        {
            bool successed = false;

            try
            {
                int rlen = socket.SendTo(data, address);

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

        string StringFromBytes(byte[] data)
        {
            return new string(Encoding.UTF8.GetChars(data, 0, data.Length));
        }

        string ReverseString(string message)
        {
            char[] chars = message.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        byte[] BytesFromString(string message)
        {
            byte[] response = Encoding.UTF8.GetBytes(message);

            return response;
        }
    }
}
