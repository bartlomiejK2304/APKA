using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Klasy;

namespace APKA
{
    [XmlInclude(typeof(Uczen))]
    [XmlInclude(typeof(Nauczyciel))]
    public abstract class Osoba
    {
        public string ?Imie { get; set; }
        public string ?Nazwisko { get; set; }

        private string _pesel;
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
        public string Login { get; set; }
        public string Haslo { get; set; }


        public Osoba()
        {
            
        }

        protected Osoba(string imie, string nazwisko, string pesel, string login, string haslo)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Pesel = pesel;
            Login = login;
            Haslo = haslo;
        }

      public virtual  string PobierzNaglowek()
        {
            return $"{Imie} {Nazwisko}";
        }
        
    }

}
