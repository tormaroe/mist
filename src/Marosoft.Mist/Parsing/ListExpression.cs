using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Parsing
{
    public class ListExpression : Expression
    {
        public ListExpression(IEnumerable<Expression> elements)
            : this()
        {
            Elements.AddRange(elements);
        }

        public ListExpression()
            : base(new Token(Tokens.LIST, string.Empty))
        {

        }

        public override string ToString()
        {
            return string.Format("({0})",
                Elements.Select(e => e.ToString())
                        .Aggregate((acc, e) => string.Format("{0} {1}", acc, e)));
        }
    }
}
