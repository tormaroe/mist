using NUnit.Framework;
using Marosoft.Mist;
using Marosoft.Mist.Lexing;
using test.Evaluation.Common;
using Marosoft.Mist.Evaluation;

namespace test.Evaluation
{
    public class ZipSpec : EvaluationTests
    {
        [Test]
        public void Zip()
        {
            Evaluate("(zip + (list 1 2 3) (list 1 2 3))");
            result.ToString().ShouldEqual("(2 4 6)");
        }

        [Test]
        public void Zip_with_lambda()
        {
            Evaluate("(zip (fn (x y) (+ (* x x) y)) (list 3 2 1) (list 1 2 3 1 1 1 1 1 1))");
            result.ToString().ShouldEqual("(10 6 4)");
        }
    }
}
