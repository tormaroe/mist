using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Marosoft.Mist.Lexing;

namespace test.Lexing
{
    [TokenizeThis("(foo)")]
    public class Simplest_expression : BaseLexerTest
    {
        [Test]
        public void Test()
        {
            ExpectTokens(Tokens.LEFTPAREN, Tokens.SYMBOL, Tokens.RIGHTPAREN);
            ExpectTokens("(", "foo", ")");
        }
    }

    [TokenizeThis("(foo \"bar zot\")")]
    public class Expression_with_string_argument : BaseLexerTest
    {
        [Test]
        public void Test()
        {
            ExpectTokens(Tokens.LEFTPAREN, Tokens.SYMBOL, Tokens.STRING, Tokens.RIGHTPAREN);
            ExpectTokens("(", "foo", "\"bar zot\"", ")");
        }
    }

    [TokenizeThis(@"(foo ""bar
zot"")")]
    public class Expression_with_string_argument_with_newline : BaseLexerTest
    {
        [Test]
        public void Test()
        {
            ExpectTokens(Tokens.LEFTPAREN, Tokens.SYMBOL, Tokens.STRING, Tokens.RIGHTPAREN);
            ExpectTokens("(", "foo", "\"bar\r\nzot\"", ")");
        }
    }

    [TokenizeThis("\t(\r\nfoo      \"bar zot\"\r\n\t)\t")]
    public class Whitespace_should_not_bother_me : BaseLexerTest
    {
        [Test]
        public void Test()
        {
            ExpectTokens(Tokens.LEFTPAREN, Tokens.SYMBOL, Tokens.STRING, Tokens.RIGHTPAREN);
            ExpectTokens("(", "foo", "\"bar zot\"", ")");
        }
    }

    [TokenizeThis("(foo 0 123 -1000)")]
    public class Expression_with_integer_arguments : BaseLexerTest
    {
        [Test]
        public void Test()
        {
            ExpectTokens(Tokens.LEFTPAREN, Tokens.SYMBOL,
                Tokens.INT, Tokens.INT, Tokens.INT,
                Tokens.RIGHTPAREN);
            ExpectTokens("(", "foo", "0", "123", "-1000", ")");
        }
    }

    [TokenizeThis(@"
;; This is a comment
(foo a ; This is a comment 
     b) ; This is a comment as well
    ")]
    public class Comments_should_be_removed : BaseLexerTest
    {
        [Test]
        public void Test()
        {
            ExpectTokens(Tokens.LEFTPAREN, Tokens.SYMBOL,
                Tokens.SYMBOL, Tokens.SYMBOL,
                Tokens.RIGHTPAREN);
            ExpectTokens("(", "foo", "a", "b", ")");
        }
    }

    [TokenizeThis("(foo (bar (zot 1) 2))")]
    public class Nested_expressions : BaseLexerTest
    {
        [Test]
        public void Test()
        {
            ExpectTokens(Tokens.LEFTPAREN, Tokens.SYMBOL,
                Tokens.LEFTPAREN, Tokens.SYMBOL, 
                Tokens.LEFTPAREN, Tokens.SYMBOL, Tokens.INT,
                Tokens.RIGHTPAREN, Tokens.INT, 
                Tokens.RIGHTPAREN, Tokens.RIGHTPAREN);
            ExpectTokens("(", "foo", "(", "bar", "(", "zot", "1", ")", "2", ")", ")");
        }
    }
}