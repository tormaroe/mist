using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Marosoft.Mist;
using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;

namespace test.Parsing
{
    [TestFixture]
    public class ParserTests
    {        
        [Test]
        public void Simple_form_no_arguments()
        {
            Parse("(foo)");
            expressions.Count().ShouldEqual(1);
            expr1.Token.Type.ShouldEqual(Tokens.LIST);
            expr1.Elements.First().Token.Text.ShouldEqual("foo");
            expr1.Elements.First().Token.Type.ShouldEqual(Tokens.SYMBOL);
        }

        [Test]
        public void Simple_form_one_symbol_argument()
        {
            Parse("(foo bar)");
            
            expressions.Count().ShouldEqual(1);

            expr1.Token.Type.ShouldEqual(Tokens.LIST);

            expr1.Elements.First().Token.Type.ShouldEqual(Tokens.SYMBOL);
            expr1.Elements.First().Token.Text.ShouldEqual("foo");

            expr1.Elements.Second().Token.Type.ShouldEqual(Tokens.SYMBOL);
            expr1.Elements.Second().Token.Text.ShouldEqual("bar");
        }

        [Test]
        public void Simple_form_integer_and_string_arguments()
        {
            Parse("(foo 1 \"2\")");
            expressions.Count().ShouldEqual(1);
            expr1.Elements.Second().Token.Type.ShouldEqual(Tokens.INT);
            expr1.Elements.Second().Token.Text.ShouldEqual("1");
            expr1.Elements.Third().Token.Type.ShouldEqual(Tokens.STRING);
            expr1.Elements.Third().Token.Text.ShouldEqual("\"2\"");
        }

        [Test]
        public void Nested_form()
        {
            Parse("(foo (bar \"abc\") 123)");

            expressions.Count().ShouldEqual(1);

            expr1.Elements.First().Token.Text.ShouldEqual("foo");
            expr1.Elements.Second().Token.Type.ShouldEqual(Tokens.LIST);
            expr1.Elements.Third().Token.Text.ShouldEqual("123");

            var inner = expr1.Elements.Second();

            inner.Elements.First().Token.Text.ShouldEqual("bar");
            inner.Elements.Second().Token.Text.ShouldEqual("\"abc\"");
        }


        [Test]
        public void Nested_form_v2()
        {
            Parse("((bar))");

            var innerList = expr1.Elements.First();
            innerList.Token.Type.ShouldEqual(Tokens.LIST);
            innerList.Elements.First().Token.Text.ShouldEqual("bar");
        }

        [Test]
        public void Empty_list()
        {
            Parse("()");

            expr1.Token.Type.ShouldEqual(Tokens.LIST);
            expr1.Elements.ShouldBeEmpty();
        }

        [Test]
        public void Several_list()
        {
            Parse("() (list 1 2 3) (foo)");

            expressions.Count().ShouldEqual(3);
        }



        IEnumerable<Expression> expressions;

        private Expression expr1 { get { return expressions.First(); } }
        
        private void Parse(string source)
        {
            var parser = new Parser(new Lexer(Tokens.All));
            expressions = parser.Parse(source);
        }

    }
}
