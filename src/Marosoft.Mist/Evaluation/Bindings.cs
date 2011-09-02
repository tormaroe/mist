using System.Collections.Generic;
using Marosoft.Mist.Parsing;
using System;
using System.Linq;

namespace Marosoft.Mist.Evaluation
{
    public class Bindings
    {
        private Dictionary<Expression, Expression> _symbolBindings = new Dictionary<Expression, Expression>();

        public Bindings ParentScope { get; set; }

        public virtual Expression Resolve(string symbol)
        {
            var v = _symbolBindings.FirstOrDefault(kv => kv.Key.Token.Text == symbol).Value;
            if(v != null)  
               return v;

            if (ParentScope != null)
                return ParentScope.Resolve(symbol);

            throw new SymbolResolveException(symbol);
        }

        public Function GetFunction(string symbol)
        {
            return (Function)Resolve(symbol);
        }

        public IEnumerable<Expression> AllBindings
        {
            get
            {
                //TODO: enhance when Bindings stores symbols instead of strings!!!
                // Not working as I hoped... :(
                var bindings = _symbolBindings.Keys;
                if(ParentScope != null)
                    return bindings.Concat(ParentScope.AllBindings);
                return bindings;
            }
        }

        public void AddBinding(Expression expr)
        {
            //TODO: should probably store the symbol here as well
            // will make it possible to retrieve the doc string from the symbol instead of the value
            // see AllBindings as well

            AddBinding(new SymbolExpression(expr.Token.Text), expr);
        }


        public void AddBinding(Expression symbol, Expression expr)
        {
            _symbolBindings.Add(symbol, expr);
        }

        public void RemoveBinding(string symbol)
        {
            _symbolBindings.Remove(_symbolBindings.First(kv => kv.Key.Token.Text == symbol).Key);
        }

        public void UpdateBinding(Expression symbol, Expression value)
        {
            string lookupKey = symbol.Token.Text;

            var k = _symbolBindings.FirstOrDefault(kv => kv.Key.Token.Text == lookupKey).Key;
            if (k != null)
                _symbolBindings[k] = value;

            else if (ParentScope != null)
                ParentScope.UpdateBinding(symbol, value);

            else 
                throw new SymbolResolveException(lookupKey);
        }
    }
}
