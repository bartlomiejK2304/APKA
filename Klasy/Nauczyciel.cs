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
    public class Nauczyciel : Osoba
    {
        public List<Przedmiot> Przedmioty { get; set; }


        public Nauczyciel()
        {
            
        }

        public Nauczyciel(string imie, string nazwisko, string pesel,
                          string login, string haslo, List<Przedmiot> przedmiot)
            : base(imie, nazwisko, pesel, login, haslo)
        {
            Przedmioty = przedmiot;
        }

        // Compatibility constructor to accept a single Przedmiot
        public Nauczyciel(string imie, string nazwisko, string pesel,
                          string login, string haslo, Przedmiot przedmiot)
            : base(imie, nazwisko, pesel, login, haslo)
        {
            Przedmioty = new List<Przedmiot> { przedmiot };
        }

        // Compatibility property to expose a single Przedmiot when needed by tests
        public Przedmiot Przedmiot
        {
            get => (Przedmioty != null && Przedmioty.Count > 0) ? Przedmioty[0] : default(Przedmiot);
            set => Przedmioty = new List<Przedmiot> { value };
        }


        public override string PobierzNaglowek()
        {
            return $"{base.PobierzNaglowek()} (Nauczyciel: {string.Join(", ", Przedmioty)})";
        }





    }
}
