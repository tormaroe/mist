using System.Linq;
using Marosoft.Mist.Parsing;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class FilterFunction : ListManipulatorFunction
    {
        public FilterFunction(Bindings scope)
            : base(
                symbol: "filter",
                scope: scope,
                manipulation: (f, list) => new ListExpression(list.Where(a => f.Call(a).IsTrue)))
        { }
    }
}
