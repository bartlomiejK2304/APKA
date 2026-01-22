using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klasy
{
    /// <summary>
    /// Klasa implementująca interfejs IComparer do sortowania ocen.
    /// Umożliwia sortowanie listy ocen malejąco według ich wartości.
    /// </summary>
    public class OcenaSort : IComparer<Ocena>
    {
        /// <summary>
        /// Porównuje dwie oceny.
        /// </summary>
        /// <returns>Wartość dodatnia jeśli y > x, ujemna jeśli x > y (sortowanie malejące).</returns>
        public int Compare(Ocena? x, Ocena? y)
        {
            if (x == null || y == null) return 0;
            return y.Wartosc.CompareTo(x.Wartosc);
        }
    }
}