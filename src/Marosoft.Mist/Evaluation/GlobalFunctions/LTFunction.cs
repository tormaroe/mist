
namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class LTFunction : LTGTFunctionBase
    {
        public LTFunction(Bindings scope)
            : base("<", scope)
        {
            Comparer = (a, b) => a < b;
        }
    }
}
