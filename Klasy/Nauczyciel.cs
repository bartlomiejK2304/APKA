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
        public Przedmiot Przedmiot { get; set; }


        public Nauczyciel()
        {
            
        }

        public Nauczyciel(string imie, string nazwisko, string pesel,
                          string login, string haslo, Przedmiot przedmiot)
            : base(imie, nazwisko, pesel, login, haslo)
        {
            Przedmiot = przedmiot;
        }


        public override string PobierzNaglowek()
        {
            return $"{base.PobierzNaglowek()} (Nauczyciel: {Przedmiot})";
        }





    }
}
