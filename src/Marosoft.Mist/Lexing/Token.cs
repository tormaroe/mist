using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marosoft.Mist.Lexing
{
    public class Token
    {
        public Token(int type, string text)
        {
            Text = text;
            Type = type;
        }
        public int Type { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return string.Format("<{0},\"{1}\">",
                Tokens.TokenNames[Type],
                Text.Replace("\n", "\\n").Replace("\r", "\\r"));
        }
    }
}
