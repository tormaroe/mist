using Marosoft.Mist.Parsing;

namespace Marosoft.Mist.Evaluation
{
    public interface Scope
    {
        Expression Resolve(string symbol);
    }
}
