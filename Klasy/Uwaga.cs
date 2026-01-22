using System;
using System.Collections.Generic;
using System.Text;
using Klasy;

namespace Klasy
{
    /// <summary>
    /// Reprezentuje pojedynczą uwagę wpisaną uczniowi przez nauczyciela.
    /// </summary>
    public class Uwaga
    {
        /// <summary>
        /// Treść uwagi (opis zdarzenia).
        /// </summary>
        public string Tresc { get; set; }

        /// <summary>
        /// Data wystawienia uwagi.
        /// </summary>
        public DateTime DataWystawienia { get; set; }

        /// <summary>
        /// Imię i nazwisko osoby (nauczyciela), która wystawiła uwagę.
        /// </summary>
        public string Wystawil { get; set; }

        /// <summary>
        /// Konstruktor bezparametrowy.
        /// </summary>
        public Uwaga()
        {

        }
    }
}