using System.Linq;
using System.Collections.Generic;
using Marosoft.Mist.Parsing;

namespace Marosoft.Mist.Evaluation
{
    public abstract class BuiltInFunction : ScopedExpression, Function
    {
        public BuiltInFunction(string symbol, Bindings scope) : base(symbol, scope) { }
        
        public Expression Call(IEnumerable<Expression> args)
        {
            return InternalCall(args);
        }

        protected virtual bool Precondition(IEnumerable<Expression> args) 
        {
            return true;
        }

        protected abstract Expression InternalCall(IEnumerable<Expression> args);
    }
}
