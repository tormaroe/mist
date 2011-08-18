using NUnit.Framework;
using Marosoft.Mist;
using test.Evaluation.Common;

namespace test.Evaluation
{
    public class SetSpec : EvaluationTests
    {
        [Test]        
        public void Set_in_global_scope()
        {
            Evaluate(@"

                (def foo 100)
                (set! foo (+ foo 100))

            ");
            interpreter.CurrentScope.Resolve("foo").Value.ShouldEqual(200);
        }

        [Test]        
        public void Set_should_return_new_value()
        {
            Evaluate(@"

                (def foo 100)
                (set! foo (+ foo 100))

            ");
            result.Value.ShouldEqual(200);
        }

        [Test]
        public void Set_in_subscope()
        {
            Evaluate(@"
		                (def foo ""booo"")

		                (defun change-it ()
		                    (set! foo ""changed""))

		                (change-it)
		            ");

            interpreter.CurrentScope.Resolve("foo").Value.ShouldEqual("changed");
        }

        [Test]
        public void Set_of_passed_in_var_should_not_work()
        {
            // AM I SURE THIS IS HOW IT SHOULD WORK???

            Evaluate(@"
		                (def foo ""booo"")

		                (defun change-it (x)
		                    (set! x ""changed""))

		                (change-it foo)
		            ");

            interpreter.CurrentScope.Resolve("foo").Value.ShouldEqual("booo");
        }
    }
}
