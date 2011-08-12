using System.Collections.Generic;
using Marosoft.Mist.Parsing;

namespace Marosoft.Mist.Evaluation
{
    public class Bindings
    {
        private Dictionary<string, Expression> _symbolBindings = new Dictionary<string, Expression>();

        public Bindings ParentScope { get; set; }

        public virtual Expression Resolve(string symbol)
        {
            if (_symbolBindings.ContainsKey(symbol))
                return _symbolBindings[symbol];

            if (ParentScope != null)
                return ParentScope.Resolve(symbol);

            throw new SymbolResolveException(symbol);
        }

        public void AddBinding(Expression expr)
        {
            AddBinding(expr.Token.Text, expr);
        }

        public void AddBinding(string symbol, Expression expr)
        {
            _symbolBindings.Add(symbol, expr);
        }

        public void RemoveBinding(string symbol)
        {
            _symbolBindings.Remove(symbol);
        }
    }
}
