using NUnit.Framework;
using test.Evaluation.Common;

namespace test.Evaluation
{
    public class LoopSpec : EvaluationTests
    {
        // TODO (not complete):        
        // infinite loops.. (test with thread?)
        
        //> (loop for i on '(1 2 3) do (print i))
        //(1 2 3)
        //(2 3)
        //(3)

        // FOR WHEN I IMPLEMENT THE DICTIOONARY SUPPORT
        //> (setf h (make-hash-table))
        //> (setf (gethash 'a h) 1)
        //> (setf (gethash 'b h) 2)
        //> (loop for k being the hash-key of h do (print k))
        //b
        //a
        //> (loop for v being the hash-value of h do (print v))
        //2
        //1
        //> (loop for k being the hash-key using (hash-value v) of h do (format t "~a ~a~%" k v))
        //b 2
        //a 1

        // investigate use of BY some more...
        // (loop for i from 1 to 10 by 2 do (print i))

        // (loop for i from 3 downto 1 do (print i))

        // ?? - not working in clisp...
        // (loop for i from 3 above 1 do (print i))

        // !!!!
        // (loop with a = '(1 2 3) for i in a do (print i))

        // !!!!
        // (loop for i from 1 to 3 for x = (* i i) do (print x))

        // (loop for i from 1 to 3 append (list i i))
        // (1 1 2 2 3 3)

        // THIS SHOULD ALREADY WORK, BUT MAKE SURE
        // (loop for i from 1 to 3 count (oddp i))
        // 2

        // > (loop for i from 1 to 3 maximize (mod i 3))
        // 2
        // > (loop for i from 1 to 3 minimize (mod i 3))
        // 0

        // ALREADY WORKS??
        //(loop
        //  for item in list
        //  for i from 1 to 10
        //  do (something))
        // will iterate at most ten times but may stop sooner if list contains fewer than ten items.

        // Destructuring Variables
        //(loop for (a b) in '((1 2) (3 4) (5 6))
        //      do (format t "a: ~a; b: ~a~%" a b))


        //(loop for i in *random*
        //   counting (evenp i) into evens
        //   counting (oddp i) into odds
        //   summing i into total
        //   maximizing i into max
        //   minimizing i into min
        //   finally (return (list min max total evens odds)))

        // initially (setupp code)
        // finally (teardown code)
        // After the initially or finally, these clauses consist of all the Lisp 
        // forms up to the start of the next loop clause or the end of the loop. 
        // All the initially forms are combined into a single prologue, which runs once, 
        // immediately after all the local loop variables are initialized and before the 
        // body of the loop. The finally forms are similarly combined into a epilogue to 
        // be run after the last iteration of the loop body. Both the prologue and epilogue 
        // code can refer to local loop variables.

        // The prologue is always run, even if the loop body iterates zero times. 
        // The loop can return without running the epilogue if any of the following happens:
        // A return clause executes, or The loop is terminated by an always, never, or thereis 
        // clause, as I'll discuss in the next section.

        // if same as when ?!

        // if, when and unless can also take an else

        /*
         (loop for i from 1 to 100
              if (evenp i)
                minimize i into min-even and 
                maximize i into max-even and
                unless (zerop (mod i 4))
                  sum i into even-not-fours-total
                end
                and sum i into even-total
              else
                minimize i into min-odd and
                maximize i into max-odd and
                when (zerop (mod i 5)) 
                  sum i into fives-total
                end
                and sum i into odd-total
              do (update-analysis min-even
                                  max-even
                                  min-odd
                                  max-odd
                                  even-total
                                  odd-total
                                  fives-total
                                  even-not-fours-total))
         */

        //(if (loop for n in numbers always (evenp n))
            // (print "All numbers even."))
        // (if (loop for n in numbers never (oddp n))
            // (print "All numbers even."))

        // (loop for char across "abc123" thereis (digit-char-p char)) ==> 1

        // (loop for char across "abcdef" thereis (digit-char-p char)) ==> NIL

        /*;; The "return" action both stops the loop and returns a result.
            ;; Here we return the first numeric character in the string s.

            (let ((s "alpha45"))
              (loop for i from 0 below (length s)
	            for ch =  (char s i)
	            when (find ch "0123456789" :test #'eql)
	            return ch) )
            #\4
         */

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
