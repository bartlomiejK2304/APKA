using APKA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Klasy
{
    
    public static class DataManager
    {
        public static List<Uczen> Uczniowie = new();
        public static List<Nauczyciel> Nauczyciele = new();
        public static List<Sprawdzian> Sprawdziany = new();

        // Plik zapisu
        private static string Sciezka = "dane.xml";

        public static void Wczytaj()
        {
            DziennikStore? wczytaneDane = DziennikStore.OdczytXml(Sciezka);

            if (wczytaneDane != null)
            {
                Uczniowie = wczytaneDane.ListaUczniow;
                Nauczyciele = wczytaneDane.ListaNauczycieli;
                Sprawdziany = wczytaneDane.ListaSprawdzianow;
            }
            else
            {
                DodajDaneStartowe();
            }

        }

        public static void Zapisz()
        {
            DziennikStore store = new DziennikStore();
            store.ListaUczniow = Uczniowie;
            store.ListaNauczycieli = Nauczyciele;
            store.ListaSprawdzianow = Sprawdziany;

            store.ZapisXml(Sciezka);
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
        public static void DodajSprawdzian(Sprawdzian sprawdzian)
        {
            Sprawdziany.Add(sprawdzian);
            Zapisz();
        }

        public static List<Sprawdzian> PobierzSprawdzianyDlaKlasy(string klasa)
        {
            return Sprawdziany.Where(s => s.Klasa == klasa).ToList();
        }
        private static void DodajDaneStartowe()
        {
            Nauczyciele.Add(new Nauczyciel("Jan", "Kowalski", "80010112345", "belfer", "123", new List<Przedmiot> { Przedmiot.Matematyka, Przedmiot.Fizyka }));
            Uczniowie.Add(new Uczen("Adam", "Nowak", "05210112345", "uczen", "123", "1A"));
            Uczniowie.Add(new Uczen("Andrzej", "Kowalski", "05413212345", "uczen", "123", "1A"));

            Zapisz();
        }
    }

  
    [XmlRoot("DziennikDanych")]
    public class DziennikStore
    {
        public List<Uczen> ListaUczniow { get; set; }
        public List<Nauczyciel> ListaNauczycieli { get; set; }
        public List<Sprawdzian> ListaSprawdzianow { get; set; }

        public DziennikStore()
        {
            ListaUczniow = new List<Uczen>();
            ListaNauczycieli = new List<Nauczyciel>();
            ListaSprawdzianow = new List<Sprawdzian>();
        }

        public void ZapisXml(string nazwaPliku)
        {
            using StreamWriter sw = new StreamWriter(nazwaPliku);
            XmlSerializer xs = new XmlSerializer(typeof(DziennikStore));
            xs.Serialize(sw, this);
        }

        public static DziennikStore? OdczytXml(string nazwaPliku)
        {
            if (!File.Exists(nazwaPliku)) return null;

            using StreamReader sr = new StreamReader(nazwaPliku);
            XmlSerializer xs = new XmlSerializer(typeof(DziennikStore));

            return (DziennikStore?)xs.Deserialize(sr);
        }
    }
}