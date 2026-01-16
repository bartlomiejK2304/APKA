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

namespace APKA
{
    /// <summary>
    /// Logika interakcji dla klasy Dzienniczek_Uczen.xaml
    /// </summary>
    public partial class Dzienniczek_Uczen : UserControl
    {
        private Uczen zalogowany;
        public Dzienniczek_Uczen(Uczen osoba)
        {
            InitializeComponent();
            
            zalogowany = osoba;
            UserDisplay.Text = zalogowany.PobierzNaglowek();
            GridOceny.ItemsSource = zalogowany.Oceny;
            GridUwagi.ItemsSource = zalogowany.Uwagi;
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Window.GetWindow(this);
            if (mainWindow?.MainContent is ContentControl contentControl)
            {
                contentControl.Content = new Login();
            }
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                PanelMenu.Visibility = Visibility.Collapsed;
                if (btn.Name == "btnOcenyMenu")
                {
                    WidokOceny.Visibility = Visibility.Visible;
                    WidokUwag.Visibility = Visibility.Collapsed;
                }
                else
                {
                    WidokUwag.Visibility = Visibility.Visible;
                    WidokOceny.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            WidokUwag.Visibility = Visibility.Collapsed;
            WidokOceny.Visibility = Visibility.Collapsed;
            PanelMenu.Visibility = Visibility.Visible;
        }
    }
}
