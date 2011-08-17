using NUnit.Framework;
using test.Evaluation.Common;
using Marosoft.Mist.Parsing;

namespace test.Evaluation
{
    public class ReduceSpec : EvaluationTests
    {
        [Test]
        public void Simple_reduce()
        {
            Evaluate("(reduce + (list 1 2 3))");
            result.ShouldBeOfType<IntExpression>();
            result.Value.ShouldEqual(6);
        }

        [Test]
        public void Simple_reduce_with_seed_value()
        {
            Evaluate("(reduce + 10 (list 1 2 3))");
            result.ShouldBeOfType<IntExpression>();
            result.Value.ShouldEqual(16);
        }

        [Test]
        public void Using_lambda__and_alias()
        {
            Evaluate("(aggregate (fn (acc x) (- acc x)) 10 (list 1 2 2))");
            result.ShouldBeOfType<IntExpression>();
            result.Value.ShouldEqual(5);
        }

        [Test]
        public void Dont_mutate_orginal_when_using_seed()
        {
            Evaluate(@"

                (def foo (list 1 2 2))
                (def bar (aggregate (fn (acc x) (- acc x)) 10 foo))
            ");
            interpreter.EvaluateString("((fn () bar))").ToString().ShouldEqual("5");
            interpreter.EvaluateString("((fn () foo))").ToString().ShouldEqual("(1 2 2)");
        }
    }
}
