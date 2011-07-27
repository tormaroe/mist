using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Parsing
{
    public class StringExpression : Expression
    {
        public static StringExpression Create(string s)
        {
            return new StringExpression(new Token(Tokens.STRING, "\"" + s + "\""));
        }

        public StringExpression(Token t)
            : base(t)
        {
            Value = Token.Text.Substring(1, Token.Text.Length - 2);
        }

        public override string ToString()
        {
            return Token.Text;
        }
    }
}
