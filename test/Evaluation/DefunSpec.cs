using NUnit.Framework;
using Marosoft.Mist;
using Marosoft.Mist.Lexing;
using test.Evaluation.Common;
using Marosoft.Mist.Evaluation;
using Marosoft.Mist.Parsing;

namespace test.Evaluation
{
    public class DefunSpec : EvaluationTests
    {
        [Test]
        public void Test()
        {
            Evaluate(@"

                        (defun foo (x) 
                            (list x))

                        (foo 10)

            ");

            result.ShouldBeOfType<ListExpression>();
        }
    }
}
