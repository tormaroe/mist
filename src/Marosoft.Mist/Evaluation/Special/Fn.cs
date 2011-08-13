using Marosoft.Mist.Parsing;

namespace Marosoft.Mist.Evaluation.Special
{
    [SpecialForm("fn")]
    public class Fn : SpecialForm
    {
        public override Expression Call(Expression expr)
        {
            return new Closure(expr, Environment);
        }
    }
}