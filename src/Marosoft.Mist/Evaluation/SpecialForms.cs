using System;
using System.Collections.Generic;
using System.Linq;
using Marosoft.Mist.Parsing;
using System.Reflection;

namespace Marosoft.Mist.Evaluation
{
    public class SpecialForms
    {
        private readonly Environment _environment;
        private Dictionary<string, Func<Expression, Expression>> _formsMap;

        public SpecialForms(Environment environment)
        {
            _environment = environment;            
            _formsMap = new Dictionary<string, Func<Expression, Expression>>();
            Array.ForEach(Assembly.GetCallingAssembly().GetTypes(), LoadSpecialForm);
        }

        public void LoadSpecialForm(Type t)
        {
            var formAttribute = Attribute.GetCustomAttribute(t, typeof(SpecialFormAttribute)) as SpecialFormAttribute;

            if (formAttribute != null)
            {
                SpecialForm f = (SpecialForm)Activator.CreateInstance(t);
                f.Environment = _environment;
                _formsMap.Add(formAttribute.Name, expr => f.Call(expr));
            }
        }

        public bool IsSpecialForm(string name)
        {
            return _formsMap.ContainsKey(name);
        }

        public Expression CallSpecialForm(Expression expr)
        {
            return _formsMap[expr.Elements.First().Token.Text](expr);
        }
    }
}
