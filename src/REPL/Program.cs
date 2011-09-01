﻿using System;
using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Evaluation;
using System.Collections.Generic;

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
       A LISP IMPLEMENTATION FOR .NET
      http://tormaroe.github.com/mist/
          Type :h <ENTER> for help!

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

        #region READ
        private static string READ()
        {
            Console.Write("=> ");
            return ReadExpression();
        }

        private static string ReadExpression(string acc = "")
        {
            acc = string.Format("{0} {1}", acc, Console.ReadLine());
            return ExpressionDone(acc) ? acc : ReadExpression(acc);
        }

        private static bool ExpressionDone(string input)
        {
            if (IsForm(input))
            {
                var characters = input.ToCharArray();
                return characters.Count(ch => ch == '(') <= characters.Count(ch => ch == ')');
            }
            return true;
        }
        private static bool IsForm(string input)
        {
            return input.Trim().StartsWith("(");
        }
        #endregion


        private static Dictionary<string, Func<string>> ReplCommands
            = new Dictionary<string, Func<string>>
            {
                { ":q", () => "(quit)" },
                { ":r", () => "(restart)" },
                { ":h", () => 
                    {
                        Console.WriteLine(@"
REPL HELP
------------------------------------------------------------
:h                 Prints this message
:q or (quit)       Quits REPL
:r or (restart)    Start REPL from scratch
(load ""filepath"")  Evaluate the content of a file
*m*                Contains the value of the last evaluation
*m2*               The value of the next to last evaluation
*m3*               The value of the third last evaluation
");
                        return "(identity nil)";
                    } 
                },
            };

        public static Expression EVAL(string input)
        {
            input = input.Trim();

            if (!IsForm(input))
            {
                if (ReplCommands.ContainsKey(input))
                    input = ReplCommands[input].Invoke();

                else if (!input.Contains(' '))
                    input = string.Format("(identity {0})", input);
            }

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
