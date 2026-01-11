using Klasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APKA
{
    public class Uczen:Osoba
    {
        public List<Uwaga> Uwagi = new List<Uwaga>();
        public List<Ocena> Oceny = new List<Ocena>();

        public Uczen(string imie, string nazwisko, string pesel) : base(imie, nazwisko, pesel)
        {
            Uwagi = new List<Uwaga>();
            Oceny = new List<Ocena>();
        }

        public void DodajUwage(Uwaga uwaga)
        {
            Uwagi.Add(uwaga);
        }

        public void UsunUwage(Uwaga uwaga)
        {
            Uwagi.Remove(uwaga);
        }

    }
}
