using NUnit.Framework;
using Marosoft.Mist;
using Marosoft.Mist.Lexing;
using test.Evaluation.Common;
using Marosoft.Mist.Evaluation;
using Marosoft.Mist.Parsing;
using System.Linq;

namespace test.Evaluation
{
    public class FirstAndRestSpec : EvaluationTests
    {
        [Test]
        public void First()
        {
            Evaluate("(first (list 100 200 300))");
            result.Value.ShouldEqual(100);
        }

        [Test]
        public void First_of_empty_list_is_nil()
        {
            Evaluate("(first (list))");
            result.ShouldBeSameAs(NIL.Instance);
        }

        [Test]
        public void Rest()
        {
            Evaluate("(rest (list 100 200 300))");
            var list = result as ListExpression;
            list.Elements.Count.ShouldEqual(2);
            list.Elements.First().Value.ShouldEqual(200);
            list.Elements.Second().Value.ShouldEqual(300);
        }

        [Test]
        public void Rest_of_one_element_list()
        {
            Evaluate("(rest (list 100))");
            var list = result as ListExpression;
            list.Elements.Count.ShouldEqual(0);
        }

        [Test]
        public void Rest_of_empty_list()
        {
            Evaluate("(rest (rest (list 100)))");
            var list = result as ListExpression;
            list.Elements.Count.ShouldEqual(0);
        }
    }
}
