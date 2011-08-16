using NUnit.Framework;
using Marosoft.Mist;
using test.Evaluation.Common;

namespace test.Evaluation
{
    public class LoadSpec : EvaluationTests
    {
        [Test]
        public void Test()
        {
            // See the embedded file for details..
            Evaluate("(load \"Evaluation\\load_test.mist\")");

            // The result of the script..
            result.Value
                .ShouldEqual(665);

            // xxx, defined in global scope, should still be there..
            interpreter.CurrentScope.Resolve("xxx").Value
                .ShouldEqual(666);

            // f, the function, should have a doc string..
            interpreter.CurrentScope.Resolve("f").DocString.Value
                .ShouldEqual("A function");

            // to-subtract, defined inside f, should now be out of scope..
            typeof(SymbolResolveException).ShouldBeThrownBy(() =>
                interpreter.CurrentScope.Resolve("to-subtract"));
        }
    }
}
