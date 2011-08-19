using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;
using System.Collections.Generic;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class ApplyFunction : BuiltInFunction
    {
        public ApplyFunction(Bindings scope) : base("apply", scope) { }

        protected override Expression InternalCall(IEnumerable<Expression> args)
        {
            var f = args.First() as Function;
            return f.Call(args.Second().Elements);
        }

        protected override bool Precondition(IEnumerable<Expression> args)
        {
            return args.Count() == 2
                && args.First() is Function
                && args.Second() is ListExpression;
        }
    }
}
