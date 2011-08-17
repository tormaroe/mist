using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;
using System.Collections.Generic;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class ErrorFunction : BuiltInFunction
    {
        public ErrorFunction(Bindings scope) : base("error", scope) { }

        protected override Expression InternalCall(IEnumerable<Expression> args)
        {
            throw new MistApplicationException(args.Select(a => a.Evaluate(Scope).Value.ToString()));
        }
    }
}
