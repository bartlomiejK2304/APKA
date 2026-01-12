using APKA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Klasy
{
    public static class DataManager
    {
        public static List<Uczen> Uczniowie = new();
        public static List<Nauczyciel> Nauczyciele = new();

        private static string Sciezka = "dane.xml";

        public static void Wczytaj()
        {
            if (!File.Exists(Sciezka)) return;

            XDocument doc = XDocument.Load(Sciezka);

            // Nauczyciele
            foreach (var n in doc.Root.Element("Nauczyciele").Elements("Nauczyciel"))
            {
                Nauczyciele.Add(new Nauczyciel(
                    n.Element("Imie").Value,
                    n.Element("Nazwisko").Value,
                    n.Element("Pesel").Value,
                    n.Element("Login").Value,
                    n.Element("Haslo").Value,
                    (Przedmiot)Enum.Parse(typeof(Przedmiot), n.Element("Przedmiot").Value)
                ));
            }

            // Uczniowie
            foreach (var u in doc.Root.Element("Uczniowie").Elements("Uczen"))
            {
                Uczniowie.Add(new Uczen(
                    u.Element("Imie").Value,
                    u.Element("Nazwisko").Value,
                    u.Element("Pesel").Value,
                    u.Element("Login").Value,
                    u.Element("Haslo").Value,
                    u.Element("Klasa").Value   
                ));
            }
        }

        public static Osoba Zaloguj(string login, string haslo)
        {
            var u = Uczniowie.FirstOrDefault(x => x.Login == login && x.Haslo == haslo);
            if (u != null) return u;

            return Nauczyciele.FirstOrDefault(x => x.Login == login && x.Haslo == haslo);
        }

        public static List<Uczen> PobierzKlase(string klasa)
        {
            return Uczniowie.Where(u => u.NazwaKlasy == klasa).ToList();
        }
    }
}
