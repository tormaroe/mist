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
            if (Value == null)
                return "nil";
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

        public override bool IsNil
        {
            get
            {
                if (Token.Text == "nil")
                    return true;

                return base.IsNil;
            }
        }

    }

}
