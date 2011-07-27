using NUnit.Framework;
using Marosoft.Mist;
using Marosoft.Mist.Lexing;
using test.Evaluation.Common;
using Marosoft.Mist.Evaluation;

namespace test.Evaluation
{
    public class DefSpec : EvaluationTests
    {
        [Test]
        public void Def_evaluates_to_the_bound_symbol()
        {
            Evaluate("(def x 10)");
            result.Token.Text.ShouldEqual("x");
            result.Token.Type.ShouldEqual(Tokens.SYMBOL);
        }

        [Test]
        public void Def_variable_should_exist_in_environment()
        {
            Evaluate(@"
		                (def x 10)
		                (if x x (throw-some-error))");
            result.Value.ShouldEqual(10);
        }

        // TODO: test first argument as complex expression ??
    }
}