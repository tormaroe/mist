using System.Linq;
using Marosoft.Mist.Parsing;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{

    [GlobalFunction]
    public class MapFunction : ListManipulatorFunction
    {
        public MapFunction(Bindings scope)
            : base(
                symbol: "map",
                scope: scope,
                manipulation: (f, list) => new ListExpression(list.Select(a => f.Call(a))))
        { }
    }
}