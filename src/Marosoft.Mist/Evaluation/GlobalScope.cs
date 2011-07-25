using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation
{
    public class GlobalScope : BasicScope
    {                
        public GlobalScope() : base(null)
        {
            AddBinding(new Function("+", this)
            {
                Precondition = args => 
                    args.Count() > 0 
                    && args.All(a => a.Token.Type == Tokens.INT),
                Implementation = args =>
                    new Expression(new Token(args.First().Token.Type,
                        args.Select(expr => Int32.Parse(expr.Token.Text)).Sum().ToString())),
            });
        }
    }
}