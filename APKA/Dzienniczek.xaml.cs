using Klasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        /// <param name="osoba">Obiekt zalogowanego nauczyciela.</param>
        public Dzienniczek(Nauczyciel osoba)
        {
            InitializeComponent();
            zalogowany = (Nauczyciel)osoba;

            UserDisplay.Text = zalogowany.PobierzNaglowek();

            WypelnijPrzedmiotyDlaOcen();
            WypelnijKlasyOceny();
        }

        /// <summary>
        /// Uzupełnia listę klas (hardcoded) w ComboBoxie.
        /// </summary>
        private void WypelnijKlasyOceny()
        {
            ComboKlasaOceny.Items.Add(new ComboBoxItem { Content = "1A" });
            ComboKlasaOceny.Items.Add(new ComboBoxItem { Content = "1B" });
            ComboKlasaOceny.Items.Add(new ComboBoxItem { Content = "2A" });
        }

        /// <summary>
        /// Pobiera przedmioty przypisane do nauczyciela i wypełnia listę rozwijaną.
        /// </summary>
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

        /// <summary>
        /// Obsługuje wylogowanie - powrót do ekranu Login.
        /// </summary>
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Window.GetWindow(this);
            if (mainWindow.MainContent is ContentControl contentControl)
            {
                contentControl.Content = new Login();
            }
        }

        /// <summary>
        /// Obsługuje powrót z widoków szczegółowych (Oceny, Uwagi, Sprawdziany) do głównego menu kafelkowego.
        /// </summary>
        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            WidokStudenci.Visibility = Visibility.Collapsed;
            WidokOceny.Visibility = Visibility.Collapsed;
            WidokSprawdziany.Visibility = Visibility.Collapsed;

            PanelMenu.Visibility = Visibility.Visible;
            PanelPowitanie.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Obsługuje nawigację z głównego menu do odpowiednich podstron.
        /// </summary>
        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;

            PanelMenu.Visibility = Visibility.Collapsed;
            WidokOceny.Visibility = Visibility.Collapsed;
            WidokStudenci.Visibility = Visibility.Collapsed;
            WidokSprawdziany.Visibility = Visibility.Collapsed;
            PanelPowitanie.Visibility = Visibility.Collapsed;

            if (btn.Name == "btnOcenyMenu")
                WidokOceny.Visibility = Visibility.Visible;
            else if (btn.Name == "btnUwagiMenu")
                WidokStudenci.Visibility = Visibility.Visible;
            else if (btn.Name == "btnSprawdzianyMenu")
                WidokSprawdziany.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Wczytuje listę uczniów po wybraniu klasy w module Uwag.
        /// Sortuje uczniów alfabetycznie.
        /// </summary>
        private void ComboKlasa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboKlasa.SelectedItem == null) return;

            string klasa = ((ComboBoxItem)ComboKlasa.SelectedItem).Content.ToString();
            var uczniowie = BazaDanychDziennika.PobierzKlase(klasa);

            // Sortowanie alfabetyczne (wykorzystuje IComparable w klasie Uczen)
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

        /// <summary>
        /// Dodaje nową ocenę uczniowi.
        /// Waliduje formularz, tworzy obiekt Ocena, dodaje do ucznia i zapisuje zmiany w bazie.
        /// </summary>
        private void DodajOcene_Click(object sender, RoutedEventArgs e)
        {
            // Walidacja formularza
            if (ComboKlasaOceny.SelectedItem == null || ComboUczenOceny.SelectedItem == null ||
                ComboPrzedmiotOceny.SelectedItem == null || ComboTypOceny.SelectedItem == null ||
                wybranaWartoscOceny == 0)
            {
                MessageBox.Show("Uzupełnij wszystkie dane oceny.");
                return;
            }

            // Pobranie danych
            Uczen wybranyUczen = (Uczen)ComboUczenOceny.SelectedItem;
            Przedmiot przedmiot = (Przedmiot)ComboPrzedmiotOceny.SelectedItem;
            string typString = ((ComboBoxItem)ComboTypOceny.SelectedItem).Content.ToString();
            TypOceny typ = (TypOceny)Enum.Parse(typeof(TypOceny), typString);

            // Utworzenie obiektu oceny
            Ocena nowaOcena = new Ocena
            {
                Wartosc = wybranaWartoscOceny,
                Przedmiot = przedmiot,
                Typ = typ,
                DataWystawienia = DateTime.Now
            };

            // Zapis danych
            wybranyUczen.Oceny.Add(nowaOcena);
            BazaDanychDziennika.Zapisz();

            MessageBox.Show($"Dodano ocenę: {wybranaWartoscOceny} dla {wybranyUczen.ImieNazwisko}", "Sukces");

            // Resetowanie formularza
            wybranaWartoscOceny = 0;
            wybranyUczenOceny = null;
        }

        /// <summary>
        /// Dodaje nową uwagę dla wybranego ucznia.
        /// </summary>
        private void BtnDodajUwage_Click(object sender, RoutedEventArgs e)
        {
            if (ListaUczniow.SelectedItem == null || string.IsNullOrWhiteSpace(TxtUwaga.Text))
            {
                MessageBox.Show("Wybierz ucznia i wpisz treść uwagi.");
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
        /// Planuje nowy sprawdzian dla wybranej klasy. Sprawdza poprawność daty.
        /// </summary>
        private void BtnDodajSprawdzian_Click(object sender, RoutedEventArgs e)
        {
            if (ComboKlasaSprawdziany.SelectedItem == null || DateSprawdzian.SelectedDate == null)
            {
                MessageBox.Show("Wybierz klasę i datę.");
                return;
            }

            if (DateSprawdzian.SelectedDate < DateTime.Now)
            {
                MessageBox.Show("Data sprawdzianu nie może być z przeszłości.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string klasa = ((ComboBoxItem)ComboKlasaSprawdziany.SelectedItem).Content.ToString();
            DateTime data = DateSprawdzian.SelectedDate.Value;
            Przedmiot przedmiot = zalogowany.Przedmioty.FirstOrDefault(); // Domyślnie pierwszy przedmiot nauczyciela

            Sprawdzian nowySprawdzian = new Sprawdzian(przedmiot, "Sprawdzian", data, klasa);
            BazaDanychDziennika.DodajSprawdzian(nowySprawdzian);

            MessageBox.Show($"Zaplanowano sprawdzian: {klasa}, {data.ToShortDateString()}");

            ComboKlasaSprawdziany.SelectedItem = null;
            DateSprawdzian.SelectedDate = null;
        }
    }
}