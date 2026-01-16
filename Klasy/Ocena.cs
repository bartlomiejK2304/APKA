using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klasy
{
    public class Ocena
    {
        private int wartosc;
        private Przedmiot przedmiot;
        private TypOceny typ;
        private DateTime dataWystawienia;

        public int Wartosc
        {
            get => wartosc;
            set
            {
                if (value < 1 || value > 6)
                    throw new DziennikException("Ocena musi być w zakresie 1-6");
                wartosc = value;
            }
        }

        public Przedmiot Przedmiot { get => przedmiot; set => przedmiot = value; }
        public TypOceny Typ { get => typ; set => typ = value; }

        public DateTime DataWystawienia
        {
            get => dataWystawienia;
            set
            {
                if (value > DateTime.Now)
                    throw new DziennikException("Zla data");
                dataWystawienia = value;
            }
        }

        public Ocena()
        {
            
        }

        public Ocena(int wartosc, Przedmiot przedmiot, TypOceny typ, DateTime dataWystawienia)
        {
            Wartosc = wartosc;
            Przedmiot = przedmiot;
            Typ = typ;
            DataWystawienia = DateTime.Now;
        }

    }
}
