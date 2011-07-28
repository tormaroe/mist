using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation
{
    public class Closure : BuiltInFunction
    {
        public Closure(Expression expr, Environment environment)
            : base("anonymous", environment.CurrentScope)
        {
            _environment = environment;
            _formalParameters = expr.Elements.Second();
            _functionScope = new BasicScope();

            CloseAroundExternalSymbols(_functionScope, expr.Elements.Third());

            if (!(_formalParameters is ListExpression))
                throw new MistException("First argument to fn must be a list, not " + _formalParameters);

            if (!_formalParameters.Elements.All(p => p.Token.Type == Tokens.SYMBOL))
                throw new MistException("Only symbols allowed in fn's list of formal paremeters. " + _formalParameters);

            Precondition = args => args.Count() == _formalParameters.Elements.Count;
            Implementation = args => environment.Evaluate(expr.Elements.Third());
        }

        private void CloseAroundExternalSymbols(BasicScope closure, Expression expr)
        {
            if (expr is ListExpression)
                expr.Elements.ForEach(e => CloseAroundExternalSymbols(closure, e));
            else
            {
                try
                {
                    if (expr.Token.Type == Tokens.SYMBOL
                        && !_formalParameters.Elements.Select(p => p.Token.Text).Contains(expr.Token.Text))
                    {
                        closure.AddBinding(expr.Token.Text, _environment.CurrentScope.Resolve(expr.Token.Text));
                    }
                }
                catch (SymbolResolveException)
                {
                    // might not be a problem... (other than performance (TODO))
                }
            }
        }

    }
}
