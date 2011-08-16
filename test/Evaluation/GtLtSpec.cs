using NUnit.Framework;
using Marosoft.Mist;
using Marosoft.Mist.Lexing;
using test.Evaluation.Common;
using Marosoft.Mist.Evaluation;
using Marosoft.Mist.Parsing;
using System.Linq;

namespace test.Evaluation
{
    public class GtLtSpec : EvaluationTests
    {
        [Test]
        public void gt_positive_2_args()
        {
            Evaluate("(> 2 1)");
            result.IsTrue.ShouldBeTrue();
        }

        [Test]
        public void gt_negative_2_args()
        {
            Evaluate("(> 1 1)");
            result.IsTrue.ShouldBeFalse();
        }

        [Test]
        public void gt_positive_n_args()
        {
            Evaluate("(> 2 1 0 -1)");
            result.IsTrue.ShouldBeTrue();
        }

        [Test]
        public void gt_negative_n_args()
        {
            Evaluate("(> 2 1 1 0)");
            result.IsTrue.ShouldBeFalse();
        }

        [Test]
        public void lt_positive_2_args()
        {
            Evaluate("(< 0 1)");
            result.IsTrue.ShouldBeTrue();
        }

        [Test]
        public void lt_negative_2_args()
        {
            Evaluate("(< 1 1)");
            result.IsTrue.ShouldBeFalse();
        }

        [Test]
        public void lt_positive_n_args()
        {
            Evaluate("(< 2 3 4 5)");
            result.IsTrue.ShouldBeTrue();
        }

        [Test]
        public void lt_negative_n_args()
        {
            Evaluate("(< 1 3 6 5)");
            result.IsTrue.ShouldBeFalse();
        }
    }
}
