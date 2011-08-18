using System.Collections.Generic;
using Marosoft.Mist.Parsing;
using System;
using System.Linq;

namespace Marosoft.Mist.Evaluation
{
    public interface Function
    {
        Expression Call(IEnumerable<Expression> args);
    }

    public static class FunctionExtensions
    {
        public static Expression Call(this Function f, Expression expr)
        {
            return f.Call(expr.AsEnumerable());
        }

        public static Expression Call(this Function f, params Expression[] exprs)
        {
            return f.Call(exprs.AsEnumerable());
        }
    }
}