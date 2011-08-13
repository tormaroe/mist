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
                var f = args.First() as Function;

                var result = new ListExpression();
                result.Elements.AddRange(((ListExpression)args.Second())
                    .Elements
                    .Select(a => new Expression[]{a}.AsEnumerable<Expression>())
                    .Select(a => f.Call(a)));
                return result;
            };
        }
    }
}
