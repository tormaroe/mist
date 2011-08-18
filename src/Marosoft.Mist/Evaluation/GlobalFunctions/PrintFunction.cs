using System;
using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;
using System.Collections.Generic;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class PrintFunction : BuiltInFunction
    {
        public PrintFunction(Bindings scope) : base("print", scope) { }

        protected override Expression InternalCall(IEnumerable<Expression> args)
        {
            string output = args
                .Select(e => e.Value.ToString())
                .Aggregate((e1, e2) => string.Format("{0} {1}", e1, e2));

            Console.Write(output);
            return StringExpression.Create(output);
        }
    }
}