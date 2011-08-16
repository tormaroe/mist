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
            //TODO: should probably store the symbol here as well
            // will make it possible to retrieve the doc string from the symbol instead of the value

            AddBinding(expr.Token.Text, expr);
        }

        public void AddBinding(string symbol, Expression expr)
        {
            // Above TODO will make this method problematic

            _symbolBindings.Add(symbol, expr);
        }

        public void RemoveBinding(string symbol)
        {
            _symbolBindings.Remove(symbol);
        }

        internal void UpdateBinding(Expression symbol, Expression value)
        {
            string key = symbol.Token.Text;

            if (_symbolBindings.ContainsKey(key))
                _symbolBindings[key] = value;

            else if (ParentScope != null)
                ParentScope.UpdateBinding(symbol, value);

            else 
                throw new SymbolResolveException(key);
        }
    }
}
