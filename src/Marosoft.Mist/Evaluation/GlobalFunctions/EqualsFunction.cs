using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class EqualsFunction : BuiltInFunction
    {
        public EqualsFunction(Bindings scope)
            : base("=", scope)
        {
            Precondition = args => args.Count() >= 2;

            Implementation = args =>
            {
                var firstValue = args.First().Value;
                if (args.Skip(1).All(x => x.Value.Equals(firstValue)))
                    return Scope.Resolve("true");
                return Scope.Resolve("false");
            };
        }
    }
}
