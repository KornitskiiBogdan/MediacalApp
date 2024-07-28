using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class StringComparer : IEqualityComparer<string>
    {
        public static StringComparer Ordinal = new StringComparer(StringComparison.Ordinal);

        public static StringComparer CurrentCulturer = new StringComparer(StringComparison.CurrentCulture);

        public static StringComparer InvariantCulture = new StringComparer(StringComparison.InvariantCulture);

        public static StringComparer OrdinalIgnoreCase = new StringComparer(StringComparison.OrdinalIgnoreCase);

        public static StringComparer CurrentCultureIgnoreCase = new StringComparer(StringComparison.CurrentCultureIgnoreCase);

        public static StringComparer InvariantCultureIgnoreCase = new StringComparer(StringComparison.InvariantCultureIgnoreCase);

        private readonly StringComparison _stringComparison;
        public StringComparer(StringComparison stringComparison)
        {
            _stringComparison = stringComparison;
        }

        public bool Equals(string? x, string? y)
        {
            return string.Equals(x, y, _stringComparison);
        }

        public int GetHashCode(string obj)
        {
            return 0;
        }
    }
}
