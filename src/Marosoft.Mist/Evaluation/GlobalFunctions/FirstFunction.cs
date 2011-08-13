using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class FirstFunction : BuiltInFunction
    {
        public FirstFunction(Bindings scope)
            : base("first", scope)
        {
            Precondition = args =>
                args.Count() == 1
                &&
                args.First() is ListExpression;

            Implementation = args =>
            {
                var list = (ListExpression)args.First();
                if (list.Elements.Count > 0)
                    return list.Elements.First();
                else
                    return Scope.Resolve("nil");
            };
        }
    }
}