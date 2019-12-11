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

namespace GUI_Projekt
{
    /// <summary>
    /// Interaktionslogik für SkinPage.xaml
    /// </summary>
    public partial class SkinPage : Page
    {
        User loggedInUser;
        public SkinPage(User user)
        {
            loggedInUser = user;
            InitializeComponent();
        }

        private void BackToStartPage(object sender, RoutedEventArgs e)
        {
            StartPage NewStartPage = new StartPage(loggedInUser);
            this.NavigationService.Navigate(NewStartPage);
        }
    }
}
