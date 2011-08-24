using System.Collections.Generic;
using Marosoft.Mist.Parsing;
using System;
using System.Linq;

namespace Marosoft.Mist.Evaluation
{
    public class FunctionFromFunc<T> : BuiltInFunction
    {
        protected readonly dynamic fun;
        private readonly string symbol;
        public dynamic Function { set; protected get; }

        public FunctionFromFunc(Bindings scope, string symbol)
            : base(symbol, scope)
        {
            symbol = symbol;
        }

        protected override Expression InternalCall(IEnumerable<Expression> args)
        {
            ValidateArgs(args, expectedArgsCount: 0);
            return ((T)Function.Invoke()).ToExpression();
        }

        protected void ValidateArgs(IEnumerable<Expression> args, int expectedArgsCount)
        {
            if (args.Count() != expectedArgsCount)
                throw new MistException(symbol + " takes "
                    + expectedArgsCount + " arguments (not "
                    + args + ")");
        }
    }
    public class FunctionFromFunc<T, TResult> : FunctionFromFunc<TResult>
    {
        public FunctionFromFunc(Bindings scope, string symbol)
            : base(scope, symbol)
        {
        }

        protected override Expression InternalCall(IEnumerable<Expression> args)
        {
            ValidateArgs(args, expectedArgsCount: 1);
            return ((T)Function.Invoke((T)args.First().Value)).ToExpression();
        }
    }
    public class FunctionFromFunc<T1, T2, TResult> : FunctionFromFunc<TResult>
    {
        public FunctionFromFunc(Bindings scope, string symbol)
            : base(scope, symbol)
        {
        }

        protected override Expression InternalCall(IEnumerable<Expression> args)
        {
            ValidateArgs(args, expectedArgsCount: 2);
            return ((TResult)Function.Invoke(
                        (T1)args.First().Value,
                        (T2)args.Second().Value
                    )).ToExpression();
        }
    }

    public class FunctionFromFunc<T1, T2, T3, TResult> : FunctionFromFunc<TResult>
    {
        public FunctionFromFunc(Bindings scope, string symbol)
            : base(scope, symbol)
        {
        }

        protected override Expression InternalCall(IEnumerable<Expression> args)
        {
            ValidateArgs(args, expectedArgsCount: 3);
            return ((TResult)Function.Invoke(
                        (T1)args.First().Value,
                        (T2)args.Second().Value,
                        (T3)args.Third().Value
                    )).ToExpression();
        }
    }
}
