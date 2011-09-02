using Marosoft.Mist.Parsing;
using Marosoft.Mist.Evaluation;
using System;

namespace Marosoft.Mist
{
    /// <summary>
    /// Facade class for using Mist from within C# or other .NET language.
    /// Create an instance and use it's methods to interact with the interpreter.
    /// 
    /// Typical usage is to first set some variables and/or functions using Set, 
    /// then evaluate one or more Mist files using Load, and finally read some
    /// values back using Get and/or call some Mist function from the loaded
    /// files using Call.
    /// </summary>
    public class EmbeddedMist
    {
        private Interpreter _mist;

        public EmbeddedMist()
        {
            _mist = new Interpreter();
        }

        /// <summary>
        /// Bind an object to a symbol in the Mist environment.
        /// </summary>
        /// <param name="symbol">The name of the symbol to represent the value.</param>
        /// <param name="value">The value to bind to the symbol.</param>
        public void Set(string symbol, object value)
        {
            _mist.CurrentScope.AddBinding(new SymbolExpression(symbol), value.ToExpression());
        }

        /// <summary>
        /// Create a function in the Mist environment by binding a Func of T
        /// to a symbol.
        /// </summary>
        /// <typeparam name="T">The resturn type of the function</typeparam>
        /// <param name="symbol">The name of function to create in Mist</param>
        /// <param name="function">The lambda implementing the function</param>
        public void Set<T>(string symbol, Func<T> function)
        {
            _mist.CurrentScope.AddBinding(
                new FunctionFromFunc<T>(_mist.CurrentScope, symbol) 
                { Function = function });
        }

        /// <summary>
        /// Create a function of one argument in the Mist environment 
        /// by binding a Func to a symbol.
        /// </summary>
        /// <typeparam name="T">The type of the single argument</typeparam>
        /// <typeparam name="TResult">The resturn type of the function</typeparam>
        /// <param name="symbol">The name of function to create in Mist</param>
        /// <param name="function">The lambda implementing the function</param>
        public void Set<T, TResult>(string symbol, Func<T, TResult> function)
        {
            _mist.CurrentScope.AddBinding(
                new FunctionFromFunc<T, TResult>(_mist.CurrentScope, symbol) 
                { Function = function });
        }

        /// <summary>
        /// Create a function of two arguments in the Mist environment 
        /// by binding a Func to a symbol.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument</typeparam>
        /// <typeparam name="T2">The type of the second argument</typeparam>
        /// <typeparam name="TResult">The resturn type of the function</typeparam>
        /// <param name="symbol">The name of function to create in Mist</param>
        /// <param name="function">The lambda implementing the function</param>
        public void Set<T1, T2, TResult>(string symbol, Func<T1, T2, TResult> function)
        {
            _mist.CurrentScope.AddBinding(
                new FunctionFromFunc<T1, T2, TResult>(_mist.CurrentScope, symbol) 
                { Function = function });
        }

        /// <summary>
        /// Create a function of three arguments in the Mist environment 
        /// by binding a Func to a symbol.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument</typeparam>
        /// <typeparam name="T2">The type of the second argument</typeparam>
        /// <typeparam name="T3">The type of the third argument</typeparam>
        /// <typeparam name="TResult">The resturn type of the function</typeparam>
        /// <param name="symbol">The name of function to create in Mist</param>
        /// <param name="function">The lambda implementing the function</param>
        public void Set<T1, T2, T3, TResult>(string symbol, Func<T1, T2, T3, TResult> function)
        {
            _mist.CurrentScope.AddBinding(
                new FunctionFromFunc<T1, T2, T3, TResult>(_mist.CurrentScope, symbol) 
                { Function = function });
        }

        /// <summary>
        /// Load and evaluate a file containing Mist code in the environment.
        /// The Mist code can use any symbols you have previously bound.
        /// </summary>
        /// <param name="path">The relative or absolute path to the mist file</param>
        public void Load(string path)
        {
            _mist.EvaluateString(string.Format("(load \"{0}\")", path));
        }

        public void Evaluate(string source)
        {
            _mist.EvaluateString(source);
        }

        /// <summary>
        /// Get a value from the Mist environment by specifying a symbol.
        /// </summary>
        /// <typeparam name="T">The expected type of the value</typeparam>
        /// <param name="symbol">The name of the symbol to look up</param>
        /// <returns>A value from the Mist environment</returns>
        public T Get<T>(string symbol)
        {
            var expr = _mist.CurrentScope.Resolve(symbol);
            return (T)expr.Value;
        }

        /// <summary>
        /// Call the Mist function with no formal parameters bound to a symbol 
        /// in the Mist environment.
        /// </summary>
        /// <typeparam name="TResult">The expected return type of the function</typeparam>
        /// <param name="symbol">The name of the function to call</param>
        /// <returns>The evaluated result of the call</returns>
        public TResult Call<TResult>(string symbol)
        {
            return InternalCall<TResult>(symbol, f => f.Call());
        }

        /// <summary>
        /// Call the Mist function with a single formal parameter bound to a symbol 
        /// in the Mist environment.
        /// </summary>
        /// <typeparam name="T">The type of the argument</typeparam>
        /// <typeparam name="TResult">The expected return type of the function</typeparam>
        /// <param name="symbol">The name of the function to call</param>
        /// <param name="argument">The argument to provide in the function call</param>
        /// <returns>The evaluated result of the call</returns>
        public TResult Call<T, TResult>(string symbol, T argument)
        {
            return InternalCall<TResult>(symbol,
                f => f.Call(
                    argument.ToExpression()));
        }

        /// <summary>
        /// Call the Mist function with two formal parameters bound to a symbol 
        /// in the Mist environment.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument</typeparam>
        /// <typeparam name="T2">The type of the second argument</typeparam>
        /// <typeparam name="TResult">The expected return type of the function</typeparam>
        /// <param name="symbol">The name of the function to call</param>
        /// <param name="argument1">The first argument to provide in the function call</param>
        /// <param name="argument2">The second argument to provide in the function call</param>
        /// <returns>The evaluated result of the call</returns>
        public TResult Call<T1, T2, TResult>(string symbol, T1 argument1, T2 argument2)
        {
            return InternalCall<TResult>(symbol, 
                f => f.Call(
                    argument1.ToExpression(), 
                    argument2.ToExpression()));
        }

        /// <summary>
        /// Call the Mist function with three formal parameters bound to a symbol 
        /// in the Mist environment.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument</typeparam>
        /// <typeparam name="T2">The type of the second argument</typeparam>
        /// <typeparam name="T3">The type of the third argument</typeparam>
        /// <typeparam name="TResult">The expected return type of the function</typeparam>
        /// <param name="symbol">The name of the function to call</param>
        /// <param name="argument1">The first argument to provide in the function call</param>
        /// <param name="argument2">The second argument to provide in the function call</param>
        /// <param name="argument3">The third argument to provide in the function call</param>
        /// <returns>The evaluated result of the call</returns>
        public TResult Call<T1, T2, T3, TResult>(string symbol, T1 argument1, T2 argument2, T3 argument3)
        {
            return InternalCall<TResult>(symbol,
                f => f.Call(
                    argument1.ToExpression(),
                    argument2.ToExpression(),
                    argument3.ToExpression()));
        }

        private TResult InternalCall<TResult>(string symbol, Func<Function, Expression> callDelegate)
        {
            var f = _mist.CurrentScope.Resolve(symbol) as Function;

            if (f == null)
                throw new MistException(symbol + " is not bound to a function.");

            var exprResult = callDelegate(f);
            return (TResult)exprResult.Value;
        }
    }
}
