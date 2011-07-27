using System;
using System.Linq;
using System.Collections.Generic;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation
{
    public class Function : Expression, Scope
    {
        private Expression _formalParameters;
        private BasicScope _functionScope;

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

        public Function(Expression expr, Environment environment)
            : base(new Token(Tokens.FUNCTION, "anonymous"))
        {
            _functionScope = new BasicScope();

            _formalParameters = expr.Elements.Second();

            if (!(_formalParameters is ListExpression))
                throw new MistException("First argument to fn must be a list, not " + _formalParameters);

            if (!_formalParameters.Elements.All(p => p.Token.Type == Tokens.SYMBOL))
                throw new MistException("Only symbols allowed in fn's list of formal paremeters. " + _formalParameters);

            Precondition = args => args.Count() == _formalParameters.Elements.Count;
            Implementation = args => environment.Evaluate(expr.Elements.Third());
        }

        public Function(string symbol, Scope parentScope)
            : base(new Token(Tokens.FUNCTION, symbol))
        {
            _functionScope = new BasicScope();
            Precondition = args => Implementation != null;
        }

        public Predicate<IEnumerable<Expression>> Precondition { get; set; }
        public Func<IEnumerable<Expression>, Expression> Implementation { get; set; }

        public Expression Call(IEnumerable<Expression> args)
        {
            if (!Precondition(args))
                throw new FunctionEvaluationPreconditionException(Token.Text, args);

            return WithArgumentBindings(args, Implementation);
        }

        public Expression Resolve(string symbol)
        {
            return _functionScope.Resolve(symbol);
        }

        private T WithArgumentBindings<T>(IEnumerable<Expression> args, 
            Func<IEnumerable<Expression>, T> functionNeedingBindings)
        {
            try
            {
                BindArguments(args);
                return functionNeedingBindings.Invoke(args);
            }
            finally
            {
                RemoveArgumentBindings();
            }
        }

        private void BindArguments(IEnumerable<Expression> args)
        {
            if (_formalParameters != null)
                for (int i = 0; i < _formalParameters.Elements.Count; i++)
                    _functionScope.AddBinding(
                        _formalParameters.Elements[i].Token.Text, 
                        args.ElementAt(i));
        }

        private void RemoveArgumentBindings()
        {
            if (_formalParameters != null)
                _formalParameters.Elements.ForEach(p => 
                    _functionScope.RemoveBinding(p.Token.Text));
        }

        public void AddBinding(string symbol, Expression expr)
        {
            _functionScope.AddBinding(symbol, expr);
        }
    }
}
