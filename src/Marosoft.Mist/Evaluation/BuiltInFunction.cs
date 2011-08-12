using System;
using System.Linq;
using System.Collections.Generic;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation
{
    public class BuiltInFunction : Expression, Function
    {
        protected Bindings _functionScope;
        protected Expression _formalParameters;

        public BuiltInFunction(string symbol)
            : base(new Token(Tokens.FUNCTION, symbol))
        {
            Precondition = args => Implementation != null;
        }

        public Predicate<IEnumerable<Expression>> Precondition { get; set; }
        public Func<IEnumerable<Expression>, Expression> Implementation { get; set; }

        public Expression Call(IEnumerable<Expression> args)
        {
            return WithArgumentBindings(
                new Bindings() { ParentScope = null }, 
                args, 
                Implementation);
        }

        private T WithArgumentBindings<T>(
            Bindings invocationScope,
            IEnumerable<Expression> args,
            Func<IEnumerable<Expression>, T> functionNeedingBindings)
        {
            BindArguments(invocationScope, args);
            return functionNeedingBindings.Invoke(args);
        }

        private void BindArguments(Bindings invocationScope, IEnumerable<Expression> args)
        {
            if (_formalParameters != null)
                for (int i = 0; i < _formalParameters.Elements.Count; i++)
                    invocationScope.AddBinding(
                        _formalParameters.Elements[i].Token.Text,
                        args.ElementAt(i));
        }

    }
}
