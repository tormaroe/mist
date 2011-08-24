using System.Linq;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;
using System.Collections.Generic;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    [GlobalFunction]
    public class SlurpFunction : BuiltInFunction
    {
        public SlurpFunction(Bindings scope)
            : base("slurp", scope)
        {
            DocString = @"(slurp f)
Reads the file named by f into a list of strings (separated by 
line breaks) and returns it.".ToExpression();

        }

        protected override Expression InternalCall(IEnumerable<Expression> args)
        {
            var file = (string)args.First().Value;
            var txt = System.IO.File.ReadAllText(file);
            return txt.ToExpression(); 
        }

        protected override bool Precondition(IEnumerable<Expression> args)
        {
            return args.Count() == 1 
                && args.First().Token.Type == Tokens.STRING;
        }
    }
}
