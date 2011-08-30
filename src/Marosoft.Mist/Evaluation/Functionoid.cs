using System;
using System.Linq;
using System.Collections.Generic;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;
using System.Reflection;

namespace Marosoft.Mist.Evaluation
{
    /// <summary>
    /// A function object, also called a functor, functional, or functionoid,
    /// is a computer programming construct allowing an object to be invoked
    /// or called as though it were an ordinary function, usually with the
    /// same syntax.
    /// </summary>
    public class Functionoid : Function
    {        
        public static bool CanWrap(Expression e)
        {
            return !(e is ListExpression) 
                && !(e is Function)
                && e.Value != null;
        }
        
        private readonly Expression _objectExpression;
        
        public Functionoid(Expression e)
        {
            _objectExpression = e;            
        }

        private Type Type
        {
            get
            {
                return _objectExpression.Value.GetType();
            }
        }

        private object Object
        {
            get
            {
                return _objectExpression.Value;
            }
        }

        public Expression Call(IEnumerable<Expression> args)
        {
            return 
                GetMethod(args, GetMessage(args))
                .Invoke(Object, ArgValues(args))
                .ToExpression();
        }

        private static SymbolExpression GetMessage(IEnumerable<Expression> args)
        {
            var message = args.First() as SymbolExpression;

            if (message == null)
                throw new MistException("First argument to Functionoid must be a symbol");

            return message;
        }

        private MethodInfo GetMethod(IEnumerable<Expression> args, SymbolExpression message)
        {
            Type[] parameterTypes = ArgTypes(args);
            var method = Type.GetMethod((string)message.Value, parameterTypes);

            if (method == null)
                throw new MistException(string.Format(
                    "Functionoid {0} does not respond to message {1} with arguments {2}",
                    _objectExpression.Value.GetType(),
                    message.Value,
                    args.Skip(1).Select(a => string.Format("{0} {1}", a.Value.GetType(), a.Token.Text))
                        .Aggregate((o1, o2) => o1.ToString() + ", " + o2.ToString())));

            return method;
        }

        private static Type[] ArgTypes(IEnumerable<Expression> args)
        {
            return args.Skip(1).Select(a => a.Value.GetType()).ToArray();
        }

        private static object[] ArgValues(IEnumerable<Expression> args)
        {
            return args.Skip(1).Select(a => a.Value).ToArray();
        }
    }
}
