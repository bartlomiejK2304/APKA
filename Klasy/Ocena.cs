using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klasy
{
    /// <summary>
    /// Reprezentuje pojedynczą ocenę ucznia.
    /// Zawiera walidację wartości (1-6) oraz daty wystawienia.
    /// </summary>
    public class Ocena
    {
        private int wartosc;
        private Przedmiot przedmiot;
        private TypOceny typ;
        private DateTime dataWystawienia;

        /// <summary>
        /// Wartość numeryczna oceny (od 1 do 6).
        /// </summary>
        /// <exception cref="DziennikException">Rzucany, gdy wartość jest spoza zakresu 1-6.</exception>
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

        /// <summary>
        /// Przedmiot szkolny, z którego wystawiona jest ocena.
        /// </summary>
        public Przedmiot Przedmiot { get => przedmiot; set => przedmiot = value; }

        /// <summary>
        /// Typ oceny (np. Sprawdzian, Kartkówka).
        /// </summary>
        public TypOceny Typ { get => typ; set => typ = value; }

        /// <summary>
        /// Data wystawienia oceny. Nie może być z przyszłości.
        /// </summary>
        /// <exception cref="DziennikException">Rzucany, gdy data jest późniejsza niż obecna.</exception>
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

        public Ocena() { }

        /// <summary>
        /// Tworzy nową ocenę i ustawia datę wystawienia na obecną chwilę.
        /// </summary>
        public Ocena(int wartosc, Przedmiot przedmiot, TypOceny typ, DateTime dataWystawienia)
        {
            Wartosc = wartosc;
            Przedmiot = przedmiot;
            Typ = typ;
            DataWystawienia = DateTime.Now;
        }

        /// <summary>
        /// Klasa zagnieżdżona implementująca komparator do sortowania ocen malejąco.
        /// </summary>
        public class OcenaWartoscComparer : IComparer<Ocena>
        {
            public int Compare(Ocena? x, Ocena? y)
            {
                if (x == null || y == null) return 0;
                return y.Wartosc.CompareTo(x.Wartosc);
            }
        }
    }
}