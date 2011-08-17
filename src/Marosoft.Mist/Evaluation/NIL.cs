using System.Linq;
using System.Collections.Generic;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;
using System;

namespace Marosoft.Mist.Evaluation
{
    public class NIL : Expression
    {
        public static NIL Instance { get; private set; }

        public NIL()
            : base(new Token(Tokens.SYMBOL, "nil"))
        {
            Value = "nil";
            Instance = this;
        }
        public override bool IsTrue
        {
            get
            {
                return false;
            }
        }
        public override bool IsNil
        {
            get
            {
                return true;
            }
        }
    }
    public class FALSE : Expression
    {
        public static FALSE Instance { get; private set; }

        public FALSE()
            : base(new Token(Tokens.SYMBOL, "false"))
        {
            Value = false;
            Instance = this;
        }
        public override bool IsTrue
        {
            get
            {
                return false;
            }
        }
    }
    public class TRUE : Expression
    {
        public static TRUE Instance { get; private set; }

        public TRUE()
            : base(new Token(Tokens.SYMBOL, "true"))
        {
            Value = true;
            Instance = this;
        }
    }
}
