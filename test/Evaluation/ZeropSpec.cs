using NUnit.Framework;
using test.Evaluation.Common;

namespace test.Evaluation
{
    public class ZeropSpec : EvaluationTests
    {
        [Test]
        public void PositiveTest()
        {
            Evaluate("(zero? 0)");
            result.IsTrue.ShouldBeTrue();
        }

        [Test]
        public void NegativeTest()
        {
            Evaluate("(zero? 1)");
            result.IsTrue.ShouldBeFalse();
        }
    }
}
