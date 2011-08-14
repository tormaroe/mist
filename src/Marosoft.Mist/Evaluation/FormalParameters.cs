using System.Linq;
using System.Collections.Generic;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation
{
    public class FormalParameters
    {
        private ListExpression _parameters;

        public FormalParameters(Expression expr)
        {
            if (!(expr is ListExpression))
                throw new MistException("Formal parameters must be a list, not " + expr);

            if (!expr.Elements.All(p => p.Token.Type == Tokens.SYMBOL))
                throw new MistException("Only symbols allowed in formal paremeters. " + expr);

            _parameters = (ListExpression)expr;
        }

        public int Count
        {
            get
            {
                return _parameters.Elements.Count;
            }
        }

        public Bindings BindArguments(Bindings scope, IEnumerable<Expression> args)
        {
            var invocationScope = new Bindings { ParentScope = scope };

            if (_parameters != null)
                for (int i = 0; i < Count; i++)
                    invocationScope.AddBinding(
                        _parameters.Elements[i].Token.Text,
                        args.ElementAt(i));

            return invocationScope;
        }
    }
}
