using System;
using System.Net;
using System.Net.Sockets;

namespace Server_Checker
{
    class Checker
    {
        private IPAddress ip;
        private short port;
        private Socket s;

        public Checker(IPAddress ip, short port)
        {
            this.ip = ip;
            this.port = port;
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Check()
        {
            try
            {
                IAsyncResult iar = s.BeginConnect(ip, port, null, null);
            }
            catch (SocketException)
            {
                s.Dispose();
            }
        }
    }
}
