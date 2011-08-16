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
            result.ShouldBeOfType<Lambda>();
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

        [Test]
        public void Recursion()
        {
            Evaluate(@" ; Multiply by recurively adding self

                        (def foo (fn (x times acc) 
                                   (if (= 0 times)
                                     acc
                                     (foo x
                                          (- times 1)
                                          (+ acc x)))))
                        (foo 2 10 0)
                    ");
            result.Value.ShouldEqual(20);
        }

        [Test]
        public void Closures()
        {
            Evaluate(@" ; y in formal parameters to closure should ""overwrite"" the outher y
                        ; x should be included in closure 

                        (def closure ((fn (x y)
                                         (fn (y) (+ x y))) 100 50))
                        (closure 2)
                    ");
            result.Value.ShouldEqual(102);
        }


        [Test]
        public void Return_lambda_from_function()
        {
            Evaluate(@" 
                        (defun make-adder (x)
                            (fn (y)
                                (+ x y)))

                        (def add-10 (make-adder 10))

                        (add-10 1) 
                    ");
            result.Value.ShouldEqual(11);
        }

        [Test]
        public void Ultimate_higher_order_test()
        {
            Evaluate(@"

                (defun make-fun ()
                    (fn () 1))

                (def the-fun (make-fun))

                (defun call-fun (fun)
                    (fun))

                (call-fun the-fun)

            ");
            result.Value.ShouldEqual(1);
        }

        [Test]
        public void Fn_can_have_multiple_expressions_in_body()
        {
            Evaluate(@"

                (def foo (fn ()
                    (list 1 2 3)
                    (list 4 5 6)))

                (foo)

            ");
            result.ToString().ShouldEqual("(4 5 6)");
        }
    }
}
