namespace Klasy
{
    /// <summary>
    /// Typ oceny określający wagę i rodzaj aktywności.
    /// </summary>
    public enum TypOceny
    {
        Sprawdzian,
        Kartkowka,
        Odpowiedz,
        Aktywnosc,
        Projekt
    }

    /// <summary>
    /// Rodzaj uwagi behawioralnej.
    /// </summary>
    public enum TypUwagi
    {
        Pozytywna,
        Negatywna
    }

    /// <summary>
    /// Lista przedmiotów szkolnych dostępnych w systemie.
    /// </summary>
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

    /// <summary>
    /// Niestandardowy wyjątek dla błędów logicznych w dzienniku (np. błędna ocena, zła data).
    /// </summary>
    public class DziennikException : Exception
    {
        public DziennikException() { }
        public DziennikException(string message) : base(message) { }
    }
}