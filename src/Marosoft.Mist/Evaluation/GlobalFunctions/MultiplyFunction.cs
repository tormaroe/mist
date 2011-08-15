using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;
using System.Collections.Generic;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class MultiplyFunction : BuiltInFunction
    {
        public MultiplyFunction(Bindings scope) : base("*", scope) { }

        protected override bool Precondition(IEnumerable<Expression> args)
        {
            return args.Count() > 0
                && args.All(a => a.Token.Type == Tokens.INT);
        }

        protected override Expression InternalCall(IEnumerable<Expression> args)
        {
            return ExpressionFactory.Create(new Token(args.First().Token.Type,
                    args.Select(expr => (int)expr.Value).Aggregate((x, y) => x * y).ToString()));
        }
    }
}
