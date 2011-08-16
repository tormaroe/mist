using NUnit.Framework;
using Marosoft.Mist;
using test.Evaluation.Common;

namespace test.TestPrograms
{
    public class MessagePassingStyle : EvaluationTests
    {
        /*
         * TODO
         * - swap if in dispatch with cond (with a third else that throws error)
         * - swap use of quote with reader macro quote
         * - or swap it with keywords
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
                        (if (= m (quote withdraw))
                            withdraw
                            (if (= m (quote deposit))
                                deposit
                                (error ""Unknown request -- MAKE-ACCOUNT"" m))))
                    dispatch))

                (defun withdraw (account amount)
                    ((account (quote withdraw)) amount))

                (defun deposit (account amount)
                    ((account (quote deposit)) amount))

                (def acc-1 (make-account 100))
                (def acc-2 (make-account 100))

                (withdraw acc-1 50) ; acc-1 should now have 50,-
                (deposit acc-2 50)  ; acc-2 should now have 150,-
            ");

            interpreter.Evaluate("(withdraw acc-1 50)").Value.ShouldEqual("0");
            interpreter.Evaluate("(deposit acc-2 50)").Value.ShouldEqual("200");
        }
    }
}
