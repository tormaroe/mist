using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Parsing
{
    public class ListExpression : Expression
    {
        public ListExpression()
            : base(new Token(Tokens.LIST, string.Empty))
        {

        }

        public override string ToString()
        {
            return "TODO LIST TOSTRING...";
        }
    }
}
