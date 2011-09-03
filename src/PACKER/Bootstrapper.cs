
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
}
