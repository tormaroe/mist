using Marosoft.Mist.Parsing;
using System.Linq;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation.Special
{
    [SpecialForm("cond")]
    public class Cond : SpecialForm
    {
        public override Expression Call(Expression expr)
        {
            var forms = expr.Elements.Skip(1).ToList();

            if (forms.Count() % 2 != 0)
                throw new MistException("COND requires an even number of forms");

            for (int i = 0; i < forms.Count() - 1; i = i + 2)
            {
                var condition = forms[i].Evaluate(Environment.CurrentScope);
                if (condition.IsTrue)
                    return forms[i + 1].Evaluate(Environment.CurrentScope);
            }

            return NIL.Instance;
        }
    }
}
