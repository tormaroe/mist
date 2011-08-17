using Marosoft.Mist.Parsing;

namespace Marosoft.Mist.Evaluation
{
    /// <summary>
    /// A special form is a form (or function) that uses
    /// special evaluation rules. The special forms are 
    /// the most basic operations in the language, which
    /// many other abstractions are based on.
    /// </summary>
    public abstract class SpecialForm
    {
        public Environment Environment { get; set; }
        public abstract Expression Call(Expression expr);

        /// <summary>
        /// Delegates evaluation to expression itself, using current scope
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public Expression Evaluate(Expression expr)
        {
            return expr.Evaluate(Environment.CurrentScope);
        }
    }
}
