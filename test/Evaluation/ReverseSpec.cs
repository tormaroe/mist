using NUnit.Framework;
using Marosoft.Mist;
using Marosoft.Mist.Lexing;
using test.Evaluation.Common;
using Marosoft.Mist.Evaluation;

namespace test.Evaluation
{
    public class ReverseSpec : EvaluationTests
    {
        [Test]
        public void Reverse()
        {
            Evaluate("(reverse (list 1 2 3 4))");
            result.ToString().ShouldEqual("(4 3 2 1)");
        }

        [Test]
        public void Dont_mutate_orginal()
        {
            Evaluate(@"

                (def foo (list 1 2 3 4))
                (def bar (reverse foo))
            ");
            interpreter.Evaluate("((fn () bar))").ToString().ShouldEqual("(4 3 2 1)");
            interpreter.Evaluate("((fn () foo))").ToString().ShouldEqual("(1 2 3 4)");
        }
    }
}
