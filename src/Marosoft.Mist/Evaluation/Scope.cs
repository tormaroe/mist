using Marosoft.Mist.Parsing;

namespace Marosoft.Mist.Evaluation
{
    public interface Scope
    {
        Scope ParentScope { get; set; }
        void AddBinding(string symbol, Expression expr);
        Expression Resolve(string symbol);
    }
}
