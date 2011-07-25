using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;
using Marosoft.Mist.Evaluation;

namespace test.Evaluation
{
    public class Arithmetics : EvaluationTests
    {
        [Test]
        public void Add()
        {
            Evaluate("(+ 1 1)");
            result.Token.Type.ShouldEqual(Tokens.INT);
            result.Token.Text.ShouldEqual("2");
        }
    }
}
