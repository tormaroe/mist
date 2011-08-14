using System.Linq;
using System.Collections.Generic;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;
using System;

namespace Marosoft.Mist.Evaluation
{
    public class Lambda : FunctionExpression, Function
    {
        private Environment _environment;
        protected Expression _formalParameters;

        public Predicate<IEnumerable<Expression>> Precondition { get; set; }
        public Func<IEnumerable<Expression>, Expression> Implementation { get; set; }

        public Lambda(Expression expr, Environment environment)
            : base("anonymous", environment.CurrentScope)
        {
            _environment = environment;
            _formalParameters = expr.Elements.Second();

            if (!(_formalParameters is ListExpression))
                throw new MistException("First argument to fn must be a list, not " + _formalParameters);

            if (!_formalParameters.Elements.All(p => p.Token.Type == Tokens.SYMBOL))
                throw new MistException("Only symbols allowed in fn's list of formal paremeters. " + _formalParameters);

            Precondition = args => args.Count() == _formalParameters.Elements.Count;
            Implementation = args =>
            {
                Expression result = null;
                foreach (var exp in expr.Elements.Skip(2))
                    result = environment.Evaluate(exp);
                return result;
            };
        }

        public Expression Call(IEnumerable<Expression> args)
        {
            var invocationScope = new Bindings { ParentScope = Scope };
            BindArguments(invocationScope, args);
            return _environment.WithScope(invocationScope, () => Implementation.Invoke(args));            
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
