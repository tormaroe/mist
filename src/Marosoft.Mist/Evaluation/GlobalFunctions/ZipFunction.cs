using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;
using System.Collections.Generic;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class ZipFunction : BuiltInFunction
    {
        public ZipFunction(Bindings scope) : base("zip", scope) { }

        protected override Expression InternalCall(IEnumerable<Expression> args)
        {
            var f = (Function)args.First();
            var list1 = args.GetAt<ListExpression>(1).Elements;
            var list2 = args.GetAt<ListExpression>(2).Elements;
            return new ListExpression(list1.Zip(list2, (e1, e2) => f.Call(e1, e2)));
        }

        protected override bool Precondition(IEnumerable<Expression> args)
        {
            return args.Count() == 3
                   &&
                   args.First() is Function
                   &&
                   args.Skip(1).All(a => a is ListExpression);
        }
    }
}
