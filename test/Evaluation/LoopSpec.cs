using NUnit.Framework;
using Marosoft.Mist;
using Marosoft.Mist.Lexing;
using test.Evaluation.Common;
using Marosoft.Mist.Evaluation;

namespace test.Evaluation
{
    public class LoopSpec : EvaluationTests
    {
        [Test]
        public void Test()
        {
            Evaluate(@"(loop for i uptp 5 sum i)");
            result.Value.ShouldEqual(10);
        }
    }
}
