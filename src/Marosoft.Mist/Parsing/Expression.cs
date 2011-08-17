using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marosoft.Mist.Lexing;
using Marosoft.Mist.Evaluation;

namespace Marosoft.Mist.Parsing
{
    // ONGOING: Creating subclasses for Expression. Use them!
    // TODO: FunctionExpression

    public class Expression
    {
        public Token Token { get; private set; }
        public List<Expression> Elements { get; private set; }
        public object Value { get; set; }

        public StringExpression DocString { get; set; }
        
        public Expression(Token token)
        {
            Token = token;
            Elements = new List<Expression>();
        }

        /// <summary>
        /// Default evaluation - evaluates to self
        /// </summary>
        /// <param name="scope">Not used in base evaluator</param>
        public virtual Expression Evaluate(Bindings scope)
        {
            return this;
        }

        public override string ToString()
        {
            if (Value != null)
                return Value.ToString();

            return Token.ToString();
        }

        public virtual bool IsTrue
        {
            get
            {
                return true;
            }
        }

        public virtual bool IsNil
        {
            get
            {
                return false;
            }
        }
    }
}