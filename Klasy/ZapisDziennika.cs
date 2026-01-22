using APKA;
using System.Xml.Serialization;

namespace Klasy
{
    [XmlRoot("DziennikDanych")]
    public class ZapisDziennika
    {
        public List<Uczen> ListaUczniow { get; set; }
        public List<Nauczyciel> ListaNauczycieli { get; set; }
        public List<Sprawdzian> ListaSprawdzianow { get; set; }

        public ZapisDziennika()
        {
            ListaUczniow = new List<Uczen>();
            ListaNauczycieli = new List<Nauczyciel>();
            ListaSprawdzianow = new List<Sprawdzian>();
        }

        public void ZapisXml(string nazwaPliku)
        {
            using StreamWriter sw = new StreamWriter(nazwaPliku);
            XmlSerializer xs = new XmlSerializer(typeof(ZapisDziennika));
            xs.Serialize(sw, this);
        }

        public static ZapisDziennika? OdczytXml(string nazwaPliku)
        {
            if (!File.Exists(nazwaPliku)) return null;

            using StreamReader sr = new StreamReader(nazwaPliku);
            XmlSerializer xs = new XmlSerializer(typeof(ZapisDziennika));

            return (ZapisDziennika?)xs.Deserialize(sr);
        }
    }
}