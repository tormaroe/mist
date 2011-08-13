using Marosoft.Mist.Parsing;
using System.Linq;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation.Special
{
    [SpecialForm("if")]
    public class If : SpecialForm
    {
        public override Expression Call(Expression expr)
        {
            if (4 < expr.Elements.Count || expr.Elements.Count < 3)
                throw new MistException("Special form 'if' has 2 or 3 parameters, not " + (expr.Elements.Count - 1));

            var test = expr.Elements.Second();
            var then = expr.Elements.Third();
            var @else = expr.Elements.Count == 4 ? expr.Elements.Forth() : null;

            if (Environment.Evaluate(test).IsTrue)
                return Environment.Evaluate(then);
            if (@else != null)
                return Environment.Evaluate(@else);
            return Environment.CurrentScope.Resolve("nil");
        }
    }


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
