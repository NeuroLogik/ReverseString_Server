using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Server
    {
        ITransport transport;

        public Server(ITransport transport)
        {
            this.transport = transport;
        }

        public void Run()
        {
            while (true)
            {
                byte[] data = transport.Receive();

                if (data != null)
                {
                    string message = StringFromBytes(data);
                    string reversedMessage = ReverseString(message);
                    byte[] reversedData = BytesFromString(reversedMessage);
                    transport.Send(reversedData);
                }
            }
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
            return Encoding.UTF8.GetBytes(message);
        }
    }
}
