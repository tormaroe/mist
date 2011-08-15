using System.Linq;
using Marosoft.Mist.Parsing;
using System.Collections.Generic;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class ReduceFunction : ListManipulatorFunction
    {
        public ReduceFunction(Bindings scope)
            : base(
                symbol: "reduce",
                scope: scope,
                manipulation: (f, list) => list.Aggregate((acc, x) => f.Call(new Expression[] { acc, x })))
        { }

        protected override IEnumerable<Expression> PreProcessArguments(IEnumerable<Expression> arguments)
        {
            var argCount = arguments.Count();

            if (argCount == 2 && arguments.Second() is ListExpression)
                return arguments;

            if (argCount == 3 && arguments.Third() is ListExpression)
                return PrependSeedToList(arguments);

            throw new MistException("reduce does not know how to handle " + argCount + " arguments.");
        }

        private IEnumerable<Expression> PrependSeedToList(IEnumerable<Expression> arguments)
        {
            yield return arguments.First();

            var list = (ListExpression)arguments.Third();
            list.Elements.Insert(0, arguments.Second());
            yield return list;
        }
    }
}
