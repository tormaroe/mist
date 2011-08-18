using NUnit.Framework;
using test.Evaluation.Common;
using Marosoft.Mist.Parsing;

namespace test.Evaluation
{
    public class ListAsMutableStackSpec : EvaluationTests
    {
        [Test, Ignore("Waiting for working set!")]
        public void KeywordsEvaluateToThemselves()
        {
            Evaluate("(def a (list 4))");

            interpreter.EvaluateString("(push! 5 a)").ToString().ShouldEqual("(4 5)");
            interpreter.EvaluateString("(push! 6 a)").ToString().ShouldEqual("(4 5 6)");

            // push! implemented in core.mist, but set! not working properly..
            // OR MAYBE that _is_ how set! should work, and I need a different way to implement push! and pop!

            // Then pop! 
            // Pop! empty list should return nil
        }
    }
}
