using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class RestFunction : BuiltInFunction
    {
        public RestFunction(Bindings scope)
            : base("rest", scope)
        {
            Precondition = args =>
                args.Count() == 1
                &&
                args.First() is ListExpression;

            Implementation = args =>
                new ListExpression(
                    ((ListExpression)args.First()).Elements.Skip(1));
        }
    }
}
