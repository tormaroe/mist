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

        [Test, Ignore("Priorities changed, will get back to this")]
        public void Create_and_use_object_with_some_construction_parameters()
        {
            // First in C#
            var regex = new System.Text.RegularExpressions.Regex("123");
            System.Text.RegularExpressions.Match match = regex.Match("012345");
            match.Success.ShouldBeTrue();

            Evaluate("(new System.Text.RegularExpressions.Regex \"123\")");
            result.Value.ShouldBeOfType<System.Text.RegularExpressions.Regex>();
            //result.Value.ShouldBeOfType<StringBuilder>();
            
            //interpreter.EvaluateString("(foo (quote Append) \"Hello\")");

            //interpreter.EvaluateString("(foo (quote ToString))")
            //    .Value.ShouldEqual("Hello");
        }
    }
}
