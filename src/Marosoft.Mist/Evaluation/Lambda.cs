using System.Linq;
using System.Collections.Generic;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;
using System;

namespace Marosoft.Mist.Evaluation
{
    // TODO: Macro and Lambda are almost the same - do I need two different classes?

    public class Lambda : ScopedExpression, Function
    {
        private Environment _environment;
        protected FormalParameters _formalParameters;

        public Predicate<IEnumerable<Expression>> Precondition { get; set; }
        public Func<IEnumerable<Expression>, Expression> Implementation { get; set; }

        public Lambda(Expression expr, Environment environment)
            : base("anonymous", environment.CurrentScope)
        {
            _environment = environment;
            _formalParameters = new FormalParameters(expr.Elements.Second());

            Precondition = args => args.Count() == _formalParameters.Count;
            Implementation = args => environment.Evaluate(expr.Elements.Skip(2));
        }

        public Expression Call(IEnumerable<Expression> args)
        {
            var invocationScope = _formalParameters.BindArguments(Scope, args);
            return _environment.WithScope(invocationScope, () => Implementation.Invoke(args));            
        }
    }
}
