using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Svside.DB
{
    public class dbf
    {
        Dictionary<string,string[]> db;

        public dbf()
        {
            string[] ff = File.ReadAllLines("U:\\stravos\\DOTNET\\chat\\Svside\\Svside\\DB\\db.txt");

            db = new Dictionary<string, string[]>();

            string[] ax;
            string[] ax2;
            foreach (var it in ff)
            {
                ax = it.Split(' ');
                ax2 = new string[ax.Length - 1];
                for (int i = 0; i < ax2.Length; i++)
                    ax2[i] = ax[i + 1];
                db.Add(ax[0], ax2);
            }
        }

        public bool logintry(string user, string pass)
        {
            bool x = false;
            foreach (var it in db)
                if (it.Key == user)
                    if (it.Value[0] == pass)
                    {
                        x = true;
                        break;
                    }
            return x;
        }
        public bool hasfriend(string user, string friend)
        {
            List<string> hs = db[user].ToList();
            hs.RemoveAt(0);
            if (hs.Contains(friend))
                return true;
            else
                return false;
        }

        public bool addfriend(string user, string friend)
        {
            bool x = false;
            foreach (var it in db)
                if (it.Key == friend)
                {
                    List<string> ax = db[user].ToList();
                    ax.Add(friend);
                    db[user] = ax.ToArray();
                    x = true;
                    update();
                    break;
                }
            return x;
        }
        public bool remfriend(string user, string friend)
        {
            List<string> ax = db[user].ToList();
            if (ax.Remove(friend) == false)
                return false;
            db[user] = ax.ToArray();
            update();
            return true;
        }
        public string[] retfriends(string user)
        {
            List<string> ax = db[user].ToList();
            ax.RemoveAt(0);
            return ax.ToArray();
        }
            
        public void update()
        {
            string a = "";
            string[] ax = new string[db.Count]; int i = 0;
            foreach (var it in db)
            {
                a += it.Key;
                foreach (string s in it.Value)
                    a += " " + s;
                ax[i] = a;
                i++;
                a = "";
            }
            File.WriteAllLines("U:\\stravos\\DOTNET\\chat\\Svside\\Svside\\DB\\db.txt", ax);
        }
        public void afis()
        {
            foreach (var it in db)
            {
                Console.Write("User : " + it.Key);
                Console.Write(", Pass : " + it.Value[0] + ", Friends :");
                for (int i = 1; i < it.Value.Count(); i++)
                    Console.Write("  ," + it.Value[i]);
                Console.WriteLine();
            }
            Console.WriteLine("\n");
        }
    }
}
