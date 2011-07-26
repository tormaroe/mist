using NUnit.Framework;
using Marosoft.Mist;

namespace test.Evaluation
{
    public class IfSpec : EvaluationTests
    {
        [Test]
        public void Make_sure_my_testing_strategy_works()
        {
            typeof(SymbolResolveException).ShouldBeThrownBy(() =>
                Evaluate(@"(if true 
                             (will_fail_for_sure) 
                             (will_fail_for_sure))"));
        }

        [Test]
        public void True_is_true()
        {
            Evaluate("(if true 1 (will_fail_for_sure))");
            result.Value.ShouldEqual(1);
        }

        [Test]
        public void False_is_false()
        {
            Evaluate("(if false (will_fail_for_sure) (+ 1 1))");
            result.Value.ShouldEqual(2);
        }

        [Test]
        public void No_else()
        {
            Evaluate("(if 29 29)");
            result.Value.ShouldEqual(29);


            Evaluate("(if false 29)");
            result.Value.ShouldBeNull();
        }

        [Test]
        public void NIL_is_false()
        {
            Evaluate("(if nil (will_fail_for_sure) (+ 1 1))");
            result.Value.ShouldEqual(2);
        }

        [Test]
        public void Most_symbols_etc_are_true()
        {
            Evaluate("(if + 0 1)");
            result.Value.ShouldEqual(0);
        }

        [Test]
        public void Nested_if()
        {
            Evaluate(@"(if (if nil 
                             true 
                             false)
                         10
                         20)");
            result.Value.ShouldEqual(20);
        }
    }
}
