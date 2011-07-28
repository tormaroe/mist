using System;
using System.Linq;
using System.Collections.Generic;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation
{
    public class Function : Expression, Scope
    {
        protected BasicScope _functionScope;
        protected Environment _environment; // ONLY SET BY SUBCLASS !! Refactor !!
        protected Expression _formalParameters;
        
        public Scope ParentScope
        {
            get
            {
                return _functionScope.ParentScope;
            }
            set
            {
                _functionScope.ParentScope = value;
            }
        }
        
        public Function(string symbol, Scope parentScope)
            : base(new Token(Tokens.FUNCTION, symbol))
        {
            // WARNING, Scope not used yet...
            _functionScope = new BasicScope();
            Precondition = args => Implementation != null;
        }

        public Predicate<IEnumerable<Expression>> Precondition { get; set; }
        public Func<IEnumerable<Expression>, Expression> Implementation { get; set; }

        public Expression Call(IEnumerable<Expression> args)
        {
            if (!Precondition(args))
                throw new FunctionEvaluationPreconditionException(Token.Text, args);

            var invocationScope = new BasicScope() { ParentScope = this };
            return WithArgumentBindings(invocationScope, args, Implementation);
        }

        public Expression Resolve(string symbol)
        {
            return _functionScope.Resolve(symbol);
        }

        public void AddBinding(string symbol, Expression expr)
        {
            _functionScope.AddBinding(symbol, expr);
        }

        private T WithArgumentBindings<T>(
            Scope invocationScope,
            IEnumerable<Expression> args,
            Func<IEnumerable<Expression>, T> functionNeedingBindings)
        {
            BindArguments(invocationScope, args);
            if (_environment != null)
                return _environment.WithScope(invocationScope, () => functionNeedingBindings.Invoke(args));
            return functionNeedingBindings.Invoke(args);
        }

        private void BindArguments(Scope invocationScope, IEnumerable<Expression> args)
        {
            if (_formalParameters != null)
                for (int i = 0; i < _formalParameters.Elements.Count; i++)
                    invocationScope.AddBinding(
                        _formalParameters.Elements[i].Token.Text,
                        args.ElementAt(i));
        }

    }
}
