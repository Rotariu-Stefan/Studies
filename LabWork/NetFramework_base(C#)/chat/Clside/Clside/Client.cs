using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Collections.Generic;

namespace Cside
{
    class Client
    {
        private String name;
        private TcpClient cl;
        private List<String> flist;

        private Thread rth;
        private Thread sth;
        private String srec;

        public Client() : this("NoName") { }
        public Client(String nm)
        {
            name = nm;
            cl = new TcpClient();
            flist = new List<string>();

            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000);
            cl.Connect(serverEndPoint);

            rth = new Thread(new ThreadStart(Receive));
            sth = new Thread(new ThreadStart(SendMessage));
            rth.Start();
            sth.Start();
        }
        public Client(String nm, List<String> fl)
            : this(nm)
        {
            flist = new List<string>(fl);
        }

        public bool HasFriend(String fnm)
        {
            if (flist.Contains(fnm))
                return true;
            else
                return false;
        }
        public String getName()
        {
            return name;
        }

        private void Receive()
        {
            NetworkStream clStream = cl.GetStream();

            byte[] message = new byte[4096];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    bytesRead = clStream.Read(message, 0, 4096);
                }
                catch
                {
                    break;
                }

                ASCIIEncoding enc = new ASCIIEncoding();
                srec = enc.GetString(message, 0, bytesRead);
                Console.WriteLine(srec);

                if (bytesRead == 0)
                    Thread.Sleep(10);
            }


            cl.Close();
        }

        private void SendMessage()
        {
            String s;
            NetworkStream clStream = cl.GetStream();

            while (true)
            {
                s = Console.ReadLine();
                s = (name + "~") + s;

                ASCIIEncoding enc = new ASCIIEncoding();
                byte[] buf = enc.GetBytes(s);
                clStream.Write(buf, 0, buf.Length);
                clStream.Flush();
            }
        }
    }
}
