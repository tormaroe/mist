using System.Collections.Generic;
using Marosoft.Mist.Parsing;

namespace Marosoft.Mist.Evaluation
{
    public interface Function
    {
        Expression Call(IEnumerable<Expression> args);
    }
}