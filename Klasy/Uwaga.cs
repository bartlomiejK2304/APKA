using System;
using System.Collections.Generic;
using System.Text;
using Klasy;

namespace Klasy
{
    public class Uwaga
    {
        public string Tresc { get; set; }
        public DateTime DataWystawienia { get; set; }
        public string Wystawil { get; set; }

        public TypUwagi typ { get; set; }


    }
}
