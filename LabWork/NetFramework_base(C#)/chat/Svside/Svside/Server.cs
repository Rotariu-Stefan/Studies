using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using Svside.DB;

namespace SockTest
{
    class Server
    {
        private TcpListener tcpListener;
        private Thread listenThread;

        private Dictionary<string,TcpClient> clist;
        private dbf svdb;

        public Server()
        {
            tcpListener = new TcpListener(IPAddress.Any, 3000);
            listenThread = new Thread(new ThreadStart(ListenForClients));
            listenThread.Start();

            clist = new Dictionary<string,TcpClient>();
            svdb = new dbf();
        }

        private void ListenForClients()
        {
            tcpListener.Start();

            while (true)
            {
                TcpClient cl = tcpListener.AcceptTcpClient();

                Thread cltThread = new Thread(new ParameterizedThreadStart(ReceiveCLMessage));
                cltThread.Start(cl);
            }
        }

        private void ReceiveCLMessage(object cl)
        {
            TcpClient tcpClient = (TcpClient)cl;
            NetworkStream clStream = tcpClient.GetStream();         
            
            string username="";
            string[] fr=new string[1];

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

                if (bytesRead == 0)
                    break;

                ASCIIEncoding enc = new ASCIIEncoding();
                String s = enc.GetString(message, 0, bytesRead);
                string[] mess = s.Split('~');

                if (mess[0] == "login")
                {
                    Console.WriteLine("Login attempt with username: " + mess[1] + " and password: " + mess[2]);
                    if (clist.ContainsKey(mess[1]))
                    {
                        Console.WriteLine("User already conected!");

                        string messlf = "flogin";
                        SendMessage(tcpClient, messlf);
                    }
                    else
                    {
                        if (svdb.logintry(mess[1], mess[2]))
                        {
                            Console.WriteLine("Success!");
                            clist.Add(mess[1], tcpClient);
                            username = mess[1];

                            fr = svdb.retfriends(username);
                            string messls = "slogin";
                            foreach (var it in fr)
                                if (clist.ContainsKey(it))
                                    messls += "~" + "[O]" + it;
                                else
                                    messls += "~" + it;

                            SendMessage(tcpClient, messls);

                            string messlon = "fonline~" + username;
                            foreach (var it in clist)
                                if (svdb.hasfriend(it.Key,username))
                                {
                                    Console.WriteLine("Sent: " + messlon + " to " + it.Key);
                                    SendMessage(it.Value, messlon);
                                }
                        }

                        else
                        {
                            Console.WriteLine("FAIL!");

                            string messlf = "flogin";
                            SendMessage(tcpClient, messlf);
                        }
                    }
                }

                if (mess[0] == "mess")
                {
                    string u = mess[1];
                    string messchat = "mess~" + username + "~" + mess[2];
                    SendMessage(clist[u], messchat);
                    Console.WriteLine("Mesaj: " + mess[2] + "  trimis de la " + username + " la " + u);
                }
                if (mess[0] == "remove")
                {
                    if (svdb.remfriend(username, mess[1]))
                        Console.WriteLine(mess[1] + " removed from " + username + " friend list");
                    else
                        Console.WriteLine(username + " : removal request failed");
                }
                if (mess[0] == "add")
                {
                    if (svdb.addfriend(username, mess[1]))
                    {
                        Console.WriteLine(mess[1] + " added to " + username + " friend list");
                        String messadds = "sadd~" + mess[1];
                        if (clist.ContainsKey(mess[1]))
                            messadds += "~online";
                        else
                            messadds += "~offline";
                        SendMessage(tcpClient, messadds);
                    }
                    else
                    {
                        String messaddf = "fadd";
                        SendMessage(tcpClient, messaddf);
                        Console.WriteLine(username + " : add request failed");
                    }
                }
                if (mess[0] == "file")
                {
                    string u = mess[1];
                    string messchat = "file~" + username + "~" + mess[2] + "~" + mess[3];
                    SendMessage(clist[u], messchat);
                    Console.WriteLine("Fisier: " + mess[2] + "  trimis de la " + username + " la " + u);
                }
            }

            Console.WriteLine("User: " + username + "  logged off!");

            string messloff = "foffline~" + username;
            foreach (var it in clist)
                if (svdb.hasfriend(it.Key, username))
                {
                    Console.WriteLine("Sent: " + messloff + " to " + it.Key);
                    SendMessage(it.Value, messloff);
                }
            clist.Remove(username);
            tcpClient.Close();
        }

        private void SendMessage(TcpClient cl, String s)
        {
                NetworkStream clStream = cl.GetStream();
                ASCIIEncoding enc = new ASCIIEncoding();
                byte[] buf = enc.GetBytes(s);

                clStream.Write(buf, 0, buf.Length);
                clStream.Flush();
        }




    }
}