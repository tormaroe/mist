using System;
using System.Linq;
using System.IO;

namespace Marosoft.Mist.Packer
{
    class Arguments
    {
        //TODO: enable option to NOT include Mist DLL in exe
        //TODO: multiple source files

        private const string HELP = @"
Usage: mistpacker options
Options include:
	-o:filename      Name of executable output file (optional)
	-s:sourcefile    Path to file containing Mist source to pack
		";

        private static string[] _args;
        public static void Load(string[] args)
        {
            _args = args;

            try
            {
                var sourceArg = _args.SingleOrDefault(a => a.StartsWith("-s:"));
                if (sourceArg != null)
                    SourcePath = sourceArg.Substring(3);
                else
                    throw new Exception("-s option missing - you need to specify a source file!");

                if (!File.Exists(SourcePath))
                    throw new Exception("can't find sourcefile!");

                var exeNameArg = _args.SingleOrDefault(a => a.StartsWith("-o:"));
                if (exeNameArg != null)
                    ExeName = exeNameArg.Substring(3);
                else
                    ExeName = "mistprogram.exe";

                if (!ExeName.ToLower().EndsWith(".exe"))
                    throw new Exception("Output file name must have a .exe extension!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR PARSING ARGUMENTS!");
                Console.WriteLine(ex.Message);
                Console.WriteLine(HELP);
                Environment.Exit(1);
            }
        }

        public static string SourcePath { get; set; }
        public static string ExeName { get; set; }
    }
}
