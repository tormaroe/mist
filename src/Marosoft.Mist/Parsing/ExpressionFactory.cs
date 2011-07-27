using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Parsing
{
    public static class ExpressionFactory
    {
        public static Expression Create(Token token)
        {
            switch (token.Type)
            {
                case Tokens.SYMBOL: return new SymbolExpression(token);
                case Tokens.INT: return new IntExpression(token);
                case Tokens.STRING: return new StringExpression(token);
                default: return new Expression(token);
            }
        }
    }
}
