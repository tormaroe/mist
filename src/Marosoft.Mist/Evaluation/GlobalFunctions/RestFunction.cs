using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;
using System.Collections.Generic;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class RestFunction : BuiltInFunction
    {
        public RestFunction(Bindings scope)
            : base("rest", scope)
        {
        }

        protected override Expression InternalCall(IEnumerable<Expression> args)
        {
            return new ListExpression(((ListExpression)args.First()).Elements.Skip(1));
        }

        protected override bool Precondition(IEnumerable<Expression> args)
        {
            return
                args.Count() == 1
                &&
                args.First() is ListExpression;
        }
    }
}
