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
        public List<Przedmiot> Przedmioty = new List<Przedmiot>();
        public Nauczyciel(string imie, string nazwisko, string pesel) : base(imie, nazwisko, pesel)
        {
            Przedmioty = new List<Przedmiot>();
        }
    }
}
