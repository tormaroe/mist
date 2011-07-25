using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation
{
    public class BuiltInFunctions
    {
        private Dictionary<string, Func<IEnumerable<Expression>, Expression>> _functions;

        public bool Contains(string function)
        {
            return _functions.ContainsKey(function);
        }

        public Func<IEnumerable<Expression>, Expression> this[string function]
        {
            get
            {
                return _functions[function];
            }
        }

        public BuiltInFunctions()
        {
            _functions = new Dictionary<string, Func<IEnumerable<Expression>, Expression>>();

            _functions.Add("+", args =>
            {
                return new Expression(new Token(args.First().Token.Type,
                    args.Select(expr => Int32.Parse(expr.Token.Text)).Sum().ToString()));
            });
        }
    }
}
