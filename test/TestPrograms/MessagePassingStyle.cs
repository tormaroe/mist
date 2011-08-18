using NUnit.Framework;
using Marosoft.Mist;
using test.Evaluation.Common;
using Marosoft.Mist.Evaluation;

namespace test.TestPrograms
{
    public class MessagePassingStyle : EvaluationTests
    {
        /*
         * TODO
         * - when defun can take multiple-expression-body, change make-account to use defun
         */

        [Test]
        public void Run()
        {
            Evaluate(@"

                (def make-account (fn (balance)
                    (defun withdraw (amount)
                        (if (>= balance amount)
                            (set! balance (- balance amount))
                            ""Insufficient funds""))
                    (defun deposit (amount)
                        (set! balance (+ balance amount)))
                    (defun dispatch (m)
                        (cond
                            (= m :withdraw) withdraw
                            (= m :deposit) deposit
                            :else (error ""Unknown request -- MAKE-ACCOUNT"" m)))
                    dispatch))

                (defun withdraw (account amount)
                    ((account :withdraw) amount))

                (defun deposit (account amount)
                    ((account :deposit) amount))

                (def acc-1 (make-account 100))
                (def acc-2 (make-account 100))

                (withdraw acc-1 50) ; acc-1 should now have 50,-
                (deposit acc-2 50)  ; acc-2 should now have 150,-
            ");

            interpreter.EvaluateString("(withdraw acc-1 50)").Value.ShouldEqual(0);
            interpreter.EvaluateString("(withdraw acc-1 50)").Value.ShouldEqual("Insufficient funds");

            interpreter.EvaluateString("(deposit acc-2 50)").Value.ShouldEqual(200);

            typeof(MistApplicationException).ShouldBeThrownBy(
                () => interpreter.EvaluateString("((acc-1 :unknown-keyword))"),
                ex => ex.Message == "Unknown request -- MAKE-ACCOUNT :unknown-keyword");
        }
    }
}
