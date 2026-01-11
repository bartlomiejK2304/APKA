using System;
using System.Collections.Generic;
using System.Text;
using APKA;
using Klasy;

namespace Klasy
{
    public class DziennikException : Exception
    {
        public DziennikException() { }
        public DziennikException(string message) : base(message) { }
    }
    public enum Przedmiot
    {
        JezykPolski,
        Matematyka,
        Historia,
        Geografia,
        Biologia,
        Chemia,
        Fizyka,
        JezykAngielski,
        JezykNiemiecki,
        Informatyka,
        WychowanieFizyczne
    }

    public enum TypOceny
    {
        Sprawdzian,
        Kartkowka,
        Odpowiedz,
        Aktywnosc,
        Projekt
    }

    public enum TypUwagi
    {
        Pozytywna,
        Negatywna
    }


}
