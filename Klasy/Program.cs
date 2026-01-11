using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APKA;
using Klasy;

internal class Program
{
    private static void Main(string[] args)
    {
        
        Nauczyciel n1 = new Nauczyciel("Paweł", "Lemaniak", "04290445321", Przedmiot.Matematyka);
        Nauczyciel n2 = new Nauczyciel("Małgorzata", "Lemaniak", "04290445321", Przedmiot.JezykPolski);
        Uczen u1 = new Uczen("Jan", "Kowalski", "04290445321");
        Uczen u2 = new Uczen("Anna", "Miszalsa", "67123456789");

    }
}