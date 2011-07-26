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
        private readonly Interpreter _environment;

        public SpecialForms(Interpreter environment)
        {
            _environment = environment;
        }

        public Expression If(Expression expr)
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

        public Expression Def(Expression expr)
        {
            if (expr.Elements.Count != 3)
                throw new MistException("Special form 'def' needs 2 parameters, not " + (expr.Elements.Count - 1));

            var symbol = expr.Elements.Second();

            if (symbol.Token.Type != Tokens.SYMBOL)
                throw new MistException(string.Format("The first argument to def does not evaluate to a symbol ({0})", symbol.Token));

            var value = _environment.Evaluate(expr.Elements.Third());

            _environment.CurrentScope.AddBinding(symbol.Token.Text, value);
            return symbol;
        }
    }
}
