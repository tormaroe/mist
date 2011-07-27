using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Parsing
{
    public class IntExpression : Expression
    {
        public IntExpression(Token t)
            : base(t)
        {
            Value = Int32.Parse(Token.Text);
        }
    }
}
