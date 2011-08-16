using NUnit.Framework;
using Marosoft.Mist;
using Marosoft.Mist.Lexing;
using test.Evaluation.Common;
using Marosoft.Mist.Evaluation;

namespace test.Evaluation
{
    public class IdentitySpec : EvaluationTests
    {
        [Test]
        public void Test()
        {
            Evaluate("(identity \"hello\")");
            result.Value.ShouldEqual("hello");
        }
    }
}
