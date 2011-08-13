using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class ListFunction : BuiltInFunction
    {
        public ListFunction(Bindings scope)
            : base("list", scope)
        {
            Precondition = args =>
                args.Count() > 0;

            Implementation = args =>
                new ListExpression(args);
        }
    }
}
