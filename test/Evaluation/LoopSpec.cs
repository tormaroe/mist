using NUnit.Framework;
using test.Evaluation.Common;

namespace test.Evaluation
{
    public class LoopSpec : EvaluationTests
    {
        // TODO (not complete):        
        // infinite loops.. (test with thread?)
        
        [Test]
        public void For_i_upto_5_sum_i()
        {
            Evaluate(@"(loop for i upto 5 sum i)");
            result.Value.ShouldEqual(15);
        }

        [Test]
        public void For_i_below_5_sum_i()
        {
            Evaluate(@"(loop for i below 5 sum i)");
            result.Value.ShouldEqual(10);
        }

        [Test]
        public void For_i_upto_5_count_i()
        {
            Evaluate(@"(loop for i upto 5 count i)");
            result.Value.ShouldEqual(6);
        }

        [Test]
        public void For_x_in_list_sum_x()
        {
            Evaluate(@"(loop for x in (list 1 2 3) sum x)");
            result.Value.ShouldEqual(6);
        }

        [Test]
        public void For_x_in_list_count_x__Do_not_count_nil()
        {
            Evaluate(@"(loop for x in (list 1 nil 2 nil 3) count x)");
            result.Value.ShouldEqual(3);
        }

        [Test]
        public void For_i_until_xxx()
        {
            Evaluate(@"(loop for i until (> i 10) count i)");
            result.Value.ShouldEqual(11);
        }

        [Test]
        public void For_i_while_xxx()
        {
            Evaluate(@"(loop for i while (< i 10) count i)");
            result.Value.ShouldEqual(10);
        }

        [Test]
        public void For_i_from_2_to_4()
        {
            Evaluate(@"(loop for i from 2 to 4 sum i)");
            result.Value.ShouldEqual(9);
        }

        [Test]
        public void Sum_expression_must_be_evaluated()
        {
            Evaluate(@"(loop for i below 3 sum (inc i))");
            result.Value.ShouldEqual(1 + 2 + 3);
        }

        [Test]
        public void Collect_a_range()
        {
            Evaluate(@"(loop for i from 2 to 4 collect (+ 2 i))");
            result.ToString().ShouldEqual("(4 5 6)");
        }

        [Test]
        public void When()
        {
            Evaluate(@"
                    (loop for n from 1 to 10
                        when (= 5 n)
                            collect n)");
            result.ToString().ShouldEqual("(5)");
        }

        [Test]
        public void Do_until()
        {
            Evaluate(@"
                    (def i 0) 
                    (loop do (set! i (inc i)) 
                          until (> i 5))
                    (identity i)");
            result.Value.ShouldEqual(6);
        }

        [Test]
        public void Repeat__and_do_twice()
        {
            Evaluate(@"
                    (def i 0) 
                    (loop repeat 5
                          do (set! i (inc i))
                          do (set! i (inc i)))
                    (identity i)");
            result.Value.ShouldEqual(10);
        }

        [Test]
        public void Loop_on_two_lists()
        {
            Evaluate(@"
                    (loop for a in (list 1 2 3 4)
                          for b in (list 4 5 6)
                          collect (+ a b))");
            result.ToString().ShouldEqual("(5 7 9)");
        }
    }
}
