using System;
using System.Linq;
using Marosoft.Mist.Parsing;
using System.Collections.Generic;

namespace Marosoft.Mist.Evaluation.GlobalFunctions
{
    /// <summary>
    /// Base class for built in functions that manipulates a stream of
    /// elements (ListExpression) with a function and returns a new element
    /// (quite possibly another list). 
    /// 
    /// Typical implementations include: map, filter, reduce
    /// </summary>
    public abstract class ListManipulatorFunction : BuiltInFunction
    {
        public ListManipulatorFunction(
            string symbol, 
            Bindings scope, 
            Func<Function, IEnumerable<Expression>, Expression> manipulation)
            : 
            base(symbol, scope)
        {
            Precondition = args =>
                args.Count() == 2 // Enhance to take more than one list
                &&
                args.First() is Function
                &&
                args.Second() is ListExpression;

            Implementation = args =>
            {
                var f = (Function)args.First();
                var sourceList = (ListExpression)args.Second();

                return manipulation.Invoke(f, sourceList.Elements);
            };
        }
    }
}
