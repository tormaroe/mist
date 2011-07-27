using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Parsing
{
    public class SymbolExpression : Expression
    {
        public SymbolExpression(string name)
            : base(new Token(Tokens.SYMBOL, name))
        {
        }
        public SymbolExpression(Token token)
            : base(token)
        {
        }
    }
}
