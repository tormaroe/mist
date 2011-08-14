using Marosoft.Mist.Parsing;
using System.Linq;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation.Special
{
    [SpecialForm("load")]
    public class Load : SpecialForm
    {
        public override Expression Call(Expression expr)
        {
            if (expr.Elements.Count != 2 || expr.Elements.Second().Token.Type != Tokens.STRING)
                throw new MistException("Load takes a single string argument.");

            var source = Environment.Evaluate("(slurp " + expr.Elements.Second().ToString() + ")");
            return Environment.Evaluate(source.Value.ToString());
        }
    }
}
