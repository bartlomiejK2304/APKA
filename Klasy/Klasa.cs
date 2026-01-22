using APKA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klasy
{
    /// <summary>
    /// Reprezentuje oddział szkolny (klasę), np. "1A".
    /// Przechowuje przypisane do klasy sprawdziany i przedmioty.
    /// </summary>
    public class Klasa
    {
        /// <summary>
        /// Nazwa oddziału (np. "1A").
        /// </summary>
        public string nazwaKlasy; // Uwaga: warto zmienić na Właściwość (Property) PascalCase: NazwaKlasy

        /// <summary>
        /// Lista uczniów przypisanych do tej klasy.
        /// </summary>
        public List<Uczen> klasa = new List<Uczen>(); // Uwaga: nazwa pola "klasa" może być myląca, lepiej "Uczniowie"

        /// <summary>
        /// Lista sprawdzianów zaplanowanych dla tej klasy.
        /// </summary>
        public List<Sprawdzian> Sprawdziany = new List<Sprawdzian>();

        /// <summary>
        /// Lista przedmiotów realizowanych w tej klasie.
        /// </summary>
        public List<Przedmiot> Przedmioty = new List<Przedmiot>();
    }
}