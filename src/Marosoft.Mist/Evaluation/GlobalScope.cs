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
        public GlobalScope()
        {
            AddBinding("nil", new SpecialSymbolExpression("nil", null));
            AddBinding("true", new SpecialSymbolExpression("true", true));
            AddBinding("false", new SpecialSymbolExpression("false", false));
            
            /**
             *      Adding the "built in" functions implemented in C#
             **/

            AddBinding(new BuiltInFunction("+", this)
            {
                Precondition = args => 
                    args.Count() > 0 
                    && args.All(a => a.Token.Type == Tokens.INT),
                Implementation = args =>
                    ExpressionFactory.Create(new Token(args.First().Token.Type,
                        args.Select(expr => (int)expr.Value).Sum().ToString())),
            });

            AddBinding(new BuiltInFunction("-", this)
            {
                Precondition = args =>
                    args.Count() > 0
                    && args.All(a => a.Token.Type == Tokens.INT),
                Implementation = args =>
                    ExpressionFactory.Create(new Token(args.First().Token.Type,
                        args.Select(expr => (int)expr.Value).Aggregate((x, y) => x - y).ToString())),
            });

            AddBinding(new BuiltInFunction("=", this)
            {
                Precondition = args =>
                    args.Count() >= 2,
                Implementation = args =>
                {
                    var firstValue = args.First().Value;
                    if (args.Skip(1).All(x => x.Value.Equals(firstValue)))
                        return Resolve("true");
                    return Resolve("false");
                },
            });

            AddBinding(new BuiltInFunction("slurp", this)
            {
                Precondition = args =>
                    args.Count() == 1 && args.First().Token.Type == Tokens.STRING,
                Implementation = args =>
                {
                    var file = (string)args.First().Value;
                    var lines = System.IO.File.ReadLines(file);
                    var list = new ListExpression();
                    list.Elements.AddRange(lines.Select(StringExpression.Create));
                    return list;
                },
            });
        }
    }
}