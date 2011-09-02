using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;
using System.Collections.Generic;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class AllBindingsFunction : BuiltInFunction
    {
        public AllBindingsFunction(Bindings scope) : base("all-bindings", scope) { }

        protected override Expression InternalCall(IEnumerable<Expression> args)
        {
            return new ListExpression(Scope.AllBindings);
        }

        protected override bool Precondition(IEnumerable<Expression> args)
        {
            return args.Count() == 0;
        }
    }
}
