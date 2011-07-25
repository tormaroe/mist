using System;
using System.Collections.Generic;
using Marosoft.Mist.Parsing;

namespace Marosoft.Mist.Evaluation
{
    public class BasicScope : Scope
    {
        private Dictionary<string, Expression> _symbolBindings = new Dictionary<string, Expression>();
        private readonly Scope _parentScope;

        public BasicScope(Scope parentScope)
        {
            _parentScope = parentScope;
        }

        public Expression Resolve(string symbol)
        {
            if (_symbolBindings.ContainsKey(symbol))
                return _symbolBindings[symbol];

            if (_parentScope != null)
                return _parentScope.Resolve(symbol);

            throw new Exception(string.Format("Unable to resolve symbol {0} in current scope", symbol));
        }

        public void AddBinding(Expression expr)
        {
            AddBinding(expr.Token.Text, expr);
        }

        public void AddBinding(string symbol, Expression expr)
        {
            _symbolBindings.Add(symbol, expr);
        }
    }
}
