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
        private GlobalScope _global = new GlobalScope();
        private Scope _currentScope;

        public Interpreter()
        {
            _currentScope = _global;
        }

        public GlobalScope Global { get { return _global; } }

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
                case Tokens.INT: return Int(expr);
                case Tokens.SYMBOL: return _currentScope.Resolve(expr.Token.Text);
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
                case "if": return If_SpecialForm(expr);
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

        private Expression If_SpecialForm(Expression expr)
        {
            if (4 < expr.Elements.Count || expr.Elements.Count < 3)
                throw new MistException("Special form 'if' has 2 or 3 parameters, not " + (expr.Elements.Count - 1));

            var test = expr.Elements.Second();
            var then = expr.Elements.Third();
            var @else = expr.Elements.Count == 4 ? expr.Elements.Forth() : null;

            if (Evaluate(test).IsTrue)
                return Evaluate(then);
            if (@else != null)
                return Evaluate(@else);
            return _currentScope.Resolve("nil");
        }

        private Expression Int(Expression expr)
        {
            return expr;
        }
    }
}