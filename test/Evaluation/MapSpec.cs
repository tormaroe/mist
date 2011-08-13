using NUnit.Framework;
using Marosoft.Mist;
using test.Evaluation.Common;
using Marosoft.Mist.Parsing;
using System.Linq;

namespace test.Evaluation
{
    public class MapSpec : EvaluationTests
    {
        [Test]
        public void Test()
        {
            Evaluate(@"

                (map (fn (x) 
                        (+ 1 x)) 
                     (list 1 2 3))

            ");

            var list = result as ListExpression;
            list.Elements.Count.ShouldEqual(3);
            list.Elements.First().Value.ShouldEqual(2);
            list.Elements.Second().Value.ShouldEqual(3);
            list.Elements.Third().Value.ShouldEqual(4);
        }

        [Test]
        public void Test2()
        {
            Evaluate(@"

                (def double (fn (x) (+ x x)))
                (map double (list 1 2 3))

            ");

        }

        [Test]
        public void Map_With_built_in_function_pointer()
        {
            Evaluate(@"

                (def a ""doc a"" 1)
                (def b ""doc b"" 2)
                (def c ""doc c"" 3)

                (map doc (list a b c))

            ");

            var list = result as ListExpression;
            list.Elements.Count.ShouldEqual(3);
            list.Elements.First().Value.ShouldEqual("doc a");
            list.Elements.Second().Value.ShouldEqual("doc b");
            list.Elements.Third().Value.ShouldEqual("doc c");
        }

        [Test]
        public void Select_should_be_an_alias() // since Select is the name used in Linq
        {
            Evaluate(@"

                (select (fn (x) 
                          (+ 1 x)) 
                        (list 1 2 3))

            ");

            var list = result as ListExpression;
            list.Elements.Count.ShouldEqual(3);
            list.Elements.First().Value.ShouldEqual(2);
            list.Elements.Second().Value.ShouldEqual(3);
            list.Elements.Third().Value.ShouldEqual(4);
        }
    }
}
