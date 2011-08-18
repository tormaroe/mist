using NUnit.Framework;
using Marosoft.Mist;
using test.Evaluation.Common;

namespace test.Evaluation
{
    public class LetSpec : EvaluationTests
    {
        [Test]
        public void Simple()
        {
            Evaluate(@"

                (let (x 2)
                    (+ x x))
            ");
            result.Value.ShouldEqual(4);
        }

        [Test]
        public void Use_previously_defined_binding()
        {
            Evaluate(@"

                (let (x 2 
                      y (inc x))
                    (+ y x))
            ");
            result.Value.ShouldEqual(5);
        }

        [Test]
        public void Use_binding_from_outer_scopes()
        {
            Evaluate(@"

                (def a 10)

                (let (x a)
                    (let (y (inc x)) 
                        (+ y x)))
            ");
            result.Value.ShouldEqual(21);
        }

        [Test]
        public void Global_binding_must_not_change()
        {
            Evaluate(@"

                (def x 100)

                (let (x 2)
                    (+ x x))
            ");
            result.Value.ShouldEqual(4);
            interpreter.EvaluateString("(identity x)")
                .Value.ShouldEqual(100);
        }

        [Test]
        public void Lexical_scope__NOT_dynamic_scope()
        {
            Evaluate(@"

                (def x 111)

                (defun check-x () x)

                (let (x 222)
                    (check-x))
            ");
            result.Value.ShouldEqual(111);            
        }
    }
}
