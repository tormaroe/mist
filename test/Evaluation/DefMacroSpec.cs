using NUnit.Framework;
using Marosoft.Mist;
using Marosoft.Mist.Lexing;
using test.Evaluation.Common;
using Marosoft.Mist.Evaluation;
using Marosoft.Mist.Parsing;

namespace test.Evaluation
{
    public class DefMacroSpec : EvaluationTests
    {
        [Test]
        public void Simple_macro_no_arguments()
        {
            Evaluate(@"

                (defmacro xxx () 
                  (list (quote list) 1 2 3))

                (xxx)

            ");
            result.ShouldBeOfType<ListExpression>();
            result.ToString().ShouldEqual("(1 2 3)");
        }

        [Test]
        public void Simple_macro_with_arguments()
        {
            Evaluate(@"

                (defmacro xxx (x) 
                  (list (quote list) 1 x 3))

                (xxx 22)

            ");
            result.ShouldBeOfType<ListExpression>();
            result.ToString().ShouldEqual("(1 22 3)");
        }
    }
}
