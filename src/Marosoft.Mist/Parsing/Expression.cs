using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Parsing
{
    public class Expression
    {
        public Token Token { get; private set; }
        public List<Expression> Elements { get; private set; }
        public object Value { get; set; }

        public Expression(Token token)
        {
            Token = token;
            Elements = new List<Expression>();

            MaybeSetValue();
        }

        private void MaybeSetValue()
        {
            switch (Token.Type)
            {
                case Tokens.INT: Value = Int32.Parse(Token.Text); break;
            }    
        }

        public override string ToString()
        {
            if (Value != null)
                return Value.ToString();

            return Token.ToString();
        }
    }
}