using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab3
{
    [Serializable()]
    class E2 : ISerializable
    {
        private List<string> words = new List<string>();
        
        public E2()
        {
            Console.WriteLine("E2_________\n ");
        }

        public E2(SerializationInfo sinf, StreamingContext scon)
        {
            words = (List<string>)sinf.GetValue("Swords", typeof(string));
        }
        public void GetObjectData(SerializationInfo sinf, StreamingContext scon)
        {
            sinf.AddValue("Swords", words);
        }
        public void Serialize()
        {
            string filename = "U:\\stravos\\DOTNET\\ser\\ser.txt";
            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter BF = new BinaryFormatter();
            BF.Serialize(stream, words);
            stream.Close();
            Console.WriteLine("Serialization complete!");
        }
        public void Deserialize()
        {
            string filename = "U:\\stravos\\DOTNET\\ser\\ser.txt";
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter BF = new BinaryFormatter();
            words = (List<string>)BF.Deserialize(stream);
            stream.Close();
            Console.WriteLine("Deserialization complete!");
        }


        public void insert(string x)
        {
            words.Add(x);
        }
        public void afis()
        {
            foreach (string s in words)
            {
                Console.WriteLine(s);
            }
        }
    }
}
