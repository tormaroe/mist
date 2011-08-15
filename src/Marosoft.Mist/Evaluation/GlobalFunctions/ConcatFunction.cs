using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;
using System.Collections.Generic;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class ConcatFunction : BuiltInFunction
    {
        public ConcatFunction(Bindings scope) : base("concat", scope) { }

        protected override Expression InternalCall(IEnumerable<Expression> args)
        {
            IEnumerable<Expression> list1 = new List<Expression>();

            foreach (var otherList in args)
                if (!otherList.IsNil)
                    list1 = list1.Concat(((ListExpression)otherList).Elements);

            return new ListExpression(list1);
        }

        protected override bool Precondition(IEnumerable<Expression> args)
        {
            return args.Count() >= 2
                   &&
                   args.All(a => a is ListExpression || a.IsNil);
        }
    }
}
