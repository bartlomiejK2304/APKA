using Klasy;
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
    /// Logika interakcji dla klasy Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
        }
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginBox.Text;
            string haslo = PasswordBox.Password;

            // Sprawdź w XML
            Osoba osoba = DataManager.Zaloguj(login, haslo);

            if (osoba == null)
            {
                MessageBox.Show("Niepoprawny login lub hasło!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                LoginBox.Text = "";
                PasswordBox.Password = "";
                return;
            }

            // Jeśli poprawne – przechodzimy dalej
            var mainWindow = (MainWindow)Window.GetWindow(this);
            if (osoba is Nauczyciel)
            {
                mainWindow.MainContent.Content = new Dzienniczek(osoba);
            }
            else
                mainWindow.MainContent.Content = new Dzienniczek_Uczen((Uczen)osoba);
        }
    }
}
