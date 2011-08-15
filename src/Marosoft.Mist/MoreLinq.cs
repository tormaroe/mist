using System.Collections.Generic;
using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist
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
        public static T Forth<T>(this IEnumerable<T> self)
        {
            return self.Skip(3).First();
        }

        public static T GetAt<T>(this IEnumerable<Expression> self, int index) where T : Expression
        {
            return (T) self.Skip(index).First();
        }
    }
}
