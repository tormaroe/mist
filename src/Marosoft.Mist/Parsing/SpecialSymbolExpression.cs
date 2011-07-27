using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Parsing
{
    public class SpecialSymbolExpression : Expression
    {
        public SpecialSymbolExpression(string name, object value)
            : base(new Token(Tokens.SYMBOL, name))
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override bool IsTrue
        {
            get
            {
                if (Token.Text == "false" || Token.Text == "nil")
                    return false;

                return base.IsTrue;
            }
        }
    }
}