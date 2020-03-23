using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;

namespace Lab4
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
    public class Autor : Attribute
    {
        string AuthorName;
        DateTime Date;
        string Machine;

        public Autor(string nm)
        {
            AuthorName = nm;
            Date = DateTime.Now;
            Machine = Environment.MachineName;
        }

        public string getName()
        {
            return AuthorName;
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class Vpass : Attribute
    {
        private int _min;
        private int _max;
        private string _message;

        public Vpass()
        {
            _min = 8;
            _max = 16;
        }

        public string Message
        {
            get { return (_message); }
            set { _message = value; }
        }
        public string Min
        {
            get { return _min.ToString(); }
        }
        public string Max
        {
            get { return _max.ToString(); }
        }

        public bool IsValidLen(string theValue)
        {
            int len = theValue.Length;
            if (len >= _min && len <= _max) 
                return true;
            else
                return false;
        }
        public bool IsValidPat(string theValue)
        {
            Match m = Regex.Match(theValue, "[wtf?]");// pat.Match(theValue);
            if (m.Success)
                return true;
            else
                return false;
        }
        public bool IsValid(string theValue)
        {
            if ((IsValidPat(theValue) == true) && (IsValidLen(theValue) == true))
                return true;
            else
                return false;
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class Vemail : Attribute
    {
        private int _min;
        private int _max;
        private string _message;

        public Vemail()
        {
            _min = 10;
            _max = 60;
        }

        public string Message
        {
            get { return (_message); }
            set { _message = value; }
        }
        public string Min
        {
            get { return _min.ToString(); }
        }
        public string Max
        {
            get { return _max.ToString(); }
        }

        public bool IsValidLen(string theValue)
        {
            int len = theValue.Length;
            if (len >= _min && len <= _max)
                return true;
            else
                return false;
        }
        public bool IsValidPat(string theValue)
        {
            Regex pat = new Regex("[wtf?]");
            Match m = pat.Match(theValue);
            if (m.Success)
                return true;
            else
                return false;
        }
        public bool IsValidServer(string theValue)
        {
            bool vs = true;
            string[] host = (theValue.Split('@'));
            string mserv = "www.";
            mserv = mserv + host[1];

            Ping pingSender = new Ping();
            PingReply reply;
            try
            {
                reply = pingSender.Send(mserv);    // waits for a reply from the address
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                vs = false;
            }
            return vs;
        }

        public bool IsValid(string theValue)
        {
            if (IsValidLen(theValue) == true && IsValidPat(theValue) == true && IsValidServer(theValue))
                return true;
            else
                return false;
        }
    }

    public class Validator
    {
        public ArrayList Messages = new ArrayList();
        object ob;
        FieldInfo[] fields;

        public Validator(object obj)
        {
            ob = obj;
            FieldInfo[] fs = ob.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            fields = new FieldInfo[fs.Length];
            for (int i = 0; i < fs.Length; i++)
                fields[i] = fs[i];

            object[] atr = ob.GetType().GetCustomAttributes(true);
            FieldInfo[] fa = atr[0].GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            for (int i = 0; i < fa.Length; i++)
                Console.WriteLine("{0} : {1}", fa[i].Name, fa[i].GetValue(atr[0]));
            Console.WriteLine();
        }

        public bool IsValid()
        {
            bool isValid = true;

            foreach (FieldInfo fi in fields)
                if (!IsValidField(fi)) 
                    isValid = false;
            return isValid;
        }

        private bool IsValidField(FieldInfo aFi)
        {
            bool vb = true;

            object[] atr = aFi.GetCustomAttributes(true);

            if (atr.Length == 0)
            {
                Console.WriteLine("Campul {0} nu are definita o validare !!\n", aFi.Name);
                return vb;
            }

            bool x;
            for (int i = 0; i < atr.Length; i++)
            {
                object[] parm = new object[] { aFi.GetValue(ob) };
                x = (bool)atr[i].GetType().InvokeMember("IsValid", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public, null, atr[i], parm);
                if (x == false)
                {
                    Console.WriteLine("Valoarea {0} a campului {1} \ndin obiectul {2} al clasei {3} NU este Valida !!\n", parm[0], aFi.Name, ob, ob.GetType().Name);
                    vb = false;
                }
                else
                    Console.WriteLine("Valoarea {0} a campului {1} \ndin obiectul {2} al clasei {3} ESTE Valida !!\n", parm[0], aFi.Name, ob, ob.GetType().Name);

            }

            return vb;
        }

        private void AddMessages(FieldInfo aField, Vpass anAttr)
        {
            if (anAttr.Message != null)
            {
                Messages.Add(anAttr.Message);
                return;
            }
            Messages.Add("Invalid range for " + aField.Name +". Valid range is between " + anAttr.Min + " and " + anAttr.Max);
        }
    }

    [Autor("StravoS")]
    public class user
    {
        public string usr;
        [Vemail()]
        public string email;
        [Vpass()]
        public string pass;

        public user()
        {
            usr = "user";
            email = "email";
            pass = "pass";
        }
        public user(string u, string e, string p)
        {
            usr = u;
            email = e;
            pass = p;
        }

        public void afis()
        {
            Console.WriteLine("User: " + usr + "\tEmail: " + email + "\tPass: " + pass);
        }
    }

}
