using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;
using System.Linq;

namespace Marosoft.Mist.Evaluation.Special
{
    [SpecialForm("let")]
    public class Let : SpecialForm
    {
        public override Expression Call(Expression expr)
        {
            var bindings = expr.Elements.Second().Elements;
            var tempScope = new Bindings() { ParentScope = Environment.CurrentScope };

            for (int i = 0; i < bindings.Count - 1; i = i + 2)
                tempScope.AddBinding(
                    bindings[i].Token.Text,
                    bindings[i + 1].Evaluate(tempScope));

            return Environment.WithScope(tempScope,
                () => expr.Elements.Skip(2).Evaluate(tempScope));
        }
    }
}
