using System;
using System.Collections.Generic;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation
{
    public class Function : Expression, Scope
    {
        private Scope _functionScope;

        public Function(string symbol, Scope parentScope)
            : base(new Token(Tokens.SYMBOL, symbol))
        {
            _functionScope = new BasicScope(parentScope);
            Precondition = args => Implementation != null;
        }

        public Predicate<IEnumerable<Expression>> Precondition { get; set; }
        public Func<IEnumerable<Expression>, Expression> Implementation { get; set; }

        public Expression Call(IEnumerable<Expression> args)
        {
            if (!Precondition(args))
                throw new FunctionEvaluationPreconditionException(Token.Text, args);

            return Implementation.Invoke(args);
        }

        public Expression Resolve(string symbol)
        {
            return _functionScope.Resolve(symbol);
        }

        public void AddBinding(string symbol, Expression expr)
        {
            _functionScope.AddBinding(symbol, expr);
        }

    }
}
