using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class AddFunction : BuiltInFunction
    {
        public AddFunction(Bindings scope)
            : base("+", scope)
        {
            Precondition = args =>
                args.Count() > 0
                && args.All(a => a.Token.Type == Tokens.INT);

            Implementation = args =>
                ExpressionFactory.Create(new Token(args.First().Token.Type,
                    args.Select(expr => (int)expr.Value).Sum().ToString()));
        }
    }
}
