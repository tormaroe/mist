using NUnit.Framework;
using Marosoft.Mist;
using Marosoft.Mist.Lexing;
using test.Evaluation.Common;
using Marosoft.Mist.Evaluation;
using Marosoft.Mist.Parsing;
using System.Linq;

namespace test.Evaluation
{
    public class LoadSpec : EvaluationTests
    {
        [Test]
        public void Test()
        {
            Evaluate("(load \"Evaluation\\load_test.mist\")");
            result.Value.ShouldEqual(665);
            interpreter.CurrentScope.Resolve("xxx").Value.ShouldEqual(666);
            interpreter.CurrentScope.Resolve("f").DocString.Value.ShouldEqual("A function");
        }
    }
}
