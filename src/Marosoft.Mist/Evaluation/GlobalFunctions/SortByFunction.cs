using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;
using System.Collections.Generic;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class SortByFunction : BuiltInFunction
    {
        public SortByFunction(Bindings scope) : base("sort-by", scope) { }

        protected override Expression InternalCall(IEnumerable<Expression> args)
        {
            var f = (Function)args.First();
            var list = args.GetAt<ListExpression>(1);
            var result = new ListExpression(list.Elements.OrderBy(expr => f.Call(expr).Value));
            return result;
        }

        protected override bool Precondition(IEnumerable<Expression> args)
        {
            return args.Count() == 2
                && args.First() is Function
                && args.Second() is ListExpression;
        }
    }
}