using NUnit.Framework;
using Marosoft.Mist;
using Marosoft.Mist.Lexing;
using test.Evaluation.Common;
using Marosoft.Mist.Evaluation;
using System.Text;

namespace test.Evaluation
{
    public class Functionoids : EvaluationTests
    {
        [Test]
        public void Call_Replace_on_a_string()
        {
            "abc".Replace("a", "b").ShouldEqual("bbc");

            Evaluate("(\"abc\" (quote Replace) \"a\" \"b\")");
            result.Value.ShouldEqual("bbc");
        }
    }
}
