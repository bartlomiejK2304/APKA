using APKA;
using System.Xml.Serialization;

namespace Klasy
{
    /// <summary>
    /// Klasa pomocnicza (DTO) służąca do serializacji całego stanu bazy danych do pliku XML.
    /// Grupuje listy uczniów, nauczycieli i sprawdzianów.
    /// </summary>
    [XmlRoot("DziennikDanych")]
    public class ZapisDziennika
    {
        /// <summary>
        /// Lista wszystkich uczniów do zapisania.
        /// </summary>
        public List<Uczen> ListaUczniow { get; set; }

        /// <summary>
        /// Lista wszystkich nauczycieli do zapisania.
        /// </summary>
        public List<Nauczyciel> ListaNauczycieli { get; set; }

        /// <summary>
        /// Lista wszystkich sprawdzianów do zapisania.
        /// </summary>
        public List<Sprawdzian> ListaSprawdzianow { get; set; }

        /// <summary>
        /// Inicjalizuje puste listy.
        /// </summary>
        public ZapisDziennika()
        {
            ListaUczniow = new List<Uczen>();
            ListaNauczycieli = new List<Nauczyciel>();
            ListaSprawdzianow = new List<Sprawdzian>();
        }

        /// <summary>
        /// Serializuje bieżący obiekt do pliku XML.
        /// </summary>
        /// <param name="nazwaPliku">Ścieżka do pliku wyjściowego.</param>
        public void ZapisXml(string nazwaPliku)
        {
            using StreamWriter sw = new StreamWriter(nazwaPliku);
            XmlSerializer xs = new XmlSerializer(typeof(ZapisDziennika));
            xs.Serialize(sw, this);
        }

        /// <summary>
        /// Deserializuje obiekt z pliku XML.
        /// </summary>
        /// <param name="nazwaPliku">Ścieżka do pliku wejściowego.</param>
        /// <returns>Obiekt ZapisDziennika lub null, jeśli plik nie istnieje.</returns>
        public static ZapisDziennika? OdczytXml(string nazwaPliku)
        {
            if (!File.Exists(nazwaPliku)) return null;

            using StreamReader sr = new StreamReader(nazwaPliku);
            XmlSerializer xs = new XmlSerializer(typeof(ZapisDziennika));

            return (ZapisDziennika?)xs.Deserialize(sr);
        }
    }
}