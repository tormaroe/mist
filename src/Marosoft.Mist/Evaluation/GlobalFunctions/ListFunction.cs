using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;
using System.Collections.Generic;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class ListFunction : BuiltInFunction
    {
        public ListFunction(Bindings scope)
            : base("list", scope)
        {
        }

        protected override Expression InternalCall(IEnumerable<Expression> args)
        {
            return new ListExpression(args);
        }

    }
}
