
namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class GTFunction : LTGTFunctionBase
    {
        public GTFunction(Bindings scope)
            : base(">", scope)
        {
            Comparer = (a, b) => a > b;
        }
    }
}