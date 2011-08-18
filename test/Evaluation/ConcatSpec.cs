using NUnit.Framework;
using Marosoft.Mist;
using Marosoft.Mist.Lexing;
using test.Evaluation.Common;
using Marosoft.Mist.Evaluation;

namespace test.Evaluation
{
    public class ConcatSpec : EvaluationTests
    {
        [Test]
        public void Concat_2_lists()
        {
            Evaluate("(concat (list 1 2 3) (list 3 2 1))");
            result.ToString().ShouldEqual("(1 2 3 3 2 1)");
        }

        [Test]
        public void Concat_many_lists()
        {
            Evaluate("(concat (list 1 2 3) (list 3 2 1) (list 0) (list -1 -2))");
            result.ToString().ShouldEqual("(1 2 3 3 2 1 0 -1 -2)");
        }

        [Test]
        public void Concat_with_empty_list()
        {
            Evaluate("(concat (list 1 2 3) (list) (list 0) (list -1 -2))");
            result.ToString().ShouldEqual("(1 2 3 0 -1 -2)");
        }

        [Test]
        public void Concat_with_nil()
        {
            Evaluate("(concat nil (list 0) (list -1 -2))");
            result.ToString().ShouldEqual("(0 -1 -2)");
        }

        [Test]
        public void Concat_with_non_list_arguments() // Not common in lisps..
        {
            Evaluate("(concat nil 1 2 (list 3 4) 5)");
            result.ToString().ShouldEqual("(1 2 3 4 5)");
        }
    }
}