using Marosoft.Mist.Parsing;

namespace Marosoft.Mist.Evaluation
{
    public interface Scope
    {
        void AddBinding(string symbol, Expression expr);
        Expression Resolve(string symbol);
    }
}
