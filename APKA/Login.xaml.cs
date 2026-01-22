using Klasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; // Ważne dla Task.Delay
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
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

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginBox.Text;
            string haslo = PasswordBox.Password;

            Osoba osoba = BazaDanychDziennika.Zaloguj(login, haslo);

            if (osoba == null)
            {
                MessageBox.Show("Niepoprawny login lub hasło!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                LoginBox.Text = "";
                PasswordBox.Password = "";
                return;
            }

            // Jeśli poprawne – przechodzimy dalej

            BtnLogin.IsEnabled = false;
            BtnLogin.Content = "Logowanie...";

            DoubleAnimation fadeOut = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = new Duration(TimeSpan.FromSeconds(0.4)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
            };

            DoubleAnimation slideUp = new DoubleAnimation
            {
                From = 0,
                To = -100,
                Duration = new Duration(TimeSpan.FromSeconds(0.4)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
            };

            LoginContainer.BeginAnimation(OpacityProperty, fadeOut);

            if (LoginContainer.RenderTransform is TranslateTransform transform)
            {
                transform.BeginAnimation(TranslateTransform.YProperty, slideUp);
            }

            await Task.Delay(400);

            var mainWindow = (MainWindow)Window.GetWindow(this);

            if (mainWindow != null)
            {
                if (osoba is Nauczyciel)
                {
                    mainWindow.MainContent.Content = new Dzienniczek((Nauczyciel)osoba);
                }
                else
                {
                    mainWindow.MainContent.Content = new Dzienniczek_Uczen((Uczen)osoba);
                }
            }
        }
    }
}