using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;
using System.Collections.Generic;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class FirstFunction : BuiltInFunction
    {
        public FirstFunction(Bindings scope) : base("first", scope) {}

        protected override Expression InternalCall(IEnumerable<Expression> args)
        {
            var list = (ListExpression)args.First();
            if (list.Elements.Count > 0)
                return list.Elements.First();
            else
                return NIL.Instance;
        }

        protected override bool Precondition(IEnumerable<Expression> args)
        {
            return args.Count() == 1
                &&
                args.First() is ListExpression;
        }
    }
}