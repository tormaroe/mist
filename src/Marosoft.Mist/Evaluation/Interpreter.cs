using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation
{
    public class Interpreter
    {
        private SpecialForms _specialForms;
        private GlobalScope _global = new GlobalScope();
        
        public Interpreter()
        {
            _specialForms = new SpecialForms(this);
            CurrentScope = _global;
        }

        public GlobalScope Global { get { return _global; } }
        public Scope CurrentScope { get; private set; }

        public Expression Evaluate(IEnumerable<Expression> expressions)
        {
            Expression result = null;
            foreach (var expr in expressions)
                result = Evaluate(expr);
            return result;
        }

        public Expression Evaluate(Expression expr)
        {
            switch (expr.Token.Type)
            {
                case Tokens.LIST: return List(expr); 
                case Tokens.INT: return expr;
                case Tokens.STRING: return expr;
                case Tokens.SYMBOL: return CurrentScope.Resolve(expr.Token.Text);
                default:
                    throw new Exception(string.Format("Token {0} can't be evaluate", expr.Token));
            }
        }

        private Expression List(Expression expr)
        {
            if (expr.Elements.First().Token.Type != Tokens.SYMBOL)
                throw new Exception(string.Format("(So far) can't evaluate {0} as function call", expr.Elements.First().Token));

            switch (expr.Elements.First().Token.Text)
            {
                case "if": return _specialForms.If(expr);
                case "def": return _specialForms.Def(expr);
                default:

                    // If first elem is a list, it must also be evaluated. 
                    // Should it then be evaluated before or after it's arguments?

                    var args = expr.Elements.Skip(1).Select(Evaluate);
                    var funk = GetFunction(expr.Elements.First());
                    return funk.Call(args);
            }
        }

        private Function GetFunction(Expression fExpr)
        {
            var f = _global.Resolve(fExpr.Token.Text);

            if (f is Function)
                return (Function)f;

            throw new Exception(fExpr.Token.Text + " is not a function");
        }

    }
}