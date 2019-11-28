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
using System.Xml;
using System.Xml.Serialization;

namespace GUI_Projekt
{
    /// <summary>
    /// Interaktionslogik für CreateUserPage.xaml
    /// </summary>
    public partial class CreateUserPage : Page
    {
        public CreateUserPage()
        {
            InitializeComponent();
        }

        private void CreateUser(object sender, RoutedEventArgs e)
        {
            List<User> userList;
            userList = new List<User>();

            XmlSerializer xs;
            xs = new XmlSerializer(typeof(List<User>));

            FileStream fsr = new FileStream("login.xml", FileMode.Open, FileAccess.Read);
            userList = (List<User>)xs.Deserialize(fsr);
            fsr.Close();
            string username = CreateUsernameTF.Text;
            string password = CreatePasswordTF.Password.ToString();
            string retypepassword = RetypePasswordTF.Password.ToString();
            

            if (password == retypepassword &&( username != "" || password != "" || retypepassword != ""))
            {
                foreach (User user in userList)
                {

                    if (user.Name == CreateUsernameTF.Text)
                    {


                        CreationFailed.Visibility = Visibility.Visible;
                        return;
                    }


                }

                User newUser = new User();
                newUser.Name = username;
                newUser.Password = password;
                newUser.Level = 1;
                newUser.Highscore = 0;
                userList.Add(newUser);
                
                FileStream fsw = new FileStream("login.xml", FileMode.Open, FileAccess.Write);
                xs.Serialize(fsw, userList);
                fsw.Close();
                LoginPage NewLoginPage = new LoginPage();
                this.NavigationService.Navigate(NewLoginPage);


            }
            else
            {
                PasswordsDoNotMatch.Visibility = Visibility.Visible;

            }
        }
    }
}
