using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace test.Lexing
{
    public class TokenizeThis : Attribute
    {
        public TokenizeThis(string input)
        {
            Input = input;
        }
        public string Input { get; private set; }
    }
}
