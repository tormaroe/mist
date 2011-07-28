using System.Collections.Generic;
using Marosoft.Mist.Parsing;
using System;

namespace Marosoft.Mist.Evaluation
{
    public interface Environment
    {
        Scope CurrentScope { get; }
        Expression Evaluate(IEnumerable<Expression> expressions);
        Expression Evaluate(Expression expr);
        T WithScope<T>(Scope s, Func<T> call);
    }
}
