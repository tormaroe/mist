using System.Collections.Generic;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation
{
    // TODO: Macro and Lambda are almost the same - do I need two different classes?

    public class Macro : Expression
    {
        private readonly Environment _environment;
        private readonly IEnumerable<Expression> _body;
        private readonly FormalParameters _formalParams;
        private readonly Bindings _scope;

        public Macro(
            string symbol,
            ListExpression formalParams,
            IEnumerable<Expression> body,
            Environment environment)
            : base(new Token(Tokens.MACRO, symbol))
        {
            _formalParams = new FormalParameters(formalParams);
            _body = body;
            _environment = environment;
            _scope = _environment.CurrentScope;
        }

        public override Expression Evaluate(Bindings scope)
        {
            throw new MistException("You can't evaluate a macro (" + Token.Text + ")");
        }

        public Expression ExpandAndEvaluate(IEnumerable<Expression> args, Bindings scope)
        {
            return Expand(args).Evaluate(scope);
        }

        public Expression Expand(IEnumerable<Expression> args)
        {
            var invocationScope = _formalParams.BindArguments(_scope, args);
            return _environment.WithScope(invocationScope, () => _body.Evaluate(_environment.CurrentScope));
        }
    }
}
