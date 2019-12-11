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
    /// Interaktionslogik für HighscorePage.xaml
    /// </summary>
    public partial class HighscorePage : Page
    {
        User loggedInUser;
        public HighscorePage(User user_in)
        {
            InitializeComponent();
            loggedInUser = user_in;
            DataContext = new User();

            List<User> userList;
            userList = new List<User>();

            XmlSerializer xs;
            xs = new XmlSerializer(typeof(List<User>));

            FileStream fs = new FileStream("login.xml", FileMode.Open, FileAccess.Read);
            userList = (List<User>)xs.Deserialize(fs);
            userList = userList.OrderBy(o => -1*o.Highscore).ToList();


            if (userList.Count-1 > 10)
            {
                for (int i = 0; i <= 10; i++)
                {
                    User user = userList[i];
                    user.Rang = i + 1;
                    HighscoresDataGrid.Items.Add(user);
                }
            }
            else
            {
                for (int i = 0; i <= userList.Count-1; i++)
                {
                    User user = userList[i];
                    user.Rang = i + 1;
                    HighscoresDataGrid.Items.Add(user);
                }
            }


        }
        private void BackToStartPage(object sender, RoutedEventArgs e)
        {
            StartPage NewStartPage = new StartPage(loggedInUser);
            this.NavigationService.Navigate(NewStartPage);
        }

    }
}
