using NUnit.Framework;
using Marosoft.Mist;
using Marosoft.Mist.Lexing;
using test.Evaluation.Common;
using Marosoft.Mist.Evaluation;
using Marosoft.Mist.Parsing;
using System.Linq;

namespace test.Evaluation
{
    public class ListSpec : EvaluationTests
    {
        [Test]
        public void Create_list()
        {
            Evaluate("(list 1 2 3)");
            var list = result as ListExpression;
            list.Elements.Count.ShouldEqual(3);
            list.Elements.First().Value.ShouldEqual(1);
            list.Elements.Second().Value.ShouldEqual(2);
            list.Elements.Third().Value.ShouldEqual(3);
        }
    }
}
