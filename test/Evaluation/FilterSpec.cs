using NUnit.Framework;
using test.Evaluation.Common;
using Marosoft.Mist.Parsing;

namespace test.Evaluation
{
    public class FilterSpec : EvaluationTests
    {
        [Test]
        public void Test()
        {
            Evaluate("(filter zero? (list -1 0 1 0 2 3))");
            result.ShouldBeOfType<ListExpression>();
            result.ToString().ShouldEqual("(0 0)");
        }
    }
}
