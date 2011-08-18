using NUnit.Framework;
using test.Evaluation.Common;
using Marosoft.Mist.Parsing;

namespace test.Evaluation
{
    public class KeywordSpec : EvaluationTests
    {
        [Test]
        public void KeywordsEvaluateToThemselves()
        {
            Evaluate("(identity :foo)");
            result.ShouldBeOfType<SymbolExpression>();
            result.ToString().ShouldEqual(":foo");
        }

        [Test]
        public void Same_keywords_are_equal()
        {
            Evaluate("(= :foo :foo)");
            result.IsTrue.ShouldBeTrue();
        }

        [Test]
        public void Different_keywords_are_not_equal()
        {
            Evaluate("(= :foo :bar)");
            result.IsTrue.ShouldBeFalse();
        }
    }
}
