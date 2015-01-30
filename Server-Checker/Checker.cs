using System;
using System.Net;
using System.Net.Sockets;

namespace Server_Checker
{
    class Checker
    {
        private IPAddress ip;
        private short port;

        public Checker(IPAddress ip, short port)
        {
            this.ip = ip;
            this.port = port;
        }

        public void setIP(IPAddress ip)
        {
            this.ip = ip;
        }

        public void setPort(short port)
        {
            this.port = port;
        }

        public bool Check()
        {
            using (Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                IAsyncResult iar = s.BeginConnect(ip, port, null, null);
                iar.AsyncWaitHandle.WaitOne(1000, true);

                if (s.Connected)
                    return true;
                return false;
            }
        }
    }
}
