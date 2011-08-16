namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class GtEqFunction : LTGTFunctionBase
    {
        public GtEqFunction(Bindings scope)
            : base(">=", scope)
        {
            Comparer = (a, b) => a >= b;
        }
    }
}
