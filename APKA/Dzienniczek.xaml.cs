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
    /// Logika interakcji dla klasy Dzienniczek.xaml
    /// </summary>
    public partial class Dzienniczek : UserControl
    {
        public Dzienniczek()
        {
            InitializeComponent();
        }

        private void Subject_Click(object sender, MouseButtonEventArgs e)
        {
            var border = (Border)sender;
            string subjectName = border.Tag.ToString();

            MessageBox.Show("Otwieram szczegóły dla: " + subjectName);
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Window.GetWindow(this);
            if (mainWindow.MainContent is ContentControl contentControl)
            {
                contentControl.Content = new Login();
            }
        }
        private void btnPoprzedni_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnNastepny_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (sender is Button btn)
            {
                switch (btn.Name)
                {
                    case "btnStudenci":
                        WidokStudenci.Visibility = Visibility.Visible;
                        WidokOceny.Visibility = Visibility.Collapsed;
                        break;
                    case "btnOceny":
                        WidokOceny.Visibility = Visibility.Visible;
                        WidokStudenci.Visibility = Visibility.Collapsed;
                        break;
                    default:
                        WidokStudenci.Visibility = Visibility.Collapsed;
                        WidokOceny.Visibility = Visibility.Collapsed;
                        break;
                }
            }
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            WidokStudenci.Visibility = Visibility.Collapsed;
            WidokOceny.Visibility = Visibility.Collapsed;
        }
    }


}
