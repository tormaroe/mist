using NUnit.Framework;
using Marosoft.Mist;
using test.Evaluation.Common;
using Marosoft.Mist.Parsing;
using System.Linq;
using Marosoft.Mist.Evaluation;

namespace test.Evaluation
{
    public class CondSpec : EvaluationTests
    {
        [Test]
        public void No_args()
        {
            Evaluate("(cond)");
            result.ShouldBeSameAs(NIL.Instance);
        }

        [Test]
        public void Match_condition()
        {
            Evaluate(@"

                (def x 100)

                (cond
                    (>= x 200) ""A""
                    (>= x 100) ""B""
                    (error) (error))
            ");
            result.Value.ShouldEqual("B");
        }

        [Test]
        public void No_condition_match()
        {
            Evaluate(@"

                (cond
                    (= 1 2) (error)
                    (= 3 4) (error)
                    false   (error)
                    nil     (error))
            ");
            result.ShouldBeSameAs(NIL.Instance);
        }
    }
}
