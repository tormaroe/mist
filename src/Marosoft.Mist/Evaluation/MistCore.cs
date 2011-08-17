using System.Reflection;
using System.IO;

namespace Marosoft.Mist.Evaluation
{
    public static class MistCore
    {
        public static void Load(Environment env)
        {
            var assembly = Assembly.GetCallingAssembly();
            string[] manifestResourceNames = assembly.GetManifestResourceNames();
            var reader = new StreamReader(assembly.GetManifestResourceStream("Marosoft.Mist.MistCore.core.mist"));
            env.EvaluateString(reader.ReadToEnd());
        }
    }
}
