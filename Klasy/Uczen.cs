using Klasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace APKA
{
    public class Uczen:Osoba
    {
        public List<Uwaga> Uwagi = new List<Uwaga>();
        public List<Ocena> Oceny = new List<Ocena>();
        public string NazwaKlasy { get; set; }

        public Uczen()
        {
            
        }

        public Uczen(string imie, string nazwisko, string pesel, string login, string haslo, string nazwaKlasy)
           : base(imie, nazwisko, pesel, login, haslo)
        {
            NazwaKlasy = nazwaKlasy;
        }


        public void DodajUwage(Uwaga uwaga)
        {
            Uwagi.Add(uwaga);
        }

        public void UsunUwage(Uwaga uwaga)
        {
            Uwagi.Remove(uwaga);
        }



        public override string PobierzNaglowek()
        {
            return $"{base.PobierzNaglowek()} (Uczeń kl. {NazwaKlasy})";
        }

        public string ImieNazwisko
        {
            get { return $"{Imie} {Nazwisko}"; }
        }





    }
}
