using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klasy
{
    /// <summary>
    /// Reprezentuje zaplanowany sprawdzian dla konkretnej klasy.
    /// Implementuje interfejs ICloneable umożliwiający kopiowanie obiektu.
    /// </summary>
    public class Sprawdzian : ICloneable
    {
        private Przedmiot przedmiot;
        private DateTime data;
        private string? temat;
        private string? klasa;

        /// <summary>
        /// Przedmiot, z którego odbywa się sprawdzian.
        /// </summary>
        public Przedmiot Przedmiot { get => przedmiot; set => przedmiot = value; }

        /// <summary>
        /// Data planowanego sprawdzianu.
        /// </summary>
        public DateTime Data { get => data; set => data = value; }

        /// <summary>
        /// Temat lub zakres materiału sprawdzianu.
        /// </summary>
        public string Temat { get => temat; set => temat = value; }

        /// <summary>
        /// Nazwa klasy, dla której przeznaczony jest sprawdzian (np. "1A").
        /// </summary>
        public string Klasa { get => klasa; set => klasa = value; }

        public Sprawdzian() { }

        public Sprawdzian(Przedmiot przedmiot, string temat, DateTime data, string klasa)
        {
            Przedmiot = przedmiot;
            Data = data;
            Temat = temat;
            Klasa = klasa;
        }

        /// <summary>
        /// Tworzy płytką kopię obiektu sprawdzianu (MemberwiseClone).
        /// Przydatne przy tworzeniu podobnych sprawdzianów dla różnych klas.
        /// </summary>
        /// <returns>Kopia obiektu Sprawdzian.</returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}