using NUnit.Framework;
using Marosoft.Mist;
using Marosoft.Mist.Lexing;
using test.Evaluation.Common;
using Marosoft.Mist.Evaluation;

namespace test.Evaluation
{
    public class ApplySpec : EvaluationTests
    {
        [Test]
        public void Test()
        {
            Evaluate("(apply + (list 1 2))");
            result.Value.ShouldEqual(3);
        }

        [Test]
        public void Prove_that_funcall_is_not_needed_in_mist()
        {
            Evaluate(@"(defun foo (x) (x 1 2))
                       (defun call (x y) (x y))
                       (call foo +)");
            result.Value.ShouldEqual(3);

            Evaluate(@"(let (x + 
                             y x) 
                          (y 1 2))");
            result.Value.ShouldEqual(3);
        }

        #region CORE FUNCTIONS BASED ON APPLY

        [Test]
        public void Sum()
        {
            Evaluate("(sum (list 1 2 3))");
            result.Value.ShouldEqual(6);
        }

        #endregion
    }
}
