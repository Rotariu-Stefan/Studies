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

namespace wpf
{
    /// <summary>
    /// Interaction logic for Addsong.xaml
    /// </summary>
    public partial class Addsong : Window
    {
        public Plist PL;

        public Addsong()
        {
            InitializeComponent();
        }

        private void bok_Click(object sender, RoutedEventArgs e)
        {
            PL.addSong(tbName.Text, tbAlbum.Text, tbArtist.Text, tbTime.Text);
            Close();
        }

        private void bcancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
