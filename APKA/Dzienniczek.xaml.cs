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

        
        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            WidokStudenci.Visibility = Visibility.Collapsed;
            WidokOceny.Visibility = Visibility.Collapsed;
            PanelMenu.Visibility = Visibility.Visible;
        }
        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            
            PanelMenu.Visibility = Visibility.Collapsed;

            
            if (btn.Name == "btnOcenyMenu")
                WidokOceny.Visibility = Visibility.Visible;
            else
                WidokStudenci.Visibility = Visibility.Visible;
        }

    }


}
