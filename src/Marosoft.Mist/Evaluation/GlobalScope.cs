using System;
using Marosoft.Mist.Parsing;
using System.Reflection;

namespace Marosoft.Mist.Evaluation
{
    public class GlobalScope : Bindings
    {                
        public GlobalScope()
        {
            AddBinding("nil", new SpecialSymbolExpression("nil", null));
            AddBinding("true", new SpecialSymbolExpression("true", true));
            AddBinding("false", new SpecialSymbolExpression("false", false));
            
            // Adding the "built in" functions implemented in C#
            Array.ForEach(
                Assembly.GetCallingAssembly().GetTypes(), 
                AddGlobalFunction);
        }

        private void AddGlobalFunction(Type t)
        {
            bool isGlobalFunction = Attribute.GetCustomAttribute(t, typeof(GlobalFunctionAttribute)) != null;

            if (isGlobalFunction)
                AddBinding((BuiltInFunction)Activator.CreateInstance(t, this));
        }

    }
}