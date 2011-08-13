using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class MapFunction : BuiltInFunction
    {
        public MapFunction(Bindings scope)
            : base("map", scope)
        {
            Precondition = args =>
                args.Count() == 2 // Enhance to take more than one list
                &&
                args.First() is Function
                &&
                args.Second() is ListExpression;

            Implementation = args =>
            {
                var f = (Function) args.First();

                var sourceList = (ListExpression) args.Second();

                return new ListExpression(
                    sourceList.Elements.Select(a => f.Call(a)));                
            };
        }
    }
}