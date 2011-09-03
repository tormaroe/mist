using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
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

    class Program
    {
        private const string MIST_DLL = "Marosoft.Mist.dll";

        static void Main(string[] args)
        {
            // Arguments: mistprogramfile, outname, switches: embed-dll
            //
            // Create bootstrapper code that instantiates an EmbeddedMist object
            // and loads a file from an embedded resource
            //
            // Compile outname.exe from bootstrapper code
            // with added resource file: mistprogramfile
            //
            // Link in Mist dll
            //var sourceFile = "..\\..\\..\\..\\samples\\hello-world.mist";

            ValidatePresenceOfMist();
            
            Arguments.Load(args);

            CompilerResults results = Compile();

            PrintResult(results);

            Environment.Exit(results.Errors.Count > 0 ? 2 : 0);
        }

        private static void ValidatePresenceOfMist()
        {
            if (!File.Exists(MIST_DLL))
                throw new Exception("can't find " + MIST_DLL +
                    ". Make sure it's located in the same folder as you're running mistpacker from.");
        }

        private static CompilerResults Compile()
        {
            var codeProvider = new CSharpCodeProvider();
            var parameters = new CompilerParameters()
            {
                GenerateExecutable = true,
                OutputAssembly = Arguments.ExeName,
            };
            parameters.ReferencedAssemblies.Add(Program.MIST_DLL);
            parameters.EmbeddedResources.Add(Arguments.SourcePath);

            return codeProvider.CompileAssemblyFromSource(
                parameters,
                Bootstrapper.CreateSource());
        }

        private static void PrintResult(CompilerResults results)
        {
            if (results.Errors.Count > 0)
            {
                foreach (CompilerError CompErr in results.Errors)
                {
                    Console.WriteLine(
                        "Line number " + CompErr.Line +
                        ", Error Number: " + CompErr.ErrorNumber +
                        ", '" + CompErr.ErrorText + ";" +
                        Environment.NewLine);
                }
            }
            else
            {
                Console.WriteLine("Source {0} built into {1} successfully.",
                    "sourceFile", results.PathToAssembly);
            }
        }
    }
}
