using NUnit.Framework;
using Marosoft.Mist;
using test.Evaluation.Common;
using Marosoft.Mist.Parsing;
using System.Linq;
using Marosoft.Mist.Evaluation;

namespace test.Evaluation
{
    public class QuoteSpec : EvaluationTests
    {
        [Test]
        public void Quote_a_list()
        {
            Evaluate("(quote (1 2 3))");
            var list = result as ListExpression;
            list.Elements.Count.ShouldEqual(3);
            list.Elements.First().Value.ShouldEqual(1);
            list.Elements.Second().Value.ShouldEqual(2);
            list.Elements.Third().Value.ShouldEqual(3);
        }

        [Test]
        public void Quote_a_number()
        {
            Evaluate("(quote 1)");
            result.Value.ShouldEqual(1);
        }

        [Test]
        public void Quote_a_symbol()
        {
            Evaluate("(quote xxx)");
            result.ShouldBeOfType<SymbolExpression>();
            result.Token.Text.ShouldEqual("xxx");
        }
    }
}
