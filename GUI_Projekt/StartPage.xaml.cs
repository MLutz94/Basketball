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
        public StartPage()
        {
            InitializeComponent();
        }

        /* StartGame wird bei Drücken des Start-Buttons aufgerufen.
         * Ein neues Objekt der Seite GamePage wird Instanziert
         * und diese Seite dann geladen. */
        private void StartGame(object sender, RoutedEventArgs e)
        {
            GamePage NewGamePage = new GamePage();
            this.NavigationService.Navigate(NewGamePage);
        }

        private void GoToInstructionsPage(object sender, RoutedEventArgs e)
        {
            InstructionsPage NewInstructionsPage = new InstructionsPage();
            this.NavigationService.Navigate(NewInstructionsPage);
        }
    }
}
