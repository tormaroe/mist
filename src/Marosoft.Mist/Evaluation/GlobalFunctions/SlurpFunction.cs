using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class SlurpFunction : BuiltInFunction
    {
        public SlurpFunction(Bindings scope)
            : base("slurp", scope)
        {
            DocString = StringExpression.Create(@"(slurp f)
Reads the file named by f into a list of strings (separated by 
line breaks) and returns it.");

            Precondition = args =>
                    args.Count() == 1 && args.First().Token.Type == Tokens.STRING;

            Implementation = args =>
            {
                var file = (string)args.First().Value;
                var txt = System.IO.File.ReadAllText(file);
                return StringExpression.Create(txt);                
            };
        }
    }
}
