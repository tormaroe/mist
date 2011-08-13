using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class DocFunction : BuiltInFunction
    {
        public DocFunction(Bindings scope)
            : base("Doc", scope)
        {
            DocString = StringExpression.Create(
                "Prints documentation for a var or special form given its name.");

            // TODO: print doc instead of returning, include info about formal parameters
            // Could also print a ruler
            // see http://clojuredocs.org/clojure_core/clojure.core/doc

            Precondition = args =>
                args.Count() == 1
                && args.First().Token.Type == Tokens.SYMBOL;

            Implementation = args =>
                args.First().DocString;
        }
    }
}
