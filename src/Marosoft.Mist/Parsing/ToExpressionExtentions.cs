using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marosoft.Mist.Lexing;
using Marosoft.Mist.Evaluation;

namespace Marosoft.Mist.Parsing
{
    public static class ToExpressionExtentions
    {
        public static IntExpression ToExpression(this int i)
        {
            return new IntExpression(new Token(Tokens.INT, i.ToString()));
        }

        public static StringExpression ToExpression(this string s)
        {
            return new StringExpression(new Token(Tokens.STRING, "\"" + s + "\""));
        }

        public static Expression ToExpression(this bool b)
        {
            if (b) 
                return TRUE.Instance; 
            return FALSE.Instance;
        }

        public static Expression ToExpression(this object o)
        {
            if (o is string)
                return ((string)o).ToExpression();
            if (o is int)
                return ((int)o).ToExpression();
            if (o is bool)
                return ((bool)o).ToExpression();
            
            if (o is Expression)
                return (Expression)o;

            throw new NotImplementedException(
                "ToExpression extention not implemented for "
                + o.GetType());
        }
    }
}
