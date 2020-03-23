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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Timers;
using System.Windows.Threading;
using System.Xml;

namespace wpf
{
    public partial class MainWindow : Window
    {
        Plist PL;

        bool paused;

        DispatcherTimer dt;
        DoubleAnimation newAnimation = new DoubleAnimation();
        AnimationClock clock;
        TimeSpan tmax;
        TimeSpan constTime = new TimeSpan(0, 0, 1);

        public MainWindow()
        {
            InitializeComponent();
            PL = new Plist();
            PL.MW = this;
        }

        public void playstart(string nm, string t)
        {
            lsong.Content = nm;
            ltime.Content = t;
            paused = false;
            tmax = TimeSpan.Parse(t);
            playbar.Minimum = 0;
            playbar.Maximum = tmax.TotalSeconds;

            this.RegisterName(playbar.Name, playbar);

            bplay.IsEnabled = true;
            bback.IsEnabled = true;
            bforward.IsEnabled = true;

            dt = new System.Windows.Threading.DispatcherTimer();
        }

        private void blist_Click(object sender, RoutedEventArgs e)
        {
            if (PL.IsVisible == false)
                PL.Show();
            else
                PL.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(1);
        }

        private void bplay_Click(object sender, RoutedEventArgs e)
        {
            if (paused == true)
            {
                clock.Controller.Resume();
                dt.IsEnabled = true;
            }
            else
            {
                countDown(tmax, TimeSpan.FromSeconds(1), t => ltime.Content = t.ToString(@"mm\:ss"));

                int from = 0;
                int to = Convert.ToInt32(tmax.TotalSeconds);
                newAnimation.From = from;
                newAnimation.To = to;
                newAnimation.Duration = new Duration(TimeSpan.FromSeconds(to - from));
                clock = newAnimation.CreateClock();
                playbar.ApplyAnimationClock(ProgressBar.ValueProperty, clock);
            }

            bplay.IsEnabled = false;
            bpause.IsEnabled = true;
            bstop.IsEnabled = true;
        }
        void countDown(TimeSpan count, TimeSpan interval, Action<TimeSpan> ts)
        {
            int cc;
            dt.Interval = interval;
            dt.Tick += (_, ea) =>
            {
                count = count.Subtract(constTime);
                tmax.Subtract(constTime);
                cc = Convert.ToInt32(count.TotalSeconds);
                if (cc == -1)
                    dt.Stop();
                else ts(count);
            };
            ts(count);
            dt.Start();
        }

        private void bpause_Click(object sender, RoutedEventArgs e)
        {
            clock.Controller.Pause();
            dt.Stop();
            paused = true;

            bpause.IsEnabled = false;
            bplay.IsEnabled = true;
        }

        private void bstop_Click(object sender, RoutedEventArgs e)
        {
            dt.Stop();
            clock.Controller.Stop();
            ltime.Content= TimeSpan.Zero.ToString(@"mm\:ss");
            paused = false;

            bpause.IsEnabled = false;
            bplay.IsEnabled = true;
           
        }

        private void bback_Click(object sender, RoutedEventArgs e)
        {
        }

    }
}
