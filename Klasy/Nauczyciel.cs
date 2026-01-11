using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APKA;
using Klasy;

namespace APKA
{
    public class Nauczyciel : Osoba
    {
        private Przedmiot przedmiot;

        public Przedmiot Przedmiot
        {
            get => przedmiot;
            set => przedmiot = value;
        }
        public Nauczyciel(string imie, string nazwisko, string pesel, Przedmiot przedmiot) : base(imie, nazwisko, pesel)
        {
            Przedmiot = przedmiot;
        }

    }
}
