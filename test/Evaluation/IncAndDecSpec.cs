using NUnit.Framework;
using test.Evaluation.Common;

namespace test.Evaluation
{
    public class IncAndDecSpec : EvaluationTests
    {
        [Test]
        public void Inc()
        {
            Evaluate("(inc 3)");
            result.Value.ShouldEqual(4);
        }
        [Test]
        public void Dec()
        {
            Evaluate("(dec 3)");
            result.Value.ShouldEqual(2);
        }
    }
}
