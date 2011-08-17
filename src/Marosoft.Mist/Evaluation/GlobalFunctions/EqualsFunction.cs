using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;
using System.Collections.Generic;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class EqualsFunction : BuiltInFunction
    {
        public EqualsFunction(Bindings scope) : base("=", scope) {}

        protected override Expression InternalCall(IEnumerable<Expression> args)
        {
            var firstValue = args.First().Value;
            if (args.Skip(1).All(x => x.Value.Equals(firstValue)))
                return TRUE.Instance;
            return FALSE.Instance;
        }

        protected override bool Precondition(IEnumerable<Expression> args)
        {
            return args.Count() >= 2;
        }
    }
}
