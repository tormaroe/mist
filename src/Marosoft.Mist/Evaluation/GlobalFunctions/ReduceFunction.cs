using System.Linq;
using Marosoft.Mist.Parsing;
using System.Collections.Generic;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class ReduceFunction : ListManipulatorFunction
    {
        public ReduceFunction(Bindings scope) : base("reduce", scope) { }

        protected override Expression InternalCall(Function f, IEnumerable<Expression> args)
        {
            args = PreProcessArguments(args);
            var list = ((ListExpression)args.First()).Elements;
            return list.Aggregate((acc, x) => f.Call(new Expression[] { acc, x }));
        }

        protected IEnumerable<Expression> PreProcessArguments(IEnumerable<Expression> arguments)
        {
            var argCount = arguments.Count();

            if (argCount == 1 && arguments.First() is ListExpression)
                return arguments;

            if (argCount == 2 && arguments.Second() is ListExpression)
                return PrependSeedToList(arguments);

            throw new MistException("reduce does not know how to handle " + argCount + " arguments.");
        }

        private IEnumerable<Expression> PrependSeedToList(IEnumerable<Expression> arguments)
        {
            var list = arguments.GetAt<ListExpression>(1);
            list.Elements.Insert(0, arguments.First());
            yield return list;
        }
    }
}
