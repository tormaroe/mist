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
                case Tokens.STRING: Value = Token.Text.Substring(1, Token.Text.Length - 2); break;
            }    
        }

        public override string ToString()
        {
            if (Value != null)
                return Value.ToString();

            return Token.ToString();
        }

        public bool IsTrue
        {
            get
            {
                if (Token.Type == Tokens.SYMBOL 
                    && (Token.Text == "false" || Token.Text == "nil"))
                    return false;

                return true;
            }
        }
    }
}