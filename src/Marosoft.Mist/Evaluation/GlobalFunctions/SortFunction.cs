using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;
using System.Collections.Generic;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class SortFunction : BuiltInFunction
    {
        public SortFunction(Bindings scope) : base("sort", scope) { }

        protected override Expression InternalCall(IEnumerable<Expression> args)
        {
            var list = args.GetAt<ListExpression>(0);
            var result = new ListExpression(list.Elements.OrderBy(expr => expr.Value));
            return result;
        }

        protected override bool Precondition(IEnumerable<Expression> args)
        {
            // TODO: optionally provide comperator

            return args.Count() == 2
                && args.First() is ListExpression;
        }
    }
}
