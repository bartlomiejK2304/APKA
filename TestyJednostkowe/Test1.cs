using APKA;
using Klasy;
using System;

namespace TestyJednostkowe
{
    [TestClass]
    public  class Test1
    {
        /// <summary>
        /// Metoda TestKonstruktoraUcznia() ma za zadanie sprawdzić czy dane są poprawnie przypisywane
        /// </summary>
        [TestMethod]
        public void TestKonstruktoraUcznia()
        {
            //Arrange
            string name_test = "Jan";
            string surname_test = "Kowal";
            string class_test = "2B";
            string correctPesel_test = "12345678901";
            string login_test = "jan123";
            string password_test = "tajnehaslo";





            //Act
            Uczen u = new Uczen(name_test, surname_test, correctPesel_test, login_test, password_test, class_test);



            //Assert
            Assert.AreEqual(name_test, u.Imie);
            Assert.AreEqual(surname_test, u.Nazwisko);
            Assert.AreEqual(class_test, u.NazwaKlasy);
            Assert.AreEqual(login_test, u.Login);
            Assert.AreEqual(password_test, u.Haslo);
            Assert.AreEqual(correctPesel_test, u.Pesel);
        }

        /// <summary>
        /// Test dodawania nowej Uwagi
        /// </summary>


        [TestMethod]
    
        public void  TestDodawaniaUwagi()
        {

            //Arrange 
            Uczen u = new Uczen("Jan", "Test", "12345678901", "login", "haslo", "1A");

            Uwaga nowaUwaga = new Uwaga
            {
                Tresc = "Brak pracy domowej",
                DataWystawienia = DateTime.Now,
                Wystawil = "Nauczyciel"

            };

            //Act
            u.DodajUwage(nowaUwaga);

            //Assert
            Assert.AreEqual(1, u.Uwagi.Count);


        
           



        }


        /// <summary>
        /// Test wyjatku - gdy zostanie wprowadzona bledna wartosc
        /// </summary>



        [TestMethod]

        public void TestWyjatkuOcena_ZlaWartosc()
        {
            //Arrange
            Ocena ocena = new Ocena();

            //Assert

            Assert.ThrowsException<DziennikException>(() =>
            {
                ocena.Wartosc = 7;
            });

        }
        

        [TestMethod]
        public void TestKonstruktoraNauczyciela()
        {
            // Arrange 
            string imie = "Adam";
            string nazwisko = "Mickiewicz";
            string pesel = "80010112345"; 
            string login = "wieszcz";
            string haslo = "Dziady44";
            Przedmiot przedmiot = Przedmiot.JezykPolski; 

            // Act 
            Nauczyciel n = new Nauczyciel(imie, nazwisko, pesel, login, haslo, przedmiot);

            // Assert

            // 1. Sprawdzamy pola dziedziczone z klasy Osoba (base)
            Assert.AreEqual(imie, n.Imie);
            Assert.AreEqual(nazwisko, n.Nazwisko);
            Assert.AreEqual(pesel, n.Pesel);
            Assert.AreEqual(login, n.Login);

            // 2. Sprawdzamy pole specyficzne dla Nauczyciela
            Assert.AreEqual(przedmiot, n.Przedmiot);
        }

        /// <summary>
        /// Ponizsza funkcja ma na celu sprawdzenie walidacji logowania sie do Dzienniczka
        /// </summary>


        [TestMethod]



        public void TestLogowania()
        {
            //Arrange
            DataManager.Uczniowie = new List<Uczen>();
            DataManager.Nauczyciele = new List<Nauczyciel>();
          
            DataManager.Uczniowie.Add(new Uczen("Jan", "Kowalski", "00000000000", "uczen1", "haslo123", "1A"));
            DataManager.Nauczyciele.Add(new Nauczyciel("Anna", "Nowak", "11111111111", "nauczyciel1", "haslo123", Przedmiot.Biologia));


            //Act + Assert

            //Przypadek A Poprawne logowanie ucznia

            Osoba zalogowanyUczen = DataManager.Zaloguj("uczen1", "haslo123");
            Assert.IsNotNull(zalogowanyUczen);

            //Przypadek B Poprawne logowanie Nauczyciela

            Osoba zalogowanyNauczyciel = DataManager.Zaloguj("nauczyciel1", "haslo123");
            Assert.IsNotNull(zalogowanyNauczyciel);

            //Przypadek C Bledne haslo


            Osoba nieudaneLogowanie = DataManager.Zaloguj("uczen1", "zlehaslo");
            Assert.IsNull(nieudaneLogowanie);





        }






    }
}
