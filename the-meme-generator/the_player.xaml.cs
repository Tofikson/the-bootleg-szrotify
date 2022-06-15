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
    public partial class the_player : Window, INotifyPropertyChanged
    {
        public playlista SelectedPlaylista{ get; set; }
        private MediaPlayer mediaplayer = new MediaPlayer();
        public string obecnyUtwor;

        public List<playlista> Playlistas { get; set; } = GetPlaylistas();

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
        private void Button_Choose(object sender, RoutedEventArgs e)
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
       
        
        public static List<playlista> GetPlaylistas()
        { 
            
            var list = new List<playlista>();
            using (var db = new DatabaseContext())
            {
                foreach (var playlistUS in db.Playlista)
                {
                        foreach (var UtwoR in db.Utwory)
                        {
                            if (UtwoR.ID == playlistUS.utwor.ID)
                            {
                                playlistUS.utwor.tytul = UtwoR.tytul;
                                playlistUS.utwor.czas_utworu = UtwoR.czas_utworu;
                                playlistUS.utwor.data_dodania = UtwoR.data_dodania;
                            foreach (var WyK in db.Wykonawcy)
                            {
                                if (WyK.ID == playlistUS.utwor.wykonawcy.ID)
                                {
                                   
                                    playlistUS.utwor.wykonawcy.wykonawca = WyK.wykonawca;
                                }
                            }
                        }
                        }
                        list.Add(playlistUS);   
                }
                return list;
            }
        }

        private void dodaj(object sender, RoutedEventArgs e)
        {
            if (SelectedPlaylista != null)
            {
                Window1 window = new Window1(SelectedPlaylista, this);
                window.Show();
            }
            else
            {
                Window1 window = new Window1(null, this);
                window.Show();
            }
        }

        private void odswiez(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void usun(object sender, RoutedEventArgs e)
        {
            using (var db = new DatabaseContext())
            {
                var SP = db.Playlista.Find(SelectedPlaylista.ID);
                if (SP != null && SelectedPlaylista.ID > 0)
                {
                    var Pl = db.Playlista.Find(SelectedPlaylista.ID);
                    db.Playlista.Remove(Pl);
                    db.SaveChanges();
                }
            }
        }
        public void Refresh()
        {
            categoryDataGrid.ItemsSource = null;
            Playlistas = GetPlaylistas();
            categoryDataGrid.ItemsSource = Playlistas;
        }
    }
}
