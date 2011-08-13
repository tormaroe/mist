using Marosoft.Mist.Parsing;

namespace Marosoft.Mist.Evaluation
{
    public abstract class SpecialForm
    {
        public Environment Environment { get; set; }
        public abstract Expression Call(Expression expr);
    }
}
