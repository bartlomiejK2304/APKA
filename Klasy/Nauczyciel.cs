using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APKA;
using Klasy;
using System.Xml.Serialization;

namespace APKA
{
    /// <summary>
    /// Klasa reprezentująca nauczyciela. Dziedziczy po klasie Osoba.
    /// Zawiera listę przedmiotów, których dany nauczyciel uczy.
    /// </summary>
    public class Nauczyciel : Osoba
    {
        /// <summary>
        /// Lista przedmiotów nauczanych przez nauczyciela.
        /// </summary>
        public List<Przedmiot> Przedmioty { get; set; }

        /// <summary>
        /// Konstruktor bezparametrowy dla serializacji.
        /// </summary>
        public Nauczyciel()
        {

        }

        /// <summary>
        /// Tworzy nowego nauczyciela z listą przedmiotów.
        /// </summary>
        /// <param name="imie">Imię nauczyciela.</param>
        /// <param name="nazwisko">Nazwisko nauczyciela.</param>
        /// <param name="pesel">PESEL nauczyciela.</param>
        /// <param name="login">Login.</param>
        /// <param name="haslo">Hasło.</param>
        /// <param name="przedmiot">Lista przedmiotów.</param>
        public Nauczyciel(string imie, string nazwisko, string pesel,
                          string login, string haslo, List<Przedmiot> przedmiot)
            : base(imie, nazwisko, pesel, login, haslo)
        {
            Przedmioty = przedmiot;
        }

        /// <summary>
        /// Konstruktor pomocniczy akceptujący pojedynczy przedmiot (dla kompatybilności wstecznej).
        /// </summary>
        public Nauczyciel(string imie, string nazwisko, string pesel,
                          string login, string haslo, Przedmiot przedmiot)
            : base(imie, nazwisko, pesel, login, haslo)
        {
            Przedmioty = new List<Przedmiot> { przedmiot };
        }

        /// <summary>
        /// Właściwość pomocnicza zwracająca pierwszy przedmiot z listy lub domyślny.
        /// Ułatwia dostęp w testach jednostkowych.
        /// </summary>
        public Przedmiot Przedmiot
        {
            get => (Przedmioty != null && Przedmioty.Count > 0) ? Przedmioty[0] : default(Przedmiot);
            set => Przedmioty = new List<Przedmiot> { value };
        }

        /// <summary>
        /// Zwraca nagłówek z danymi nauczyciela oraz listą nauczanych przedmiotów.
        /// </summary>
        /// <returns>Sformatowany ciąg znaków.</returns>
        public override string PobierzNaglowek()
        {
            return $"{base.PobierzNaglowek()} (Nauczyciel: {string.Join(", ", Przedmioty)})";
        }
    }
}