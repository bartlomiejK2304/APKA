using APKA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Klasy
{
    /// <summary>
    /// Statyczna klasa zarządzająca danymi w pamięci oraz ich zapisem/odczytem z pliku XML.
    /// Pełni rolę głównego repozytorium danych aplikacji.
    /// </summary>
    public static class BazaDanychDziennika
    {
        /// <summary>
        /// Lista wszystkich uczniów w systemie.
        /// </summary>
        public static List<Uczen> Uczniowie = new();

        /// <summary>
        /// Lista wszystkich nauczycieli w systemie.
        /// </summary>
        public static List<Nauczyciel> Nauczyciele = new();

        /// <summary>
        /// Lista zaplanowanych sprawdzianów.
        /// </summary>
        public static List<Sprawdzian> Sprawdziany = new();

        private static string Sciezka = "dane.xml";

        /// <summary>
        /// Wczytuje dane z pliku XML do list statycznych.
        /// </summary>
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

        /// <summary>
        /// Zapisuje bieżący stan list (Uczniowie, Nauczyciele, Sprawdziany) do pliku XML.
        /// </summary>
        public static void Zapisz()
        {
            ZapisDziennika dziennikDanych = new ZapisDziennika();
            dziennikDanych.ListaUczniow = Uczniowie;
            dziennikDanych.ListaNauczycieli = Nauczyciele;
            dziennikDanych.ListaSprawdzianow = Sprawdziany;

            dziennikDanych.ZapisXml(Sciezka);
        }

        /// <summary>
        /// Weryfikuje dane logowania użytkownika. Przeszukuje listy uczniów i nauczycieli.
        /// </summary>
        /// <param name="login">Podany login.</param>
        /// <param name="haslo">Podane hasło.</param>
        /// <returns>Obiekt Osoba (Uczen lub Nauczyciel) jeśli dane są poprawne, w przeciwnym razie null.</returns>
        public static Osoba Zaloguj(string login, string haslo)
        {
            var u = Uczniowie.FirstOrDefault(x => x.Login == login && x.Haslo == haslo);
            if (u != null) return u;

            return Nauczyciele.FirstOrDefault(x => x.Login == login && x.Haslo == haslo);
        }

        /// <summary>
        /// Pobiera listę uczniów przypisanych do konkretnej klasy.
        /// </summary>
        /// <param name="klasa">Nazwa klasy (np. "1A").</param>
        /// <returns>Lista uczniów z danej klasy.</returns>
        public static List<Uczen> PobierzKlase(string klasa)
        {
            return Uczniowie.Where(u => u.NazwaKlasy == klasa).ToList();
        }

        /// <summary>
        /// Dodaje nowy sprawdzian do listy i zapisuje zmiany w pliku.
        /// </summary>
        /// <param name="sprawdzian">Obiekt sprawdzianu do dodania.</param>
        public static void DodajSprawdzian(Sprawdzian sprawdzian)
        {
            Sprawdziany.Add(sprawdzian);
            Zapisz();
        }

        /// <summary>
        /// Zwraca listę sprawdzianów zaplanowanych dla danej klasy.
        /// </summary>
        /// <param name="klasa">Nazwa klasy.</param>
        /// <returns>Lista sprawdzianów.</returns>
        public static List<Sprawdzian> PobierzSprawdzianyDlaKlasy(string klasa)
        {
            return Sprawdziany.Where(s => s.Klasa == klasa).ToList();
        }
    }
}