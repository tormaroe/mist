using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marosoft.Mist.Lexing;
using Marosoft.Mist.Evaluation;

namespace Marosoft.Mist.Parsing
{
    public class ListExpression : Expression
    {
        public ListExpression(IEnumerable<Expression> elements)
            : this()
        {
            Elements.AddRange(elements);
        }

        public ListExpression()
            : base(new Token(Tokens.LIST, string.Empty))
        {
        }

        public override object Value
        {
            get
            {
                return Elements
                    .Select(e => e.Value)
                    .ToList();
            }
            set
            {
                base.Value = value;
            }
        }

        public override Expression Evaluate(Evaluation.Bindings scope)
        {
            // A List evaluates either as a special form, a macro, or a function call

            if (SpecialForms.IsSpecialForm(Elements[0].Token.Text))
                return SpecialForms.CallSpecialForm(this);

            else if (IsMacro(Elements[0], scope))
                return CallMacro(scope);

            else
                return CallFunction(scope);
        }

        private bool IsMacro(Expression expr, Bindings scope)
        {
            return expr is SymbolExpression 
                && scope.Resolve(expr.Token.Text) is Macro;
        }

        private Expression CallMacro(Bindings scope)
        {
            var firstElem = Elements.First();
            var macro = scope.Resolve(firstElem.Token.Text) as Macro;
            return macro.ExpandAndEvaluate(Elements.Skip(1), scope);
        }

        private Expression CallFunction(Evaluation.Bindings scope)
        {
            return GetFirstExpressionAsFunction(scope)
                .Call(EvaluatedArguments(scope));
        }

        private Function GetFirstExpressionAsFunction(Bindings scope)
        {
            if (!(Elements[0] is Function))
                Elements[0] = Elements[0].Evaluate(scope); // recur to try to get a function

            if (Elements[0] is Function)
                return (Function)Elements[0];

            throw new MistException(Elements[0].ToString() + " is not a function");
        }

        private IEnumerable<Expression> EvaluatedArguments(Evaluation.Bindings scope)
        {
            return Elements.Skip(1).Select(e => e.Evaluate(scope));
        }

        public override string ToString()
        {
            return string.Format("({0})",
                Elements.Select(e => e.ToString())
                        .Aggregate((acc, e) => string.Format("{0} {1}", acc, e)));
        }
    }
}
