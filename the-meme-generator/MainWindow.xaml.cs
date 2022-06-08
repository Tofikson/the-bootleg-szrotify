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
        public List<users> pUSSR { get; set; } = Listy.GetUsers();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           if(selectedUser.login != null) // TU NULL WYSKAKIWAŁ
            { 
           selectedUser.login = Login;
            foreach (var user in pUSSR)
            {
                if(user.login == selectedUser.login)
                {
                    the_player player = new the_player();
                    player.Show();
                    Close();
                }
            }
            }
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            /*using (var db = new DatabaseContext()) 
            {
                //var spawdzany = db.Users.Find();

            foreach(var user in pUSSR)
                {
                   
                } 
                if (logowanie.Text == Login)
                    {
                        the_player player = new the_player();
                        player.Show();
                        Close();
                    }
                    else
                    {
                        Console.WriteLine("Twoój stary");
                    }
            }*/
            
        }
    }
}
