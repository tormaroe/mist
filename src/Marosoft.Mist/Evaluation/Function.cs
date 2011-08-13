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
            return f.Call(new Expression[] { expr }.AsEnumerable());
        }
    }
}