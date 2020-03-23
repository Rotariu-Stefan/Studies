using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aes
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] a = { 0x19, 0xa0, 0x9a, 0xe9, 0x3d, 0xf4, 0xc6, 0xf8, 0xe3, 0xe2, 0x8d, 0x48, 0xbe, 0x2b, 0x2a, 0x08 };
            //String a = "asdaughtirtfk\vdrt4444111211";
            blmat text = new blmat(a);

            //Console.WriteLine(text.retblmatstr());  
            text.generateRKeyList();
            text.Crypt();
            text.deCrypt();
            //Console.WriteLine(text.retblmatstr());           

            Console.Read();
        }
    }
}
