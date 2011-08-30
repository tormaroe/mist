using NUnit.Framework;
using Marosoft.Mist;
using Marosoft.Mist.Lexing;
using test.Evaluation.Common;
using Marosoft.Mist.Evaluation;

namespace test.Evaluation
{
    public class StaticMethods : EvaluationTests
    {
        [Test, Ignore("Not there yet...")]
        public void StringFormat()
        {
            Evaluate("(System.String Format \"{0}, {1}\" \"hello\" \"world\")");
            result.Value.ShouldEqual("hello, world"); // WORK IN PROGRESS!!
        }
    }
}