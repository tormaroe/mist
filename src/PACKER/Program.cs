using System;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.IO;

namespace Marosoft.Mist.Packer
{
    class Program
    {
        private const string MIST_DLL = "Marosoft.Mist.dll";

        static void Main(string[] args)
        {            
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
            parameters.ReferencedAssemblies.Add("System.Core.dll");
            parameters.ReferencedAssemblies.Add("Microsoft.CSharp.dll");
            //parameters.ReferencedAssemblies.Add(Program.MIST_DLL);
            parameters.EmbeddedResources.Add(Arguments.SourcePath);
            parameters.EmbeddedResources.Add(Program.MIST_DLL);

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
