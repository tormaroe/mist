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
          Evaluate (quit) to exit!

";

        private static Parser parser;
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
            parser = new Parser(new Lexer(Tokens.All));
            interpreter = new Interpreter();

            interpreter.Global.AddBinding(new Function("restart", interpreter.Global)
            {
                Precondition = arguments => arguments.Count() == 0,
                Implementation = arguments =>
                {
                    Console.WriteLine("RESTARTING ENVIRONMENT");
                    InitializeInterpreter();
                    return null;
                }
            });

            interpreter.Global.AddBinding(new Function("quit", interpreter.Global)
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
            var expressions = parser.Parse(input);
            return interpreter.Evaluate(expressions);
        }

        private static void PRINT(Expression exp)
        {
            Console.WriteLine(exp);
        }
    }
}
