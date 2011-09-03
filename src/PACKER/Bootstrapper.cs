
namespace Marosoft.Mist.Packer
{
    class Bootstrapper
    {
        const string TEMPLATE = @"
		using System;
        using System.Linq;
		using Marosoft.Mist;
		using System.IO;
		using System.Reflection;
		namespace Marosoft.Mist.StandaloneApp {
		    class Program { 
		        static void Main(string[] args) {

		            var ass = Assembly.GetExecutingAssembly();
                    var resourceNames = ass.GetManifestResourceNames();

		            var mistDllResource = resourceNames.First(n => n.Contains(""Marosoft.Mist.dll""));
                    var mistDll = Assembly.Load(ReadResource(ass, mistDllResource));

                    var mistResource = resourceNames.First(n => !n.Contains(""Marosoft.Mist.dll""));
		            using(var mistStream = ass.GetManifestResourceStream(mistResource))
                    {
		                using (var streamReader = new StreamReader(mistStream))
		                {
		                    var source = streamReader.ReadToEnd();

                            System.Type type = mistDll.GetType(""Marosoft.Mist.EmbeddedMist"");
                            dynamic mist = System.Activator.CreateInstance(type);

		                    mist.Evaluate(source);
		                }
                    }
		        } 

                static byte[] ReadResource(Assembly ass, string resourceName)
                {
                    using (Stream s = ass.GetManifestResourceStream(resourceName))
                    {
                        byte[] buffer = new byte[1024];
                        using (MemoryStream ms = new MemoryStream())
                        {
                            while (true)
                            {
                                int read = s.Read(buffer, 0, buffer.Length);
                                if (read <= 0)
                                    return ms.ToArray();
                                ms.Write(buffer, 0, read);
                            }
                        }
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
