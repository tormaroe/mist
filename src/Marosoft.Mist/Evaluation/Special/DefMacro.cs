using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation.Special
{
    [SpecialForm("defmacro")]
    public class DefMacro : SpecialForm
    {
        public override Expression Call(Expression expr)
        {
            if (expr.Elements.Count < 4)
                throw new MistException("defmacro must have at least 3 arguments (symbol, parameters list, and a body)");

            var symbol = expr.Elements.Second();

            if (symbol.Token.Type != Tokens.SYMBOL)
                throw new MistException(string.Format("The first argument to defmacro does not evaluate to a symbol ({0})", symbol.Token));

            var parameters = expr.Elements.Third() as ListExpression;

            if (parameters == null)
                throw new MistException("The second argument to defmacro must be a list");

            var name = symbol.Token.Text;

            var macro = new Macro(
                symbol: name, 
                formalParams: parameters, 
                body: expr.Elements.Skip(3),
                environment: Environment);

            Environment.CurrentScope.AddBinding(symbol, macro);
            
            return macro;
        }
    }
}
