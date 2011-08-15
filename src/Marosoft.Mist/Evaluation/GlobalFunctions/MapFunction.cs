using System.Linq;
using Marosoft.Mist.Parsing;
using System.Collections.Generic;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class MapFunction : ListManipulatorFunction
    {
        public MapFunction(Bindings scope) : base("map", scope) { }

        protected override Expression InternalCall(Function f, IEnumerable<Expression> args)
        {
            var list = args.GetAt<ListExpression>(0).Elements;
            return new ListExpression(list.Select(a => f.Call(a)));
        }
    }
}