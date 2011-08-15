using System.Linq;
using Marosoft.Mist.Parsing;
using System.Collections.Generic;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class FilterFunction : BuiltInFunction
    {
        public FilterFunction(Bindings scope) : base("filter", scope) { }

        protected override Expression InternalCall(IEnumerable<Expression> args)
        {
            var f = (Function)args.First();
            var list = args.GetAt<ListExpression>(1).Elements;
            return new ListExpression(list.Where(a => f.Call(a).IsTrue));
        }
    }
}
