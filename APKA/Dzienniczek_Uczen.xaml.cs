using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Klasy;

namespace APKA
{
    /// <summary>
    /// Panel główny dla Ucznia.
    /// Umożliwia przeglądanie własnych ocen, uwag i sprawdzianów oraz analizę statystyk.
    /// </summary>
    public partial class Dzienniczek_Uczen : UserControl
    {
        private Uczen zalogowany;
        private Przedmiot? aktualnyPrzedmiot = null;

        /// <summary>
        /// Inicjalizuje panel danymi zalogowanego ucznia.
        /// </summary>
        /// <param name="osoba">Obiekt ucznia.</param>
        public Dzienniczek_Uczen(Uczen osoba)
        {
            InitializeComponent();
            zalogowany = osoba;

            InicjalizujDane();
            ZaladujDane();
            ObliczStatystyki();
        }

        /// <summary>
        /// Przygotowuje dane wstępne: nagłówek oraz filtry przedmiotów w ComboBox.
        /// </summary>
        private void InicjalizujDane()
        {
            UserDisplay.Text = zalogowany.PobierzNaglowek();

            cmbFiltrujPrzedmiot.Items.Clear();
            cmbFiltrujPrzedmiot.Items.Add("Wszystkie przedmioty");

            // Pobranie listy unikalnych przedmiotów, z których uczeń ma oceny
            var przedmiotyUcznia = zalogowany.Oceny
            .Select(o => o.Przedmiot)
            .Distinct()
            .OrderBy(p => p.ToString())
            .ToList();

            foreach (var przedmiot in przedmiotyUcznia)
            {
                cmbFiltrujPrzedmiot.Items.Add(przedmiot.ToString());
            }

            cmbFiltrujPrzedmiot.SelectedIndex = 0;
            ObliczStatystyki();
        }

        /// <summary>
        /// Ładuje dane do tabel (DataGrid) ocen, uwag i sprawdzianów.
        /// </summary>
        private void ZaladujDane()
        {
            GridOceny.ItemsSource = zalogowany.Oceny;
            GridUwagi.ItemsSource = zalogowany.Uwagi;

            txtOcenyInfo.Text = $"{zalogowany.Oceny.Count} ocen";
            txtUwagiInfo.Text = $"{zalogowany.Uwagi.Count} uwag";

            ZaladujSprawdziany();
            ObliczStatystyki();
        }

        /// <summary>
        /// Pobiera i wyświetla nadchodzące sprawdziany dla klasy ucznia.
        /// </summary>
        private void ZaladujSprawdziany()
        {
            var mojeSprawdziany = BazaDanychDziennika.PobierzSprawdzianyDlaKlasy(zalogowany.NazwaKlasy)
                .Where(s => s.Data >= DateTime.Today)
                .OrderBy(s => s.Data)
                .ToList();

            GridSprawdziany.ItemsSource = mojeSprawdziany;
            txtSprawdzianyInfo.Text = $"{mojeSprawdziany.Count} nadchodzi";
        }

        /// <summary>
        /// Oblicza ogólną średnią ocen i aktualizuje liczniki w panelu statystyk.
        /// </summary>
        private void ObliczStatystyki()
        {
            if (zalogowany.Oceny.Count == 0)
            {
                txtSredniaAll.Text = "0.00";
                txtLiczbaOcenAll.Text = "0";
                return;
            }

            double srednia = zalogowany.Oceny.Average(o => o.Wartosc);
            txtSredniaAll.Text = srednia.ToString("F2");
            txtLiczbaOcenAll.Text = zalogowany.Oceny.Count.ToString();
        }

        /// <summary>
        /// Wyświetla okno popup ze zbiorczymi statystykami.
        /// </summary>
        private void Statystyki_Click(object sender, RoutedEventArgs e)
        {
            ObliczStatystyki();
            var sprawdziany = GridSprawdziany.ItemsSource as List<Sprawdzian>;
            int liczbaSprawdzianow = sprawdziany?.Count ?? 0;

            MessageBox.Show(
                $"Statystyki ucznia:\n\n" +
                $"Średnia ocen: {txtSredniaAll.Text}\n" +
                $"Liczba ocen: {zalogowany.Oceny.Count}\n" +
                $"Liczba uwag: {zalogowany.Uwagi.Count}\n" +
                $"Nadchodzące sprawdziany: {liczbaSprawdzianow}\n",
                "Statystyki", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Filtruje listę ocen po wybranym przedmiocie i oblicza dla niego średnią cząstkową.
        /// </summary>
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

                    // Filtrowanie listy
                    var filtrowaneOceny = zalogowany.Oceny
                        .Where(o => o.Przedmiot == przedmiot)
                        .ToList();
                    GridOceny.ItemsSource = filtrowaneOceny;

                    // Wyświetlenie średniej dla przedmiotu
                    if (filtrowaneOceny.Count > 0)
                    {
                        double sredniaPrzedmiot = filtrowaneOceny.Average(o => o.Wartosc);
                        txtPodsumowaniePrzedmiot.Text =
                            $"{przedmiot}: {filtrowaneOceny.Count} ocen, Średnia: {sredniaPrzedmiot:F2}";
                        PanelPodsumowanie.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        PanelPodsumowanie.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        /// <summary>
        /// Sortuje wyświetlaną listę ocen od najwyższej do najniższej używając klasy OcenaSort.
        /// </summary>
        private void Sortuj_Click(object sender, RoutedEventArgs e)
        {
            zalogowany.Oceny.Sort(new OcenaSort());
            GridOceny.Items.Refresh();
        }
    }
}