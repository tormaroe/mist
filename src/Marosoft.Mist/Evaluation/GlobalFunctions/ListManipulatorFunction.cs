using System;
using System.Linq;
using Marosoft.Mist.Parsing;
using System.Collections.Generic;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    // TODO: ListManipulatorFunction has turned out to be thr wrong name
    // This should now be merged with BuiltInFunction
    // REFACTOR REFACTOR REFACTOR

    public abstract class ListManipulatorFunction : BuiltInFunction
    {
        public ListManipulatorFunction(string symbol, Bindings scope)            
            : base(symbol, scope)
        {
            Implementation = args => InternalCall((Function)args.First(), args.Skip(1));
        }

        protected abstract Expression InternalCall(Function f, IEnumerable<Expression> args);
    }
}
