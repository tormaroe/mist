using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation
{
    public abstract class ScopedExpression : Expression
    {
        protected Bindings Scope;

        public ScopedExpression(string symbol, Bindings scope)
            : base(new Token(Tokens.FUNCTION, symbol))
        {
            Scope = new Bindings() { ParentScope = scope };
        }
    }
}
