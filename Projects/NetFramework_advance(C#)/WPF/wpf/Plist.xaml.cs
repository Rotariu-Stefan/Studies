using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Xml;

namespace wpf
{
    public partial class Plist : Window
    {
        public MainWindow MW;
        public Addsong AS;
        
        public Plist()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        public void addSong(string nm, string alb, string art, string tm)
        {
            XmlDataProvider prov = (XmlDataProvider)plist.DataContext;

            if (prov == null)
            {
                prov = new XmlDataProvider();
                XmlDocument doc = new XmlDocument();
                doc.CreateXmlDeclaration("1.0", "utf-8", "yes");
                XmlNode r = doc.CreateNode(XmlNodeType.Element, "songs", "");
                doc.AppendChild(r);
                prov.Document = doc;
            }

            XmlNode songNode = prov.Document.CreateNode(XmlNodeType.Element, "song", "");
            XmlNode nameNode = prov.Document.CreateNode(XmlNodeType.Element, "name", "");
            nameNode.InnerText = nm+"\n";
            XmlNode albumNode = prov.Document.CreateNode(XmlNodeType.Element, "album", "");
            albumNode.InnerText = "    " + alb + "\n";
            XmlNode artistNode = prov.Document.CreateNode(XmlNodeType.Element, "artist", "");
            artistNode.InnerText = "    " + art + "\n";
            XmlNode timeNode = prov.Document.CreateNode(XmlNodeType.Element, "time", "");
            timeNode.InnerText = "    " + tm + "\n";

            songNode.AppendChild(nameNode);
            songNode.AppendChild(artistNode);
            songNode.AppendChild(albumNode);
            songNode.AppendChild(timeNode);

            prov.Document.DocumentElement.AppendChild(songNode);
            plist.DataContext = prov;

        }

        private void bload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofdialog = new OpenFileDialog();
            ofdialog.CheckFileExists = true;
            ofdialog.Filter = "eXtensible Markup Language Files|*.xml";
            ofdialog.Title = "Select XML playlist file !";
            ofdialog.Multiselect = false;
            ofdialog.InitialDirectory = "U:\\stravos\\DOTNET\\wpf\\wpf";
            ofdialog.ShowDialog();

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(ofdialog.FileName);

                XmlDataProvider prov = new XmlDataProvider();
                prov.Document = doc;
                prov.XPath = "/songs/song";

                plist.DataContext = prov;
            }
            catch (Exception ex)
            {
            }
        }

        private void plist_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            XmlNode x = (XmlNode)plist.SelectedItem;
            MW.playstart(x.SelectSingleNode("artist").InnerText.Trim() + " - " + x.SelectSingleNode("name").InnerText.Trim(), x.SelectSingleNode("time").InnerText.Trim());
        }

        private void badd_Click(object sender, RoutedEventArgs e)
        {
            AS = new Addsong();
            AS.PL = this;
            AS.ShowDialog();
        }

        private void bremove_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
