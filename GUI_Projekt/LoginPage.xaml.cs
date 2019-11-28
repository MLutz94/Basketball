using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Serialization;

namespace GUI_Projekt
{
    /// <summary>
    /// Interaktionslogik für LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {


        public LoginPage()
        {
            InitializeComponent();



        }

        private void Login(object sender, RoutedEventArgs e)
        {
            List<User> userList;
            userList = new List<User>();

            XmlSerializer xs;
            xs = new XmlSerializer(typeof(List<User>));

            

            FileStream fs = new FileStream("login.xml", FileMode.Open, FileAccess.Read);
            userList = (List<User>)xs.Deserialize(fs);

            
            foreach (User user in userList)
            {

                if (user.Name == UsernameTF.Text && user.Password == PasswordTF.Password.ToString())
                {

                    
                    User currentLoggedInUser = new User();
                    currentLoggedInUser.Name = user.Name;
                    currentLoggedInUser.Level = user.Level;
                    currentLoggedInUser.Highscore = user.Highscore;
                    if (user.BallSkin == null)
                    {
                        currentLoggedInUser.BallSkin = "default.jpg";
                    }
                    else
                    {
                        currentLoggedInUser.BallSkin = user.BallSkin;
                    }
                    StartPage NewStartPage = new StartPage(currentLoggedInUser);
                    this.NavigationService.Navigate(NewStartPage);

                    break;
                }


            }
            LoginFailedLabel.Visibility = Visibility.Visible;



        }

        private void CreateUser(object sender, RoutedEventArgs e)
        {
            CreateUserPage NewCreateUserPage = new CreateUserPage();
            this.NavigationService.Navigate(NewCreateUserPage);
        }

        private void PasswordTF_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
