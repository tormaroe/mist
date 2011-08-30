using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;
using System.Linq;

namespace Marosoft.Mist.Evaluation.Special
{
    [SpecialForm("def")]
    public class Def : SpecialForm
    {
        public override Expression Call(Expression expr)
        {
            var symbol = expr.Elements.Second();

            if (symbol.Token.Type != Tokens.SYMBOL)
                throw new MistException(string.Format("The first argument to def does not evaluate to a symbol ({0})", symbol.Token));

            Expression value = null;

            if (expr.Elements.Count == 3)
            {
                value = Evaluate(expr.Elements.Third());
            }
            else if (expr.Elements.Count == 4 && expr.Elements.Third().Token.Type == Tokens.STRING)
            {
                value = Evaluate(expr.Elements.Forth());
                value.DocString = Evaluate(expr.Elements.Third()); // as StringExpression;
            }
            else
                throw new MistException("Special form 'def' needs 2 parameters (+ an optional doc string), not " + (expr.Elements.Count - 1));

            Environment.CurrentScope.AddBinding(symbol.Token.Text, value);
            return symbol;
        }
    }
}