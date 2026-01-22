using Klasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace APKA
{
    /// <summary>
    /// Klasa reprezentująca ucznia w systemie. Dziedziczy po klasie Osoba.
    /// Implementuje interfejs IComparable, aby umożliwić sortowanie alfabetyczne.
    /// </summary>
    public class Uczen : Osoba, IComparable<Uczen>
    {
        /// <summary>
        /// Lista uwag behawioralnych przypisanych do ucznia.
        /// </summary>
        public List<Uwaga> Uwagi = new List<Uwaga>();

        /// <summary>
        /// Lista ocen uzyskanych przez ucznia.
        /// </summary>
        public List<Ocena> Oceny = new List<Ocena>();

        /// <summary>
        /// Symbol klasy, do której uczęszcza uczeń (np. "1A").
        /// </summary>
        public string NazwaKlasy { get; set; }

        /// <summary>
        /// Konstruktor bezparametrowy wymagany do serializacji XML.
        /// </summary>
        public Uczen()
        {

        }

        /// <summary>
        /// Tworzy nowego ucznia z pełnym zestawem danych.
        /// </summary>
        /// <param name="imie">Imię ucznia.</param>
        /// <param name="nazwisko">Nazwisko ucznia.</param>
        /// <param name="pesel">PESEL ucznia.</param>
        /// <param name="login">Login systemowy.</param>
        /// <param name="haslo">Hasło systemowe.</param>
        /// <param name="nazwaKlasy">Klasa (np. "1A").</param>
        public Uczen(string imie, string nazwisko, string pesel, string login, string haslo, string nazwaKlasy)
           : base(imie, nazwisko, pesel, login, haslo)
        {
            NazwaKlasy = nazwaKlasy;
        }

        /// <summary>
        /// Dodaje nową uwagę do listy uwag ucznia.
        /// </summary>
        /// <param name="uwaga">Obiekt uwagi do dodania.</param>
        public void DodajUwage(Uwaga uwaga)
        {
            Uwagi.Add(uwaga);
        }

        /// <summary>
        /// Usuwa istniejącą uwagę z listy.
        /// </summary>
        /// <param name="uwaga">Obiekt uwagi do usunięcia.</param>
        public void UsunUwage(Uwaga uwaga)
        {
            Uwagi.Remove(uwaga);
        }

        /// <summary>
        /// Zwraca sformatowany nagłówek z danymi ucznia i jego klasą.
        /// </summary>
        /// <returns>Np. "Jan Kowalski (Uczeń kl. 1A)".</returns>
        public override string PobierzNaglowek()
        {
            return $"{base.PobierzNaglowek()} (Uczeń kl. {NazwaKlasy})";
        }

        /// <summary>
        /// Właściwość pomocnicza zwracająca imię i nazwisko jako jeden ciąg.
        /// </summary>
        public string ImieNazwisko
        {
            get { return $"{Imie} {Nazwisko}"; }
        }

        /// <summary>
        /// Porównuje bieżącego ucznia z innym w celu sortowania.
        /// Porównanie odbywa się najpierw po nazwisku, a następnie po imieniu.
        /// </summary>
        /// <param name="other">Inny obiekt ucznia.</param>
        /// <returns>Liczba całkowita wskazująca porządek sortowania.</returns>
        public int CompareTo(Uczen? other)
        {
            if (other == null) return 1;

            int wynik = string.Compare(this.Nazwisko, other.Nazwisko, StringComparison.OrdinalIgnoreCase);

            if (wynik == 0)
            {
                return string.Compare(this.Imie, other.Imie, StringComparison.OrdinalIgnoreCase);
            }

            return wynik;
        }
    }
}