using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace test
{
    public static class MoreLinq
    {
        public static T Second<T>(this IEnumerable<T> self)
        {
            return self.Skip(1).First();
        }
        public static T Third<T>(this IEnumerable<T> self)
        {
            return self.Skip(2).First();
        }
    }
}
