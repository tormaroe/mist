using System;
using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Evaluation;
using System.IO;
using System.Collections.Generic;

namespace Marosoft.Mist.Repl
{
    class Bootstrapper
    {
        public static void Initialize()
        {
            var interpreter = new Interpreter();
            var global = interpreter.CurrentScope;
            var memory = new ResultMemory(global);

            global.AddBinding(new Restart(global));
            global.AddBinding(new Quit(global));

            Program.SetState(interpreter, memory);

            LoadUserConfig();
        }

        public class Restart : BuiltInFunction
        {
            public Restart(Bindings scope) : base("restart", scope) { }

            protected override bool Precondition(System.Collections.Generic.IEnumerable<Expression> args)
            {
                return args.Count() == 0;
            }

            protected override Expression InternalCall(System.Collections.Generic.IEnumerable<Expression> args)
            {
                Console.WriteLine("RESTARTING ENVIRONMENT");
                Initialize();
                return null;
            }
        }

        public class Quit : BuiltInFunction
        {
            public Quit(Bindings scope) : base("quit", scope) { }

            protected override bool Precondition(IEnumerable<Expression> args)
            {
                return args.Count() == 0;
            }

            protected override Expression InternalCall(IEnumerable<Expression> args)
            {
                Console.WriteLine("BYE BYE!");
                System.Environment.Exit(0);
                return null;
            }
        }

        private static void LoadUserConfig()
        {
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
                    Program.PRINT(Program.EVAL("(load \"" + dotmistFile + "\")"));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("*** Loading user file got exception: {0}\nReview {1}\nMIST ENVIRONMENT LEFT IN AN UNKNOWN STATE!\n",
                        ex.Message, dotmistFile);
                }
            }
        }
    }
}
