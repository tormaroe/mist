using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.IO;

namespace Marosoft.Mist.Packer
{
    class Bootstrapper
    {
        const string TEMPLATE = @"
using System;
using Marosoft.Mist;
using System.IO;
using System.Reflection;
namespace Marosoft.Mist.StandaloneApp {
    class Program { 
        static void Main(string[] args) {
            var mist = new EmbeddedMist();

            var ass = Assembly.GetExecutingAssembly();
            var mistResource = ass.GetManifestResourceNames()[0];
            var mistStream = ass.GetManifestResourceStream(mistResource);
            using (var streamReader = new StreamReader(mistStream))
            {
                var source = streamReader.ReadToEnd();
                mist.Evaluate(source);
            }

        } 
    } 
}
";
 
        public static string CreateSource()
        {
            return TEMPLATE;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Arguments: mistprogramfile, outname
            //
            // Create bootstrapper code that instantiates an EmbeddedMist object
            // and loads a file from an embedded resource
            //
            // Compile outname.exe from bootstrapper code
            // with added resource file: mistprogramfile
            //
            // Link in Mist dll

            var sourceFile = "..\\..\\..\\..\\samples\\hello-world.mist";
            if (!File.Exists(sourceFile))
                throw new Exception("can't find sourcefile!");

            var codeProvider = new CSharpCodeProvider();
            
            var parameters = new CompilerParameters()
            {
                GenerateExecutable = true,
                OutputAssembly = "out.exe",               
            };
            
            
            parameters.ReferencedAssemblies.Add("Marosoft.Mist.dll");

            parameters.EmbeddedResources.Add(sourceFile);

            var results = codeProvider.CompileAssemblyFromSource(
                parameters, 
                Bootstrapper.CreateSource());

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
