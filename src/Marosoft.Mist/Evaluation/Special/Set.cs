using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation.Special
{
    [SpecialForm("set!")]
    public class Set : SpecialForm
    {
        public override Expression Call(Expression expr)
        {
            var symbol = expr.Elements.Second();

            if (symbol.Token.Type != Tokens.SYMBOL)
                throw new MistException(string.Format("The first argument to set! does not evaluate to a symbol ({0})", symbol.Token));

            Expression value = Environment.Evaluate(expr.Elements.Third());

            Environment.CurrentScope.UpdateBinding(symbol, value);

            return Environment.CurrentScope.Resolve("nil");
        }
    }
}
