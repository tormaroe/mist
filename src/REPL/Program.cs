using System;
using System.Linq;
using Marosoft.Mist.Lexing;
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

        static void Main(string[] args)
        {
            Console.Write(HEADER);
            InitializeInterpreter();

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

        private static void InitializeInterpreter()
        {
            interpreter = new Interpreter();
            var global = ((Bindings)interpreter.CurrentScope);

            global.AddBinding(new BuiltInFunction("restart", global)
            {
                Precondition = arguments => arguments.Count() == 0,
                Implementation = arguments =>
                {
                    Console.WriteLine("RESTARTING ENVIRONMENT");
                    InitializeInterpreter();
                    return null;
                }
            });

            global.AddBinding(new BuiltInFunction("quit", global)
            {
                Precondition = arguments => arguments.Count() == 0,
                Implementation = arguments =>
                {
                    Console.WriteLine("BYE BYE!");
                    System.Environment.Exit(0);
                    return null;
                }
            });
        }

        private static string READ()
        {
            Console.Write("=> ");
            return Console.ReadLine();
        }

        private static Expression EVAL(string input)
        {
            return interpreter.Evaluate(input);
        }

        private static void PRINT(Expression exp)
        {
            Console.WriteLine(exp);
        }
    }
}
