using Marosoft.Mist.Parsing;
using System.Linq;

namespace Marosoft.Mist.Evaluation.Special
{
    [SpecialForm("quote")]
    public class Quote : SpecialForm
    {
        public override Expression Call(Expression expr)
        {
            return expr.Elements.Second();
        }
    }
}
