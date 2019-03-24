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
        public byte[] ReceivedDdata;
        public byte[] ReversedData;

        public Server(ITransport transport)
        {
            this.transport = transport;
        }

        public void Run()
        {
            while(true)
            {
                SingleStep();
            }
        }

        public void SingleStep()
        {
            ReceivedDdata = transport.Receive();

            if (ReceivedDdata != null)
            {
                string message = StringFromBytes(ReceivedDdata);
                string reversedMessage = ReverseString(message);
                ReversedData = BytesFromString(reversedMessage);
                transport.Send(ReversedData);
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
