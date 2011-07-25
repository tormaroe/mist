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
            AddBinding("nil", new Expression(new Token(Tokens.SYMBOL, "nil")) { Value = null });
            AddBinding("true", new Expression(new Token(Tokens.SYMBOL, "true")) { Value = true });
            AddBinding("false", new Expression(new Token(Tokens.SYMBOL, "false")) { Value = false });
            
            /**
             *      Adding the "built in" functions implemented in C#
             **/

            AddBinding(new Function("+", this)
            {
                Precondition = args => 
                    args.Count() > 0 
                    && args.All(a => a.Token.Type == Tokens.INT),
                Implementation = args =>
                    new Expression(new Token(args.First().Token.Type,
                        args.Select(expr => (int)expr.Value).Sum().ToString())),
            });
            
            AddBinding(new Function("-", this)
            {
                Precondition = args =>
                    args.Count() > 0
                    && args.All(a => a.Token.Type == Tokens.INT),
                Implementation = args =>
                    new Expression(new Token(args.First().Token.Type,
                        args.Select(expr => (int)expr.Value).Aggregate((x, y) => x - y).ToString())),
            });
        }
    }
}