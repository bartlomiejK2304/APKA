using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klasy
{
    public class Sprawdzian
    {
        private Przedmiot przedmiot;
        private DateTime data;
        private string ?temat;
        private string ?klasa;
        private TypOceny typ;

        public Przedmiot Przedmiot { get => przedmiot; set => przedmiot = value; }
        public DateTime Data 
        { get => data; set => data = value; }

        public string Temat { get => temat; set => temat = value; }

        public string Klasa { get => klasa; set => klasa = value; }
        public TypOceny Typ { get => typ; set => typ = value; }

        public Sprawdzian() { }

        public Sprawdzian(Przedmiot przedmiot, string temat, DateTime data, string klasa, TypOceny typ)
        {
            Przedmiot = przedmiot;
            Data = data;
            Temat = temat;
            Klasa = klasa;
            Typ = typ;
        }

    }
}
