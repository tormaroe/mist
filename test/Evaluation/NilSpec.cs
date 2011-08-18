using NUnit.Framework;
using Marosoft.Mist;
using test.Evaluation.Common;
using Marosoft.Mist.Parsing;
using System.Linq;

namespace test.Evaluation
{
    public class NilSpec : EvaluationTests
    {
        [Test, Ignore("Not here yet")]
        public void The_different_appearances_of_NIL()
        {
            Evaluate("(= 'nil nil '() () (list))");
            result.IsTrue.ShouldBeTrue();
        }

        [Test, Ignore("Not here yet")]
        public void The_different_appearances_of_NIL_are_all_FALSE()
        {
            Evaluate("(all false? (list 'nil nil '() () (list)))");
            result.IsTrue.ShouldBeTrue();
        }
    }
}
