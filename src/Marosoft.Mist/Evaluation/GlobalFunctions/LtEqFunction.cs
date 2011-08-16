namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class LtEqFunction : LTGTFunctionBase
    {
        public LtEqFunction(Bindings scope)
            : base("<=", scope)
        {
            Comparer = (a, b) => a <= b;
        }
    }
}
