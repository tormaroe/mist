using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class AddFunction : BuiltInFunction
    {
        public AddFunction(Bindings scope) : base("+", scope) {}

        protected override Expression InternalCall(System.Collections.Generic.IEnumerable<Expression> args)
        {
            return ExpressionFactory.Create(new Token(args.First().Token.Type,
                    args.Select(expr => (int)expr.Value).Sum().ToString()));
        }

        protected override bool Precondition(System.Collections.Generic.IEnumerable<Expression> args)
        {
            return args.Count() > 0
                && args.All(a => a.Token.Type == Tokens.INT);
        }
    }
}
