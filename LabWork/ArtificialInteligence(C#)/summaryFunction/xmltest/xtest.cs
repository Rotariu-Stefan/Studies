using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace xmltest
{
    public class xtest
    {
        static public void createsummary(string originalname,string expath, string smpath, string rezpath)
        {
            XmlDocument ex = new XmlDocument();
            ex.Load(expath);
            XmlDocument sm = new XmlDocument();
            sm.Load(smpath);

            XmlNodeList extypes = ex.GetElementsByTagName("type");

            double max=0;
            double auxval;
            string typestr="";
            for (int i = 0; i < extypes.Count; i++)
            {
                auxval = Convert.ToDouble(extypes[i].Attributes[1].Value);
                if (auxval > max)
                {
                    max = auxval;
                    typestr = extypes[i].Attributes[0].Value;
                }
            }
            XmlNode type=ex.CreateElement("type");
            type.InnerText=typestr;
            XmlComment ncomm=ex.CreateComment("tipul romanului");

            XmlNode newex = ex.FirstChild;
            newex.ReplaceChild(type, ex.SelectSingleNode("/extraction/types"));
            if (newex.FirstChild.NodeType == XmlNodeType.Comment)
                newex.ReplaceChild(ncomm, newex.FirstChild);

            XmlNode newsm = sm.FirstChild;      //string text = sm.FirstChild.InnerText;    //<-daca vreau fara al doilea tab de summary(fara newsm)


            XmlDocument rez = new XmlDocument();
            XmlNode rroot=rez.CreateElement("summary");
            rez.AppendChild(rroot);

            XmlNode rname = rez.CreateElement("name");
            rname.InnerText = originalname;
            rroot.AppendChild(rname);

            XmlNode newex2=rez.ImportNode(newex, true);
            rroot.AppendChild(newex2);

            XmlNode rsum = rez.CreateElement("summarisation");      //rsum.InnerText = text;      //<-daca vreau fara al doilea tab de summary(fara newsm)
            rroot.AppendChild(rsum);
            XmlNode newsm2 = rez.ImportNode(newsm, true);
            rsum.AppendChild(newsm2);            

            rez.Save(rezpath);

        }
    }
}

