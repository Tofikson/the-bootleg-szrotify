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

namespace the_meme_generator
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public playlista PlaylistaALL { get; set; }
        public string Playlista { get; set; }
        public int UsersID { get; set; }
        public int UtworID { get; set; }
        public users user1 { get; set; }
        public utwory utwor1 { get; set; }

        public the_player parentWindow;

        public Window1(playlista PlaylistaALL, the_player parent)
        {
            this.PlaylistaALL = PlaylistaALL;
            DataContext = this;
            this.parentWindow = parent;
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            using (var db = new DatabaseContext())
            {
                var SelectedUser = db.Users.Find(UsersID);
                user1 = SelectedUser;
                var SelectedUtwor = db.Utwory.Find(UtworID);
                utwor1 = SelectedUtwor;
                //if (PlaylistaA > 0)
                //{
                //    var SelectedPlaylista = db.Playlista.Find(PlaylistaALL.ID);
                //    SelectedPlaylista.nazwa_playlisty = Playlista;
                //    SelectedPlaylista.utwor.ID = UtworID;
                //    SelectedPlaylista.users.ID = UsersID;
                //    db.SaveChanges();

                //}
                    db.Add(new playlista()
                    {
                        nazwa_playlisty = Playlista,
                        users = user1,
                        utwor = utwor1
                    });
                    db.SaveChanges();
                parentWindow.Refresh();
                Close();
            }
        }

    }
}
