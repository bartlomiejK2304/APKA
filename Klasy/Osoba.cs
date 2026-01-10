using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Klasy;

namespace APKA
{
    public abstract class Osoba
    {
        public string ?Imie { get; set; }
        public string ?Nazwisko { get; set; }

        private string _pesel = string.Empty;

        protected Osoba(string imie, string nazwisko, string pesel)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Pesel = pesel;
            Pesel = pesel;
        }

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

        
    }

}
