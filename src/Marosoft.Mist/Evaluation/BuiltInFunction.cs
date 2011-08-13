using System;
using System.Linq;
using System.Collections.Generic;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation
{

    public abstract class FunctionExpression : Expression
    {
        protected Bindings Scope;

        public FunctionExpression(string symbol, Bindings scope)
            : base(new Token(Tokens.FUNCTION, symbol))
        {
            Scope = new Bindings() { ParentScope = scope };
        }
    }

    public class BuiltInFunction : FunctionExpression, Function
    {
        protected Bindings _functionScope;

        public BuiltInFunction(string symbol, Bindings scope)
            : base(symbol, scope)
        {
            Precondition = args => Implementation != null;
        }

        public virtual Predicate<IEnumerable<Expression>> Precondition { get; set; }
        public virtual Func<IEnumerable<Expression>, Expression> Implementation { get; set; }

        public Expression Call(IEnumerable<Expression> args)
        {
            return Implementation.Invoke(args);
        }
    }
}
