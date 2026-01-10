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

        public Uczen(string imie, string nazwisko, string pesel) : base(imie, nazwisko, pesel)
        {

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
