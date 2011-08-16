using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;
using System.Collections.Generic;
using System;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    public abstract class LTGTFunctionBase : BuiltInFunction
    {
        public LTGTFunctionBase(string symbol, Bindings scope) : base(symbol, scope) { }

        protected Func<int, int, bool> Comparer { get; set; }

        protected override Expression InternalCall(IEnumerable<Expression> args)
        {
            var argsArray = args.Select(a => (int)a.Value).ToArray();
            for (int i = 0; i < argsArray.Length - 1; i++)
            {
                if (!(Comparer(argsArray[i], argsArray[i + 1])))
                    return Scope.Resolve("false");
            }

            return Scope.Resolve("true");
        }

        protected override bool Precondition(IEnumerable<Expression> args)
        {
            return args.Count() >= 2
                && args.All(a => a.Token.Type == Tokens.INT);
        }
    }
}
