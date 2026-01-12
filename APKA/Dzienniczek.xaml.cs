using Klasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace APKA
{
    public partial class Dzienniczek : UserControl
    {
        private Osoba zalogowany;

        public Dzienniczek(Osoba osoba)
        {
            InitializeComponent();
            zalogowany = osoba;

            UserDisplay.Text = $"{osoba.Imie} {osoba.Nazwisko}";
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
            // todo
        }

        private void btnNastepny_Click(object sender, RoutedEventArgs e)
        {
            // todo
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            WidokStudenci.Visibility = Visibility.Collapsed;
            WidokOceny.Visibility = Visibility.Collapsed;
            WidokSprawdziany.Visibility = Visibility.Collapsed;

            PanelMenu.Visibility = Visibility.Visible;
            PanelPowitanie.Visibility = Visibility.Visible; // ← WRACA NAPIS
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;

            PanelMenu.Visibility = Visibility.Collapsed;
            WidokOceny.Visibility = Visibility.Collapsed;
            WidokStudenci.Visibility = Visibility.Collapsed;
            WidokSprawdziany.Visibility = Visibility.Collapsed;

            PanelPowitanie.Visibility = Visibility.Collapsed; // ← ZNIKA NAPIS

            if (btn.Name == "btnOcenyMenu")
                WidokOceny.Visibility = Visibility.Visible;
            else if (btn.Name == "btnUwagiMenu")
                WidokStudenci.Visibility = Visibility.Visible;
            else if (btn.Name == "btnSprawdzianyMenu")
                WidokSprawdziany.Visibility = Visibility.Visible;
        }


        //wybor klasy
        private void ComboKlasa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboKlasa.SelectedItem == null)
                return;

            string klasa = ((ComboBoxItem)ComboKlasa.SelectedItem).Content.ToString();

            var uczniowie = DataManager.PobierzKlase(klasa);

            if (uczniowie.Count == 0)
            {
                ListaUczniow.ItemsSource = null;
                BrakUczniowText.Visibility = Visibility.Visible;
            }
            else
            {
                BrakUczniowText.Visibility = Visibility.Collapsed;
                ListaUczniow.ItemsSource = uczniowie;
            }
        }

        private void BtnDodajUwage_Click(object sender, RoutedEventArgs e)
        {
            if (ListaUczniow.SelectedItem == null)
            {
                MessageBox.Show("Najpierw wybierz ucznia z listy.");
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtUwaga.Text))
            {
                MessageBox.Show("Wpisz treść uwagi.");
                return;
            }

            // Na razie NIE zapisujemy nigdzie – tylko komunikat
            MessageBox.Show("Dodano uwagę!");

            TxtUwaga.Text = "";
        }
        private void BtnDodajSprawdzian_Click(object sender, RoutedEventArgs e)
        {
            if (ComboKlasaSprawdziany.SelectedItem == null)
            {
                MessageBox.Show("Wybierz klasę.");
                return;
            }

            

            if (DateSprawdzian.SelectedDate == null )

                /* 
                 TBD
                 DateSprawdzia*/
            {
                MessageBox.Show("Wybierz datę sprawdzianu.");
                return;
            }

            if (DateSprawdzian.SelectedDate <DateTime.Now)
            {
                MessageBox.Show("Zła data","Błąd",MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

                string klasa = ((ComboBoxItem)ComboKlasaSprawdziany.SelectedItem).Content.ToString();
            DateTime data = DateSprawdzian.SelectedDate.Value;

            MessageBox.Show($"Dodano sprawdzian dla klasy {klasa} na dzień {data.ToShortDateString()}!");

            // Czyścimy wybór
            ComboKlasaSprawdziany.SelectedItem = null;
            DateSprawdzian.SelectedDate = null;
        }


    }
}
