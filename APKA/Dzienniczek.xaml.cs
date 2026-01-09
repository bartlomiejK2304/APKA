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
            // logika przycisku "Poprzedni"
        }

        private void btnNastepny_Click(object sender, RoutedEventArgs e)
        {
            // logika przycisku "Następny"
        }


    }


}
