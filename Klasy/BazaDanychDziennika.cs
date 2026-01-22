using APKA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Klasy
{
    
    public static class BazaDanychDziennika
    {
        public static List<Uczen> Uczniowie = new();
        public static List<Nauczyciel> Nauczyciele = new();
        public static List<Sprawdzian> Sprawdziany = new();

       
        private static string Sciezka = "dane.xml";

        public static void Wczytaj()
        {
            ZapisDziennika? wczytaneDane = ZapisDziennika.OdczytXml(Sciezka);

            if (wczytaneDane != null)
            {
                Uczniowie = wczytaneDane.ListaUczniow;
                Nauczyciele = wczytaneDane.ListaNauczycieli;
                Sprawdziany = wczytaneDane.ListaSprawdzianow;
            }
           

        }

        public static void Zapisz()
        {
            ZapisDziennika dziennikDanych = new ZapisDziennika();
            dziennikDanych.ListaUczniow = Uczniowie;
            dziennikDanych.ListaNauczycieli = Nauczyciele;
            dziennikDanych.ListaSprawdzianow = Sprawdziany;

            dziennikDanych.ZapisXml(Sciezka);
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
      
    }
}