using NUnit.Framework;
using Marosoft.Mist;
using Marosoft.Mist.Lexing;
using test.Evaluation.Common;
using Marosoft.Mist.Evaluation;
using System.Text;

namespace test.Evaluation
{
    public class NewSpec : EvaluationTests
    {
        [Test]
        public void Create_and_use_object_with_no_construction_parameters()
        {
            Evaluate("(def foo (new System.Text.StringBuilder)) (identity foo)");
            result.Value.ShouldBeOfType<StringBuilder>();

            interpreter.EvaluateString("(foo (quote Append) \"Hello\")");

            interpreter.EvaluateString("(foo (quote ToString))")
                .Value.ShouldEqual("Hello");
        }
    }
}
