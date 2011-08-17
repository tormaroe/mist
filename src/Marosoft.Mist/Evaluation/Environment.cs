using System.Collections.Generic;
using Marosoft.Mist.Parsing;
using System;

namespace Marosoft.Mist.Evaluation
{
    public interface Environment
    {
        Bindings CurrentScope { get; }
        Expression EvaluateString(string code);
        T WithScope<T>(Bindings s, Func<T> call);
    }
}
