using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;
using System.Collections.Generic;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class ReverseFunction : BuiltInFunction
    {
        public ReverseFunction(Bindings scope) : base("reverse", scope) { }

        protected override Expression InternalCall(IEnumerable<Expression> args)
        {
            var list = args.GetAt<ListExpression>(0);
            var result = new ListExpression(list.Elements);
            result.Elements.Reverse(); 
            return result;
        }

        protected override bool Precondition(IEnumerable<Expression> args)
        {
            return args.Count() == 1
                   &&
                   args.First() is ListExpression;
        }
    }
}
