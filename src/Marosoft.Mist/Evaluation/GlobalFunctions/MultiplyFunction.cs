using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class MultiplyFunction : BuiltInFunction
    {
        public MultiplyFunction(Bindings scope)
            : base("*", scope)
        {
            Precondition = args =>
                args.Count() > 0
                && args.All(a => a.Token.Type == Tokens.INT);

            Implementation = args =>
                ExpressionFactory.Create(new Token(args.First().Token.Type,
                    args.Select(expr => (int)expr.Value).Aggregate((x, y) => x * y).ToString()));
        }
    }
}
