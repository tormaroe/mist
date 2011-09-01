using System;
using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Evaluation;

namespace Marosoft.Mist.Repl
{
    class Program
    {
        const string HEADER = @"
       _____  .___  ____________________
      /     \ |   |/   _____/\__    ___/
     /  \ /  \|   |\_____  \   |    |   
    /    Y    \   |/        \  |    |   
    \____|__  /___/_______  /  |____|   
            \/            \/            
   A LISP IMPLEMENTATION BY TORBJORN MARO
      https://github.com/tormaroe/mist
          Evaluate (quit) to exit!

";

        private static Interpreter interpreter;
        private static Bindings global;
        private static ResultMemory memory;

        public static void SetState(Interpreter i, Bindings b, ResultMemory r)
        {
            interpreter = i;
            global = b;
            memory = r;
        }

        static void Main(string[] args)
        {
            Console.Write(HEADER);
            Bootstrapper.Initialize();

            while (true)
                try
                {
                    PRINT(EVAL(READ()));
                }
                catch (MistException mex)
                {
                    Console.WriteLine("*** " + mex.GetType().Name + ":\n" + mex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
        }

        private static string READ()
        {
            Console.Write("=> ");
            return ReadExpression();
        }

        private static string ReadExpression(string acc = "")
        {
            acc += Console.ReadLine();
            return ExpressionDone(acc) ? acc : ReadExpression(acc);
        }

        private static bool ExpressionDone(string input)
        {
            if (input.Trim().StartsWith("("))
            {
                var characters = input.ToCharArray();
                return characters.Count(ch => ch == '(') <= characters.Count(ch => ch == ')');
            }
            return true;
        }

        public static Expression EVAL(string input)
        {
            var expr = interpreter.EvaluateString(input);
            memory.UpdateReplMemory(expr);
            return expr;
        }

        public static void PRINT(Expression exp)
        {
            Console.WriteLine(exp);
        }
    }
}
