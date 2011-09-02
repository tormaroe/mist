using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class DocFunction : BuiltInFunction
    {
        // Change to Special form, then get doc through the Scope (from the symbol, not the value)

        public DocFunction(Bindings scope)
            : base("doc", scope)
        {
            DocString =
                "Prints documentation for a var or special form given its name."
                .ToExpression();

            // TODO: print doc instead of returning, include info about formal parameters
            // Could also print a ruler
            // see http://clojuredocs.org/clojure_core/clojure.core/doc

        }

        protected override Expression InternalCall(System.Collections.Generic.IEnumerable<Expression> args)
        {
            var x = args.First();
            return x.DocString ?? NIL.Instance;
        }

        protected override bool Precondition(System.Collections.Generic.IEnumerable<Expression> args)
        {
            return args.Count() == 1
                && args.First().Token.Type == Tokens.SYMBOL;
        }
    }
}
