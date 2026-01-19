using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Klasy;

namespace APKA
{
    public partial class Dzienniczek_Uczen : UserControl
    {
        private Uczen zalogowany;
        private Przedmiot? aktualnyPrzedmiot = null;

        public Dzienniczek_Uczen(Uczen osoba)
        {
            InitializeComponent();
            zalogowany = osoba;
            InicjalizujDane();
            ZaladujDane();
            ObliczStatystyki();
        }

        private void InicjalizujDane()
        {
            UserDisplay.Text = zalogowany.PobierzNaglowek();

            cmbFiltrujPrzedmiot.Items.Clear();
            cmbFiltrujPrzedmiot.Items.Add("Wszystkie przedmioty");

            foreach (Przedmiot przedmiot in Enum.GetValues(typeof(Przedmiot)))
            {
                cmbFiltrujPrzedmiot.Items.Add(przedmiot.ToString());
            }
            cmbFiltrujPrzedmiot.SelectedIndex = 0;
        }

        private void ZaladujDane()
        {
            GridOceny.ItemsSource = zalogowany.Oceny;
            GridUwagi.ItemsSource = zalogowany.Uwagi;

            txtOcenyInfo.Text = $"{zalogowany.Oceny.Count} ocen";
            txtUwagiInfo.Text = $"{zalogowany.Uwagi.Count} uwag";

            ZaladujSprawdziany();
        }

        private void ZaladujSprawdziany()
        {
            var mojeSprawdziany = DataManager.PobierzSprawdzianyDlaKlasy(zalogowany.NazwaKlasy)
                .Where(s => s.Data >= DateTime.Today)
                .OrderBy(s => s.Data)
                .ToList();

            GridSprawdziany.ItemsSource = mojeSprawdziany;
            txtSprawdzianyInfo.Text = $"{mojeSprawdziany.Count} nadchodzi";
        }

        private void ObliczStatystyki()
        {
            if (zalogowany.Oceny.Count == 0)
            {
                txtSrednia.Text = "0.00";
                txtLiczbaOcen.Text = "0";
                return;
            }

            double srednia = zalogowany.Oceny.Average(o => o.Wartosc);
            txtSrednia.Text = srednia.ToString("F2");
            txtLiczbaOcen.Text = zalogowany.Oceny.Count.ToString();
        }

        private void FiltrujOceny()
        {
            if (aktualnyPrzedmiot.HasValue)
            {
                var filtrowaneOceny = zalogowany.Oceny
                    .Where(o => o.Przedmiot == aktualnyPrzedmiot.Value)
                    .ToList();
                GridOceny.ItemsSource = filtrowaneOceny;

                if (filtrowaneOceny.Count > 0)
                {
                    double sredniaPrzedmiot = filtrowaneOceny.Average(o => o.Wartosc);
                    txtPodsumowaniePrzedmiot.Text = $"{aktualnyPrzedmiot.Value}: {filtrowaneOceny.Count} ocen, Średnia: {sredniaPrzedmiot:F2}";
                    PanelPodsumowanie.Visibility = Visibility.Visible;
                }
                else
                {
                    PanelPodsumowanie.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                GridOceny.ItemsSource = zalogowany.Oceny;
                PanelPodsumowanie.Visibility = Visibility.Collapsed;
            }
        }

        // --- OBSŁUGA ZDARZEŃ ---
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
                WidokOceny.Visibility = Visibility.Collapsed;
                WidokUwag.Visibility = Visibility.Collapsed;
                WidokSprawdzianow.Visibility = Visibility.Collapsed;

                if (btn.Name == "btnOcenyMenu")
                    WidokOceny.Visibility = Visibility.Visible;
                else if (btn.Name == "btnUwagiMenu")
                    WidokUwag.Visibility = Visibility.Visible;
                else if (btn.Name == "btnSprawdzianyMenu")
                    WidokSprawdzianow.Visibility = Visibility.Visible;
            }
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            WidokUwag.Visibility = Visibility.Collapsed;
            WidokOceny.Visibility = Visibility.Collapsed;
            WidokSprawdzianow.Visibility = Visibility.Collapsed;
            PanelMenu.Visibility = Visibility.Visible;

            aktualnyPrzedmiot = null;
            cmbFiltrujPrzedmiot.SelectedIndex = 0;
            GridOceny.ItemsSource = zalogowany.Oceny;
            PanelPodsumowanie.Visibility = Visibility.Collapsed;
        }

        private void Statystyki_Click(object sender, RoutedEventArgs e)
        {
            ObliczStatystyki();
            var sprawdziany = GridSprawdziany.ItemsSource as List<Sprawdzian>;
            int liczbaSprawdzianow = sprawdziany?.Count ?? 0;

            MessageBox.Show(
                $"Statystyki ucznia:\n\n" +
                $"Średnia ocen: {txtSrednia.Text}\n" +
                $"Liczba ocen: {zalogowany.Oceny.Count}\n" +
                $"Liczba uwag: {zalogowany.Uwagi.Count}\n" +
                $"Nadchodzące sprawdziany: {liczbaSprawdzianow}\n",
                "Statystyki", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ObliczSrednia_Click(object sender, RoutedEventArgs e)
        {
            ObliczStatystyki();
            MessageBox.Show($"Średnia ocen: {txtSrednia.Text}", "Średnia ocen",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void FiltrujOceny_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFiltrujPrzedmiot.SelectedIndex == 0)
            {
                aktualnyPrzedmiot = null;
                GridOceny.ItemsSource = zalogowany.Oceny;
                PanelPodsumowanie.Visibility = Visibility.Collapsed;
            }
            else if (cmbFiltrujPrzedmiot.SelectedItem != null)
            {
                var wybrany = cmbFiltrujPrzedmiot.SelectedItem.ToString();
                if (Enum.TryParse<Przedmiot>(wybrany, out Przedmiot przedmiot))
                {
                    aktualnyPrzedmiot = przedmiot;
                    FiltrujOceny();
                }
            }
        }
    }
}