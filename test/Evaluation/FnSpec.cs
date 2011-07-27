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

        [Test, Ignore("TODO")]
        public void Closures()
        {
            // See Recursion test first!!!

            Evaluate(@"
                        (def clojure ((fn (x)
                                         (fn () x)) 100))
                        (clojure)
                    ");
            result.Value.ShouldEqual(100);
        }

        [Test, Ignore("TODO")]
        public void Recursion()
        {
            // Need to improve function scope,
            // a new scope needed on each invocation, not on function creation,
            // although function creation scope probably also needed for closures
            // Do this BEFORE trying to solve closures!

            Evaluate(@" ; Multiply by recurively adding self

                        (def foo (fn (x times acc) 
                                   (if (= 0 times)
                                     x
                                     (foo x
                                          (- times 1)
                                          (+ acc x)))))
                        (foo 2 10 0)
                    ");
            result.Value.ShouldEqual(20);
        }
    }
}
