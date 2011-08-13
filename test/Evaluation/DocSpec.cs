using NUnit.Framework;
using Marosoft.Mist;
using Marosoft.Mist.Lexing;
using test.Evaluation.Common;
using Marosoft.Mist.Evaluation;

namespace test.Evaluation
{
    public class DocSpec : EvaluationTests
    {
        [Test]
        public void Add_doc_string_to_symbol_with_def()
        {
            Evaluate(@"
                       (def foo ""some doc"" 10)
                       ((fn () foo))                      ");

            result.Value.ShouldEqual(10);

            var doc = base.interpreter.Evaluate("(doc foo)");
            doc.Token.Type.ShouldEqual(Tokens.STRING);
            doc.Value.ShouldEqual("some doc");

        }
    }
}
