using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Klasy;

namespace APKA
{
    /// <summary>
    /// Abstrakcyjna klasa bazowa reprezentująca osobę w systemie.
    /// Zawiera wspólne dane takie jak imię, nazwisko, PESEL oraz dane logowania.
    /// Implementuje interfejs IEquatable do porównywania osób.
    /// </summary>
    [XmlInclude(typeof(Uczen))]
    [XmlInclude(typeof(Nauczyciel))]
    public abstract class Osoba : IEquatable<Osoba>
    {
        /// <summary>
        /// Imię osoby.
        /// </summary>
        public string? Imie { get; set; }

        /// <summary>
        /// Nazwisko osoby.
        /// </summary>
        public string? Nazwisko { get; set; }

        private string _pesel;

        /// <summary>
        /// Numer PESEL służący jako unikalny identyfikator.
        /// </summary>
        /// <exception cref="ArgumentException">Rzucany, gdy PESEL nie ma dokładnie 11 znaków.</exception>
        public string Pesel
        {
            get { return _pesel; }
            set
            {
                if (value != null && value.Length == 11)
                {
                    _pesel = value;
                }
                else
                {
                    throw new ArgumentException("PESEL musi mieć dokładnie 11 znaków.");
                }
            }
        }

        /// <summary>
        /// Login używany do logowania w systemie.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Hasło użytkownika.
        /// </summary>
        public string Haslo { get; set; }

        /// <summary>
        /// Konstruktor bezparametrowy wymagany do serializacji XML.
        /// </summary>
        public Osoba()
        {

        }

        /// <summary>
        /// Konstruktor inicjalizujący podstawowe dane osoby.
        /// </summary>
        /// <param name="imie">Imię użytkownika.</param>
        /// <param name="nazwisko">Nazwisko użytkownika.</param>
        /// <param name="pesel">Numer PESEL (11 cyfr).</param>
        /// <param name="login">Login do systemu.</param>
        /// <param name="haslo">Hasło do systemu.</param>
        protected Osoba(string imie, string nazwisko, string pesel, string login, string haslo)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Pesel = pesel;
            Login = login;
            Haslo = haslo;
        }

        /// <summary>
        /// Zwraca sformatowany nagłówek z imieniem i nazwiskiem.
        /// Metoda wirtualna, możliwa do nadpisania w klasach pochodnych.
        /// </summary>
        /// <returns>Ciąg znaków "Imię Nazwisko".</returns>
        public virtual string PobierzNaglowek()
        {
            return $"{Imie} {Nazwisko}";
        }

        /// <summary>
        /// Sprawdza równość dwóch obiektów typu Osoba na podstawie numeru PESEL.
        /// </summary>
        /// <param name="other">Inny obiekt typu Osoba do porównania.</param>
        /// <returns>True jeśli numery PESEL są identyczne, w przeciwnym razie False.</returns>
        public bool Equals(Osoba? other)
        {
            if (other == null) return false;
            return this.Pesel == other.Pesel;
        }

        /// <summary>
        /// Przesłonięta metoda Equals. Sprawdza równość z dowolnym obiektem.
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj is Osoba osoba)
                return Equals(osoba);
            return false;
        }

        /// <summary>
        /// Zwraca kod skrótu (HashCode) na podstawie numeru PESEL.
        /// </summary>
        public override int GetHashCode()
        {
            return Pesel.GetHashCode();
        }
    }
}