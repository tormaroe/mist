using NUnit.Framework;
using Marosoft.Mist;
using Marosoft.Mist.Lexing;
using test.Evaluation.Common;
using Marosoft.Mist.Evaluation;

namespace test.Evaluation
{
    public class MapcatSpec : EvaluationTests
    {
        [Test, Ignore("Need APPLY first in order to implement in MIST itself")]
        public void Mapcat()
        {
            Evaluate("(mapcat reverse (list (list 3 2 1 0) (list 6 5 4) (list 9 8 7)))");
            result.ToString().ShouldEqual("(0 1 2 3 4 5 6 7 8 9)");
        }
    }
}
