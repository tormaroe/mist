using Marosoft.Mist.Parsing;
using System.Linq;
using Marosoft.Mist.Lexing;
using System;

namespace Marosoft.Mist.Evaluation.Special
{
    [SpecialForm("new")]
    public class New : SpecialForm
    {
        public override Expression Call(Expression expr)
        {
            //if (expr.Elements.Count != 2 || expr.Elements.Second().Token.Type != Tokens.STRING)
            //    throw new MistException("Load takes a single string argument.");

            //var source = Environment.EvaluateString("(slurp " + expr.Elements.Second().ToString() + ")");

            // WORK IN PROGRESS !!!!

            var typeName = expr.Elements[1].Token.Text;

            Type type = Type.GetType(typeName);

            object instance = Activator.CreateInstance(type);

            return instance.ToExpression();
        }
    }
}
