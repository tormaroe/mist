using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace Marosoft.Mist.Repl
{
    static class Arguments
    {
        const string HELP = @"
Usage: mist [switches] [programfile]

--continue        -c    Continue in the REPL after running programfile
--help            -h    Display command line help and exit
--no-user-config  -u    Don't load users .mist file
--no-header       -n    Don't display the startup header

Examples:
    mist
    mist --no-user-config
    mist -nu somefolder\\somefile.mist";

        private static void DisplayHelpAndExit()
        {
            Console.WriteLine(HELP);
            System.Environment.Exit(1);
        }

        private static Dictionary<char, string> argsMap = new Dictionary<char, string>
        {
            {'n', "--no-header"},
            {'u', "--no-user-config"},
            {'h', "--help"},
            {'c', "--continue"},
        };

        private static IEnumerable<string> _args;
        
        public static void Load(string[] args)
        {
            _args = args;
            ValidateAndStoreProgramfileArg();
            ExpandMiniSwitches();
            
            if (DisplayHelp)
                DisplayHelpAndExit();
        }

        private static void ValidateAndStoreProgramfileArg()
        {
            if (_args.Count() > 0)
            {
                if (_args.Any(a => !a.StartsWith("-")))
                {
                    if (_args.Reverse().Skip(1).Any(a => !a.StartsWith("-")))
                    {
                        Error("Invalid arguments, unable to parse!");
                    }
                    else if (!File.Exists(_args.Last()))
                    {
                        Error(_args.Last() + " can't be found!");
                    }
                    else
                    {
                        ProgramfileSpesified = true;
                        Programfile = _args.Last();
                    }
                }
            }
        }

        private static void ExpandMiniSwitches()
        {
            try
            {
                _args = _args
                    .Where(a => a.Length > 1 && a[0] == '-' && a[1] != '-')
                    .SelectMany(a => a.Substring(1).ToCharArray().Select(c => argsMap[c]))
                    .Concat(_args)
                    .ToList();
            }
            catch
            {
                Error("Some invalid argument was given, unable to continue!");
            }
        }

        private static void Error(string message)
        {
            ConsoleExtensions.WithColors(
                ConsoleColor.White, 
                ConsoleColor.Red,
                () => Console.WriteLine(message));
            DisplayHelpAndExit();
        }

        public static bool DisplayHeader { get { return !Contains('n'); } }
        public static bool UseUserConfig { get { return !Contains('u'); } }
        public static bool DisplayHelp { get { return Contains('h'); } }
        public static bool ContinueAfterProgramfile { get { return Contains('c'); } }
        public static bool ProgramfileSpesified { get; private set; }
        public static string Programfile { get; private set; }

        public static bool ShouldStartRepl
        {
            get
            {
                return !ProgramfileSpesified || ContinueAfterProgramfile;
            }
        }

        private static bool Contains(char c)
        {
            return _args.Any(a => a == argsMap[c]);
        }
    }
}