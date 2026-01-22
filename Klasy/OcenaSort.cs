using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klasy
{
    public class OcenaSort : IComparer<Ocena>
    {
        public int Compare(Ocena? x, Ocena? y)
        {
            if (x == null || y == null) return 0;
            return y.Wartosc.CompareTo(x.Wartosc);
        }
    }
}
