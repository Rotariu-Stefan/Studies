using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xmltest
{
    class Program
    {
        static void Main(string[] args)
        {
            String originalname = "initial file name";
            String expath="C:\\Users\\StravoS\\Desktop\\xmltest\\xmltest\\files\\output_modul2.xml";
            String smpath="C:\\Users\\StravoS\\Desktop\\xmltest\\xmltest\\files\\output_modul3.xml";
            String rezpath="C:\\Users\\StravoS\\Desktop\\xmltest\\xmltest\\files\\result.xml";

            xtest.createsummary(originalname,expath, smpath, rezpath);

            Console.WriteLine("APPLICATION TERMINATED OK!");
            Console.Read();
        }
    }
}
