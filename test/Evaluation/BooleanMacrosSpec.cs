using NUnit.Framework;
using Marosoft.Mist;
using Marosoft.Mist.Lexing;
using test.Evaluation.Common;
using Marosoft.Mist.Evaluation;

namespace test.Evaluation
{
    public class BooleanMacrosSpec : EvaluationTests
    {
        // TODO: expand tests when variable count arguments available

        [Test]
        public void AND__positive_test()
        {
            Evaluate("(and true true)");
            result.IsTrue.ShouldBeTrue();
        }
        [Test]
        public void AND__negative_test_1()
        {
            Evaluate("(and true false)");
            result.IsTrue.ShouldBeFalse();
        }
        [Test]
        public void AND__negative_test_2()
        {
            Evaluate("(and false false)");
            result.IsTrue.ShouldBeFalse();
        }
    }
}
