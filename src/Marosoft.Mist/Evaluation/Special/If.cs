using Marosoft.Mist.Parsing;

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
}
