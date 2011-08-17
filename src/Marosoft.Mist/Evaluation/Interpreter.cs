using System;
using System.Collections.Generic;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation
{
    public class Interpreter : Environment
    {        
        private Stack<Bindings> _scopeStack = new Stack<Bindings>();     
        
        private Lazy<Parser> _parser = new Lazy<Parser>(() 
            => new Parser(new Lexer(Tokens.All)));        
        
        public Interpreter()
        {
            _scopeStack.Push(new GlobalScope());
            SpecialForms.Load(this);
            MistCore.Load(this);
        }

        public Bindings CurrentScope { get { return _scopeStack.Peek(); } }

        public Expression EvaluateString(string code)
        {
            return _parser.Value.Parse(code).Evaluate(CurrentScope);
        }

        public T WithScope<T>(Bindings s, Func<T> call)
        {
            _scopeStack.Push(s);
            try
            {
                return call.Invoke();
            }
            finally
            {
                _scopeStack.Pop();
            }
        }
    }
}