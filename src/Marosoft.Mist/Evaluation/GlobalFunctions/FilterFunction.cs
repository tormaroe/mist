using System.Linq;
using Marosoft.Mist.Parsing;
using System.Collections.Generic;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class FilterFunction : ListManipulatorFunction
    {
        public FilterFunction(Bindings scope) : base("filter", scope) { }

        protected override Expression InternalCall(Function f, IEnumerable<Expression> args)
        {
            var list = args.GetAt<ListExpression>(0).Elements;
            return new ListExpression(list.Where(a => f.Call(a).IsTrue));
        }
    }
}
