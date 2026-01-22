using Klasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace APKA
{
    public class Uczen:Osoba, IComparable<Uczen>
    {
        public List<Uwaga> Uwagi = new List<Uwaga>();
        public List<Ocena> Oceny = new List<Ocena>();
        public string NazwaKlasy { get; set; }

        public Uczen()
        {
            
        }

        public Uczen(string imie, string nazwisko, string pesel, string login, string haslo, string nazwaKlasy)
           : base(imie, nazwisko, pesel, login, haslo)
        {
            NazwaKlasy = nazwaKlasy;
        }


        public void DodajUwage(Uwaga uwaga)
        {
            Uwagi.Add(uwaga);
        }

        public void UsunUwage(Uwaga uwaga)
        {
            Uwagi.Remove(uwaga);
        }



        public override string PobierzNaglowek()
        {
            return $"{base.PobierzNaglowek()} (Uczeń kl. {NazwaKlasy})";
        }

        public string ImieNazwisko
        {
            get { return $"{Imie} {Nazwisko}"; }
        }

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
