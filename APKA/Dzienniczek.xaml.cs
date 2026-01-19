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

        private Nauczyciel zalogowany;

       
        public Dzienniczek(Nauczyciel osoba)
        {
            InitializeComponent();
            zalogowany = (Nauczyciel)osoba;

            UserDisplay.Text = zalogowany.PobierzNaglowek();
            klasy_wybor.ItemsSource = osoba.Przedmioty;

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
            if (ComboTypUwagi.SelectedItem == null)
            {
                MessageBox.Show("Wybierz typ uwagi.");
                return;
            }

            var typUwagi = (ComboTypUwagi.SelectedItem as ComboBoxItem).Tag.ToString();
            TypUwagi typ = typUwagi == "Pozytywna" ? TypUwagi.Pozytywna : TypUwagi.Negatywna;

            Uczen wybranyUczen = (Uczen)ListaUczniow.SelectedItem;
            wybranyUczen.DodajUwage(new Uwaga
            {
                Tresc = TxtUwaga.Text,
                DataWystawienia = DateTime.Today,
                Wystawil = zalogowany.Imie + " " + zalogowany.Nazwisko,
                typ = typ
            });


            DataManager.Zapisz();

            MessageBox.Show("Dodano uwagę!");

            TxtUwaga.Text = "";
            ComboTypUwagi.SelectedItem = null;

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


            Przedmiot przedmiot = zalogowany.Przedmioty.FirstOrDefault();
            Sprawdzian nowySprawdzian = new Sprawdzian(przedmiot, "Sprawdzian", data, klasa);
            DataManager.DodajSprawdzian(nowySprawdzian);

            MessageBox.Show($"Dodano sprawdzian dla klasy {klasa} na dzień {data.ToShortDateString()}!");

            // Czyścimy wybór
            ComboKlasaSprawdziany.SelectedItem = null;
            DateSprawdzian.SelectedDate = null;
        }

        private void Klasa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Ocena_Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Klasa_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Oceny_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // handler placeholder for ListBox selection changed - currently no action required
        }
    }
}
