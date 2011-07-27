using NUnit.Framework;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;
using Marosoft.Mist.Evaluation;

namespace test.Evaluation.Common
{
    [TestFixture]
    public class EvaluationTests
    {
        protected Expression result;
        protected Interpreter interpreter;

        protected void Evaluate(string source)
        {
            var expressions = new Parser(new Lexer(Tokens.All)).Parse(source);
            interpreter = new Interpreter();
            result = interpreter.Evaluate(expressions);
        }
    }
}
