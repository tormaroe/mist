using NUnit.Framework;
using Marosoft.Mist;
using Marosoft.Mist.Lexing;
using test.Evaluation.Common;
using Marosoft.Mist.Evaluation;

namespace test.Evaluation
{
    public class FnSpec : EvaluationTests
    {
        [Test]
        public void Fn_should_produce_function()
        {
            Evaluate("(fn () \"hello, world\")");
            result.ShouldBeOfType<Function>();
        }

        [Test]
        public void Bind_fn_to_symbol_and_call()
        {
            Evaluate("(def x (fn () \"hello, world\")) (x)");
            result.Value.ShouldEqual("hello, world");
        }

        [Test]
        public void Call_anonymous_fn_directly()
        {
            Evaluate("((fn () (+ 1 2 3)))");
            result.Value.ShouldEqual(6);
        }

        [Test]
        public void Fn_taking_arguments()
        {
            Evaluate("((fn (a b) (- a b)) 10 2)");
            result.Value.ShouldEqual(8);
        }

        [Test]
        public void Curried_function()
        {
            Evaluate(@"
                         ((fn (a) 
                            ((fn (b) 
                                (- a b)) 
                               2)) 
                             12)
                    ");
            result.Value.ShouldEqual(10);
        }
    }
}
