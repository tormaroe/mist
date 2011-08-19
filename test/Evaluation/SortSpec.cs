using NUnit.Framework;
using Marosoft.Mist;
using Marosoft.Mist.Lexing;
using test.Evaluation.Common;
using Marosoft.Mist.Evaluation;

namespace test.Evaluation
{
    public class SortSpec : EvaluationTests
    {

        [Test]
        public void Sort_ints()
        {
            Evaluate("(reverse (sort (list 3 1 2 0 -1 1)))");
            result.ToString().ShouldEqual("(3 2 1 1 0 -1)");
        }

        [Test]
        public void Sort_by_ints()
        {
            Evaluate("(sort-by (fn (x) x) (list 3 2 0 -1 1))");
            result.ToString().ShouldEqual("(-1 0 1 2 3)");
        }

        [Test]
        public void Sort_by_strings()
        {
            Evaluate("(sort-by (fn (x) x) (list \"foo\" \"bar\" \"zot\"))");
            result.ToString().ShouldEqual("(\"bar\" \"foo\" \"zot\")");
        }

        [Test]
        public void Order_by_alias()
        {
            Evaluate("(order-by identity (list \"foo\" \"bar\" \"zot\"))");
            result.ToString().ShouldEqual("(\"bar\" \"foo\" \"zot\")");
        }
    }
}
