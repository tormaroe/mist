using NUnit.Framework;
using test.Evaluation.Common;

namespace test.Evaluation
{
    public class Arithmetics : EvaluationTests
    {
        [Test]
        public void Add_multiple_arguments()
        {
            Evaluate("(+ 1 2 3)");
            result.Value.ShouldEqual(6);
        }

        [Test]
        public void Add_single_argument()
        {
            Evaluate("(+ 1)");
            result.Value.ShouldEqual(1);
        }

        [Test]
        public void Add_including_negative()
        {
            Evaluate("(+ 1 -2)");
            result.Value.ShouldEqual(-1);
        }

        [Test]
        public void Subtract_single_argument()
        {
            Evaluate("(- 1)");
            result.Value.ShouldEqual(1);
        }

        [Test]
        public void Substract_multiple_arguments()
        {
            Evaluate("(- 1 2 3)");
            result.Value.ShouldEqual(-4);
        }

        [Test]
        public void Complex_add_and_subtract()
        {
            Evaluate("(+ (- 10 5) (- 12 1))");
            result.Value.ShouldEqual(5 + 11);
        }

        [Test]
        public void Multiply()
        {
            Evaluate("(* 2 2 3)");
            result.Value.ShouldEqual(2 * 2 * 3);
        }
    }
}