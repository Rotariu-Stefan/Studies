using System;
using System.Text;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace Clside
{
    public delegate void d_sendmess(string mes);

    class Fclient
    {
        private String name;
        private List<string> flist;
        private TcpClient cl;

        private Thread rth;
        private Thread lth;

        public Form1 flogin;
        bool logged;

        Mainchat fmain;
        d_sendmess dsm;

        public Fclient() : this("NoName") { }
        public Fclient(String nm)
        {
            name = nm;
            cl = new TcpClient();

            flogin = new Form1();
            logged = false;
            flogin.upass = "";
            flogin.uname = "";

            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000);
            cl.Connect(serverEndPoint);

            rth = new Thread(new ThreadStart(Receive));
            lth = new Thread(new ThreadStart(Logcheck));
            lth.SetApartmentState(System.Threading.ApartmentState.STA);
            rth.Start();
            lth.Start();

            dsm = new d_sendmess(SendMess);
            flogin.ShowDialog();            
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
                string sreply = enc.GetString(message, 0, bytesRead);

                if (logged == false)
                {
                    if (sreply == "flogin")
                        MessageBox.Show("Login Failed!");
                    else
                    {
                        flist = sreply.Split('~').ToList();
                        if (flist[0] == "slogin")
                        {
                            flist.RemoveAt(0);
                            logged = true;
                        }
                        else
                        {
                            MessageBox.Show("Server Error! Response Invalid!");
                            Environment.Exit(1);
                        }
                    }
                }
                else
                {
                    string[] messvec = sreply.Split('~');

                    if (messvec[0] == "fonline")
                        fmain.Invoke(fmain.dchon, new Object[] { messvec[1] });
                    if (messvec[0] == "foffline")
                        fmain.Invoke(fmain.dchoff, new Object[] { messvec[1] });
                    if (messvec[0] == "mess")
                    {
                        String asd="[O]" + messvec[1];
                        fmain.Invoke(fmain.dchcreate, new Object[] { asd });
                        fmain.Invoke(fmain.dtkm, new Object[] { messvec[1], messvec[2] });
                    }
                    if (messvec[0] == "fadd")
                        MessageBox.Show("Username does not exist!");
                    if (messvec[0] == "sadd")
                    {
                        String addf = messvec[1];
                        if (messvec[2] == "online")
                            addf = "[O]" + addf;
                        fmain.Invoke(fmain.daddf, new Object[] { addf });
                    }
                    if (messvec[0] == "file")
                    {//scrie fis in client/download/nume client
                        string path = "U:\\stravos\\DOTNET\\chat\\Clside\\Download\\"+ messvec[2];
                        try
                        {
                            String asd = "[O]" + messvec[1];
                            fmain.Invoke(fmain.dchcreate, new Object[] { asd });
                            fmain.Invoke(fmain.dtkm, new Object[] { messvec[1], "#file#" + messvec[2] });

                            FileStream fs = new FileStream(path, FileMode.Create);

                            byte[] text=enc.GetBytes(messvec[3]);
                            fs.Write(text, 0, text.Length);
                            fs.Close();
                        }
                        catch (IOException)
                        {
                            MessageBox.Show("ERR: file not read!");
                        }
                    }
                }

                if (bytesRead == 0)
                    Thread.Sleep(10);
            }

            cl.Close();
        }

        private void Logcheck()
        {
            String s;
            NetworkStream clStream = cl.GetStream();

            while (true)
            {
                if (logged == false)
                {
                    String un = flogin.uname;
                    String up = flogin.upass;

                    if (un != "" && up != "")
                    {
                        name = un;

                        s = "login~" + un + "~" + up;
                        ASCIIEncoding enc = new ASCIIEncoding();
                        byte[] buf = enc.GetBytes(s);

                        clStream.Write(buf, 0, buf.Length);
                        clStream.Flush();

                        flogin.upass = "";
                        flogin.uname = "";
                    }
                }
                else
                {
                    break;
                }
            }
            flogin.Invoke(flogin.dsc, null);

            fmain = new Mainchat(name, flist,dsm);
            fmain.ShowDialog();

            Environment.Exit(0);
        }

        public void SendMess(string mess)
        {
            NetworkStream clStream = cl.GetStream();

            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] buf = enc.GetBytes(mess);

            clStream.Write(buf, 0, buf.Length);
            clStream.Flush();
        }

    }
}
