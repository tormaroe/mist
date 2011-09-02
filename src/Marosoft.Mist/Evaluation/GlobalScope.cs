using System;
using Marosoft.Mist.Parsing;
using System.Reflection;

namespace Marosoft.Mist.Evaluation
{
    public class GlobalScope : Bindings
    {                
        public GlobalScope()
        {
            AddBinding(new NIL());
            AddBinding(new TRUE());
            AddBinding(new FALSE());
            
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