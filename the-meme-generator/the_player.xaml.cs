using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Win32;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace the_meme_generator
{
    /// <summary>
    /// Logika interakcji dla klasy the_player.xaml
    /// </summary>
    /// 
    public partial class the_player : Window, INotifyPropertyChanged
    {
        private MediaPlayer mediaplayer = new MediaPlayer();
        public string obecnyUtwor;


        public the_player()
        {
            InitializeComponent();
            DataContext = this;
        }
        void timerTick(object sender, EventArgs e)
        {
            if(mediaplayer.Source != null)
            {
                totalTime.Content = String.Format($"{mediaplayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss")}");
                currentTime.Content = String.Format($"{mediaplayer.Position.ToString(@"mm\:ss")}");
                title.Content = String.Format($"{obecnyUtwor}");
            }
            else
            {
                totalTime.Content = "0:00";
                currentTime.Content = "0:00";
                title.Content = "Nie wybrano utworu";
            }

        }
        
        private void Button_Play(object sender, RoutedEventArgs e) // play
        {
            mediaplayer.Play();
        }

        private void Button_Stop(object sender, RoutedEventArgs e) // pause
        {
            mediaplayer.Pause();
        }
        private void Button_Choose(object sender, RoutedEventArgs e) // right skip
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                mediaplayer.Open(new Uri(openFileDialog.FileName));
                obecnyUtwor = openFileDialog.FileName;
            }
                

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timerTick;
            timer.Start();
        }

        private void Button_Next(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Rewind(object sender, RoutedEventArgs e)
        {

        }

        private double _volume;
        private bool mouseCaptured = false;

        public double Volume
        {
            get { return _volume; }
            set
            {
                _volume = value;
                OnPropertyChanged("Volume");
            }
        }

        private new void MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed && mouseCaptured)
            {
                var x = e.GetPosition(volumeBar).X;
                var ratio = x / volumeBar.ActualWidth;
                Volume = ratio * volumeBar.Maximum;
            }
        }

        private new void MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseCaptured = true;
            var x = e.GetPosition(volumeBar).X;
            var ratio = x / volumeBar.ActualWidth;
            Volume = ratio * volumeBar.Maximum;
        }
        private new void MouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseCaptured = false;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
