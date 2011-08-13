using System;
using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Evaluation;
using System.IO;

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

            LoadUserConfig();
        }

        private static void LoadUserConfig()
        {
            //string userHomePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "..");

            string homePath = (System.Environment.OSVersion.Platform == PlatformID.Unix ||
                               System.Environment.OSVersion.Platform == PlatformID.MacOSX)
                ? System.Environment.GetEnvironmentVariable("HOME")
                : System.Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");

            string dotmistFile = Path.Combine(homePath, ".mist");

            if (File.Exists(dotmistFile))
            {
                Console.WriteLine("Loading user file from " + dotmistFile);
                try
                {
                    PRINT(EVAL("(load \"" + dotmistFile + "\")"));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("*** Loading user file got exception: {0}\nReview {1}\nMIST ENVIRONMENT LEFT IN AN UNKNOWN STATE!\n", 
                        ex.Message, dotmistFile);
                }
            }
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
