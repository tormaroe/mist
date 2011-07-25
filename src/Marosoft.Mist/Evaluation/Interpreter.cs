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
        private BuiltInFunctions _functions = new BuiltInFunctions();

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
                default:
                    throw new Exception(string.Format("Token {0} can't be evaluate", expr.Token));
            }
        }

        private Expression List(Expression expr)
        {
            if (expr.Elements.First().Token.Type != Tokens.SYMBOL)
                throw new Exception(string.Format("(So far) can't evaluate {0} as function call", expr.Elements.First().Token));

            // If first elem is a list, it must also be evaluated. 
            // Should it then be evaluated before or after it's arguments?

            var args = expr.Elements.Skip(1).Select(Evaluate);
            var funk = GetFunction(expr.Elements.First());
            return Call(funk, args);
        }

        private Func<IEnumerable<Expression>, Expression> GetFunction(Expression fExpr)
        {
            if (_functions.Contains(fExpr.Token.Text))
                return _functions[fExpr.Token.Text];

            throw new Exception("unknown function " + fExpr.Token.Text);
        }

        private Expression Call(Func<IEnumerable<Expression>, Expression> funk, IEnumerable<Expression> args)
        {
            return funk.Invoke(args);
        }

        private Expression Int(Expression expr)
        {
            return expr;
        }
    }
}