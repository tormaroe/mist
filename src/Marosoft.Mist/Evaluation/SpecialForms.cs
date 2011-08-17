using System;
using System.Collections.Generic;
using System.Linq;
using Marosoft.Mist.Parsing;
using System.Reflection;

namespace Marosoft.Mist.Evaluation
{
    public static class SpecialForms
    {
        private static Environment _environment;
        private static Dictionary<string, Func<Expression, Expression>> _formsMap;

        public static void Load(Environment environment)
        {
            _environment = environment;            
            _formsMap = new Dictionary<string, Func<Expression, Expression>>();
            Array.ForEach(Assembly.GetCallingAssembly().GetTypes(), LoadSpecialForm);
        }

        public static void LoadSpecialForm(Type t)
        {
            var formAttribute = Attribute.GetCustomAttribute(t, typeof(SpecialFormAttribute)) as SpecialFormAttribute;

            if (formAttribute != null)
            {
                SpecialForm f = (SpecialForm)Activator.CreateInstance(t);
                f.Environment = _environment;
                _formsMap.Add(formAttribute.Name, expr => f.Call(expr));
            }
        }

        public static bool IsSpecialForm(string name)
        {
            return _formsMap.ContainsKey(name);
        }

        public static Expression CallSpecialForm(Expression expr)
        {
            return _formsMap[expr.Elements.First().Token.Text](expr);
        }
    }
}
