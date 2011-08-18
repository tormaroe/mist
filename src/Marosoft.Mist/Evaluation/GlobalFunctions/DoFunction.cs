using System;
using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;
using System.Collections.Generic;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class DoFunction : BuiltInFunction
    {
        public DoFunction(Bindings scope) : base("do", scope) { }

        // Can't see why do would need to be a special form?!?
        // Could easily be implemented in mist as well, once I have '&rest'

        protected override Expression InternalCall(IEnumerable<Expression> args)
        {
            return args.Last();
        }
    }
}
