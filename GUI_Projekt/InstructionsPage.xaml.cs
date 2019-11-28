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
    /// Interaktionslogik für InstructionsPage.xaml
    /// </summary>
    public partial class InstructionsPage : Page
    {
        User loggedInUser;
        public InstructionsPage(User user_in)
        {
            loggedInUser = user_in;
            InitializeComponent();
        }

        private void BackToStartPage(object sender, RoutedEventArgs e)
        {
            StartPage NewStartPage = new StartPage(loggedInUser);
            this.NavigationService.Navigate(NewStartPage);
        }
    }
}
