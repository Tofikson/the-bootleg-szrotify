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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace the_meme_generator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public users selectedUser { get; set; }
        public string Login { get; set; }
        public string Haslo { get; set; }
        public int hasloid { get; set; }
        public int loginid { get; set; }
        public List<users> pUSSR { get; set; } = Listy.GetUsers();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Login != null && Haslo != null)
            {
                foreach (var user in pUSSR)
                {
                    if (user.haslo == Haslo)
                        hasloid = user.ID;
                }
                foreach (var user in pUSSR)
                {
                    if (user.login == Login)
                        loginid = user.ID;
                }
                if (loginid != 0 && hasloid != 0)
                {
                    if (loginid == hasloid)
                    {
                        the_player player = new the_player();
                        player.Show();
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("Niepoprawny login bądź hasło, spróbuj ponownie");
                }
            }
            else
            {
                MessageBox.Show("Proszę wypełnić puste pola");
            }
        }
    }
}
