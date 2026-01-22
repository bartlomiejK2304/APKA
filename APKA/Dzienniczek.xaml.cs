using Klasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Media;

namespace APKA
{
    /// <summary>
    /// Panel główny dla Nauczyciela.
    /// Umożliwia zarządzanie ocenami, uwagami oraz planowanie sprawdzianów.
    /// </summary>
    public partial class Dzienniczek : UserControl
    {
        private Nauczyciel zalogowany;
        private List<Uczen> uczniowieKlasy = new List<Uczen>();
        private Uczen wybranyUczenOceny = null;
        private int wybranaWartoscOceny = 0;

        /// <summary>
        /// Inicjalizuje panel danymi zalogowanego nauczyciela.
        /// </summary>
        public Dzienniczek(Nauczyciel osoba)
        {
            InitializeComponent();
            zalogowany = (Nauczyciel)osoba;

            UserDisplay.Text = zalogowany.PobierzNaglowek();

            WypelnijPrzedmiotyDlaOcen();

            WypelnijKlasyOceny();
        }

        private void WypelnijKlasyOceny()
        {
            ComboKlasaOceny.Items.Add(new ComboBoxItem { Content = "1A" });
            ComboKlasaOceny.Items.Add(new ComboBoxItem { Content = "1B" });
            ComboKlasaOceny.Items.Add(new ComboBoxItem { Content = "2A" });
        }

        private void WypelnijPrzedmiotyDlaOcen()
        {
            if (ComboPrzedmiotOceny != null && zalogowany?.Przedmioty != null)
            {
                ComboPrzedmiotOceny.Items.Clear();
                foreach (var przedmiot in zalogowany.Przedmioty)
                {
                    ComboPrzedmiotOceny.Items.Add(przedmiot);
                }
            }
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

            var uczniowie = BazaDanychDziennika.PobierzKlase(klasa);

            uczniowie.Sort();

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

        private void KlasaOceny_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboKlasaOceny.SelectedItem == null)
            {
                ComboUczenOceny.ItemsSource = null;
                return;
            }

            string klasa = ((ComboBoxItem)ComboKlasaOceny.SelectedItem).Content.ToString();
            uczniowieKlasy = BazaDanychDziennika.PobierzKlase(klasa);

            if (uczniowieKlasy.Count == 0 || uczniowieKlasy == null)
            {
                MessageBox.Show($"Brak uczniów w klasie {klasa}.");

                ComboUczenOceny.ItemsSource = null;
                ComboUczenOceny.IsEnabled = false;
            }
            else
            {
                ComboUczenOceny.ItemsSource = uczniowieKlasy;
                ComboUczenOceny.DisplayMemberPath = "ImieNazwisko";
                ComboUczenOceny.IsEnabled = true;
            }
        }

        private void UczenOceny_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboUczenOceny.SelectedItem == null)
            {
                wybranyUczenOceny = null;
                return;
            }

            wybranyUczenOceny = (Uczen)ComboUczenOceny.SelectedItem;
        }

        private void OcenaButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            wybranaWartoscOceny = int.Parse(button.Tag.ToString());
            AktualizujPodgladOceny();
        }

        private void AktualizujPodgladOceny()
        {
            string? klasa = ComboKlasaOceny.SelectedItem != null
                ? ((ComboBoxItem)ComboKlasaOceny.SelectedItem).Content.ToString()
                : "nie wybrano";

            string uczen = wybranyUczenOceny != null
                ? $"{wybranyUczenOceny.Imie} {wybranyUczenOceny.Nazwisko}"
                : "nie wybrano";

            string? przedmiot = ComboPrzedmiotOceny.SelectedItem != null
                ? ComboPrzedmiotOceny.SelectedItem.ToString()
                : "nie wybrano";

            string? typOceny = ComboTypOceny.SelectedItem != null ? ((ComboBoxItem)ComboTypOceny.SelectedItem).Content.ToString()
                : "nie wybrano";


            TxtSzczegolyOceny.Text = $"Klasa: {klasa}\n" +
                                    $"Uczeń: {uczen}\n" +
                                    $"Przedmiot: {przedmiot}\n" +
                                    $"Typ oceny: {typOceny}\n" +
                                    $"Ocena: {(wybranaWartoscOceny > 0 ? wybranaWartoscOceny.ToString() : "nie wybrano")}";
        }

        /// <summary>
        /// Dodaje ocenę i zapisuje zmiany w DataManagerze.
        /// </summary>
        private void DodajOcene_Click(object sender, RoutedEventArgs e)
        {
            if (ComboKlasaOceny.SelectedItem == null)
            {
                MessageBox.Show("Wybierz klasę z listy.");
                return;
            }

            if (ComboUczenOceny.SelectedItem == null)
            {
                MessageBox.Show("Wybierz ucznia z listy.");
                return;
            }

            if (ComboPrzedmiotOceny.SelectedItem == null)
            {
                MessageBox.Show("Wybierz przedmiot z listy.");

                return;
            }

            if (ComboTypOceny.SelectedItem == null)
            {
                MessageBox.Show("Wybierz typ oceny z listy."); 
                return;
            }

            if (wybranaWartoscOceny == 0)
            {
                MessageBox.Show("Wybierz ocenę");

                return;
            }

            
                Uczen wybranyUczen = (Uczen)ComboUczenOceny.SelectedItem;
                Przedmiot przedmiot = (Przedmiot)ComboPrzedmiotOceny.SelectedItem;
                string typString = ((ComboBoxItem)ComboTypOceny.SelectedItem).Content.ToString();
                TypOceny typ = (TypOceny)Enum.Parse(typeof(TypOceny), typString);

                Ocena nowaOcena = new Ocena
                {
                    Wartosc = wybranaWartoscOceny,
                    Przedmiot = przedmiot,
                    Typ = typ,
                    DataWystawienia = DateTime.Now
                };

                wybranyUczen.Oceny.Add(nowaOcena);

                BazaDanychDziennika.Zapisz();

                string klasa = ((ComboBoxItem)ComboKlasaOceny.SelectedItem).Content.ToString();
                MessageBox.Show(
                    $"Dodano ocenę: {wybranaWartoscOceny}\n" +
                    $"Uczeń: {wybranyUczen.Imie} {wybranyUczen.Nazwisko}\n" +
                    $"Klasa: {klasa}\n" +
                    $"Przedmiot: {przedmiot}\n" +
                    $"Typ: {typ}",
                    "Ocena dodana pomyślnie"

                );
                wybranaWartoscOceny = 0;
                wybranyUczenOceny = null;

                      
        }


        /// <summary>
        /// Dodaje uwagę i zapisuje zmiany w DataManagerze.
        /// </summary>
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
          

            Uczen wybranyUczen = (Uczen)ListaUczniow.SelectedItem;
            wybranyUczen.DodajUwage(new Uwaga
            {
                Tresc = TxtUwaga.Text,
                DataWystawienia = DateTime.Today,
                Wystawil = zalogowany.Imie + " " + zalogowany.Nazwisko,
            });


            BazaDanychDziennika.Zapisz();

            MessageBox.Show("Dodano uwagę!");

            TxtUwaga.Text = "";
        }

        /// <summary>
        /// Dodaje sprawdzian i zapisuje zmiany w DataManagerze.
        /// </summary>
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
            BazaDanychDziennika.DodajSprawdzian(nowySprawdzian);

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