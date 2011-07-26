using NUnit.Framework;

namespace test.Evaluation
{
    public class EqualSpec : EvaluationTests
    {
        [Test]
        public void Add_multiple_arguments()
        {
            Evaluate("(= 1 1)");
            result.IsTrue.ShouldBeTrue();
        }
    }
}
