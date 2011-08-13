using Marosoft.Mist.Parsing;

namespace Marosoft.Mist.Evaluation.Special
{
    [SpecialForm("doc")]
    public class Doc : SpecialForm
    {
        public override Expression Call(Expression expr)
        {
            var symbol = expr.Elements.Second();

            if (!(symbol is SymbolExpression))
                throw new MistException(string.Format("Special form doc takes a single argument, which must be a symbol. {0} is not a symbol.", expr));

            return Environment.Evaluate(symbol).DocString;
        }
    }
}
