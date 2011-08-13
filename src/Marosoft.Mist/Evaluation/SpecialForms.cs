using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation
{
    public class SpecialForms
    {
        private readonly Environment _environment;
        private Dictionary<string, Func<Expression, Expression>> _formsMap;

        public SpecialForms(Environment environment)
        {
            _environment = environment;
            
            _formsMap = new Dictionary<string, Func<Expression, Expression>> 
            {
                {"if", If},
                {"def", Def},
                {"fn", Fn},
                {"doc", Doc}
            };
        }

        public bool IsSpecialForm(string name)
        {
            return _formsMap.ContainsKey(name);
        }

        public Expression CallSpecialForm(Expression expr)
        {
            return _formsMap[expr.Elements.First().Token.Text](expr);
        }

        private Expression Fn(Expression expr)
        {
            return new Closure(expr, _environment);
        }

        private Expression If(Expression expr)
        {
            if (4 < expr.Elements.Count || expr.Elements.Count < 3)
                throw new MistException("Special form 'if' has 2 or 3 parameters, not " + (expr.Elements.Count - 1));

            var test = expr.Elements.Second();
            var then = expr.Elements.Third();
            var @else = expr.Elements.Count == 4 ? expr.Elements.Forth() : null;

            if (_environment.Evaluate(test).IsTrue)
                return _environment.Evaluate(then);
            if (@else != null)
                return _environment.Evaluate(@else);
            return _environment.CurrentScope.Resolve("nil");
        }

        private Expression Def(Expression expr)
        {            
            var symbol = expr.Elements.Second();

            if (symbol.Token.Type != Tokens.SYMBOL)
                throw new MistException(string.Format("The first argument to def does not evaluate to a symbol ({0})", symbol.Token));

            Expression value = null;

            if (expr.Elements.Count == 3)
            {
                value = _environment.Evaluate(expr.Elements.Third());
            }
            else if (expr.Elements.Count == 4 && expr.Elements.Third().Token.Type == Tokens.STRING)
            {
                value = _environment.Evaluate(expr.Elements.Forth());
                value.DocString = _environment.Evaluate(expr.Elements.Third()) as StringExpression;
            }
            else
                throw new MistException("Special form 'def' needs 2 parameters, not " + (expr.Elements.Count - 1));

            
            _environment.CurrentScope.AddBinding(symbol.Token.Text, value);
            return symbol;
        }

        private Expression Doc(Expression expr)
        {
            var symbol = expr.Elements.Second();

            if (!(symbol is SymbolExpression))
                throw new MistException(string.Format("Special form doc takes a single argument, which must be a symbol. {0} is not a symbol.", expr));

            return _environment.Evaluate(symbol).DocString;
        }
    }
}
