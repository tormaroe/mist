﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Evaluation
{
    public class Interpreter : Environment
    {
        private SpecialForms _specialForms;
        
        public Interpreter()
        {
            _specialForms = new SpecialForms(this);            
            _scopeStack.Push(new GlobalScope());
        }

        private Stack<Bindings> _scopeStack = new Stack<Bindings>();

        public Bindings CurrentScope { get { return _scopeStack.Peek(); } }

        private Lazy<Parser> _parser = new Lazy<Parser>(() => new Parser(new Lexer(Tokens.All)));
        
        public Expression Evaluate(string code)
        {
            return Evaluate(_parser.Value.Parse(code));
        }

        public Expression Evaluate(IEnumerable<Expression> expressions)
        {
            Expression result = null;
            foreach (var expr in expressions)
                result = Evaluate(expr);
            return result;
        }

        public Expression Evaluate(Expression expr)
        {
            switch (expr.Token.Type)
            {
                case Tokens.LIST: return List(expr); 
                case Tokens.INT: return expr;
                case Tokens.STRING: return expr;
                case Tokens.SYMBOL: return CurrentScope.Resolve(expr.Token.Text);
                default:
                    throw new Exception(string.Format("Token {0} can't be evaluate", expr.Token));
            }
        }

        private Expression List(Expression expr)
        {
            var firstElem = expr.Elements.First();

            if (firstElem is ListExpression)
            {
                expr.Elements[0] = Evaluate(firstElem);
                return CallFunction(expr);
            }
            else if (_specialForms.IsSpecialForm(firstElem.Token.Text))
                return _specialForms.CallSpecialForm(expr);
            else if (firstElem is SymbolExpression)
                return CallFunction(expr);

            throw new MistException(string.Format("Can't evaluate {0} as function", firstElem.Token));
        }

        private Expression CallFunction(Expression expr)
        {
            var args = expr.Elements.Skip(1).Select(Evaluate);
            var funk = GetFunction(expr.Elements.First());

            return funk.Call(args);
        }

        private Function GetFunction(Expression fExpr)
        {
            if (fExpr.Token.Type == Tokens.FUNCTION)
                return (Function)fExpr;

            // Review this logic, not sure if/when needed!
            var f = CurrentScope.Resolve(fExpr.Token.Text);

            if (f is Function)
                return (Function)f;

            throw new Exception(fExpr.ToString() + " is not a function");
        }

        public T WithScope<T>(Bindings s, Func<T> call)
        {
            _scopeStack.Push(s);
            try
            {
                return call.Invoke();
            }
            finally
            {
                _scopeStack.Pop();
            }
        }

    }
}