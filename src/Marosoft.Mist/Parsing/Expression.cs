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

        public Expression(Token token)
        {
            Token = token;
            Elements = new List<Expression>();
        }
    }
}