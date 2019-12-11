/* Startseite, hier findet man den Button zum Starten des Spiels, außerdem (zukünftig)
 * Optionen Highscore, etc... */

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
    /// Interaktionslogik für StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        User loggedInUser;
        public StartPage(User user)
        {
            loggedInUser = user;
            InitializeComponent();
            LoggedInAs.Content = "Eingeloggt als " + loggedInUser.Name;
        }

        public User getLoggedInUser(){
            return loggedInUser;
        } 

        /* StartGame wird bei Drücken des Start-Buttons aufgerufen.
         * Ein neues Objekt der Seite GamePage wird Instanziert
         * und diese Seite dann geladen. */
        private void StartGame(object sender, RoutedEventArgs e)
        {
            GamePage NewGamePage = new GamePage(loggedInUser);
            this.NavigationService.Navigate(NewGamePage);
        }

        private void GoToInstructionsPage(object sender, RoutedEventArgs e)
        {
            InstructionsPage NewInstructionsPage = new InstructionsPage(loggedInUser);
            this.NavigationService.Navigate(NewInstructionsPage);
        }

        private void GoToHighscorePage(object sender, RoutedEventArgs e)
        {
            HighscorePage NewHighscorePage = new HighscorePage(loggedInUser);
            this.NavigationService.Navigate(NewHighscorePage);
        }

        private void GoToSkinPage(object sender, RoutedEventArgs e)
        {
            SkinPage NewSkinPage = new SkinPage(loggedInUser);
            this.NavigationService.Navigate(NewSkinPage);
        }
    }
}
