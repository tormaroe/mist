using NUnit.Framework;
using test.Evaluation.Common;

namespace test.Evaluation
{
    public class EqualSpec : EvaluationTests
    {
        [Test]
        public void Integers_that_are_equal()
        {
            Evaluate("(= 1 1)");
            result.IsTrue.ShouldBeTrue();

            Evaluate("(= 2 2 2 2 2)");
            result.IsTrue.ShouldBeTrue();
        }

        [Test]
        public void Integers_that_are_NOT_equal()
        {
            Evaluate("(= 1 2)");
            result.IsTrue.ShouldBeFalse();

            Evaluate("(= 2 2 2 2 0)");
            result.IsTrue.ShouldBeFalse();
        }

        [Test]
        public void Equal_strings()
        {
            Evaluate("(= \"foo\" \"foo\")");
            result.IsTrue.ShouldBeTrue();
        }

        [Test]
        public void Not_Equal_strings()
        {
            Evaluate("(= \"foo\" \"bar\")");
            result.IsTrue.ShouldBeFalse();
        }
    }
}
