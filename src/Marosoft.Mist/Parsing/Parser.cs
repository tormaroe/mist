using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marosoft.Mist.Lexing;

namespace Marosoft.Mist.Parsing
{
    public class Parser
    {
        private readonly Lexer _lexer;
        private List<Token> _tokens;
        private int _tokenIndex;
        
        public Parser(Lexer lexer)
        {
            _lexer = lexer;
        }

        public IEnumerable<Expression> Parse(string source)
        {
            _tokenIndex = 0;
            _lexer.Tokenize(source);
            _tokens = _lexer.Tokens;

            return UntilToken(Tokens.EOF, List).ToList();
        }

        /// <summary>
        /// List : "(" Element* ")" ;
        /// </summary>
        private Expression List()
        {
            Match(Tokens.LEFTPAREN);
            var list = new Expression(new Token(Tokens.LIST, string.Empty));
            list.Elements.AddRange(UntilToken(Tokens.RIGHTPAREN, Element));
            return list;
        }

        /// <summary>
        /// Element : atom | List
        /// </summary>
        private Expression Element()
        {            
            if (TokenIs(Tokens.LEFTPAREN))
                return List();

            return GetCurrentCell();
        }

        private Expression GetCurrentCell()
        {
            return GetCurrentCell(CurrentToken.Type);
        }

        private Expression GetCurrentCell(int tokenType)
        {
            var cell = new Expression(CurrentToken);
            Consume();
            return cell;
        }

        private void Match(int x)
        {
            if (CurrentToken.Type == x)
                Consume();
            else
                throw new Exception(String.Format(
                    "expecting {0}; found {1}",
                    Tokens.TokenNames[x],
                    CurrentToken));
        }

        protected bool TokenIs(int tokenType)
        {
            return CurrentToken.Type == tokenType;
        }

        protected Token CurrentToken
        {
            get
            {
                return _tokens[_tokenIndex];
            }
        }

        /// <summary>
        /// Used to eat zero or more tokens (*) until specified token type reached
        /// </summary>
        protected IEnumerable<Expression> UntilToken(int tokenType, Func<Expression> block)
        {
            while (CurrentToken.Type != tokenType)
                yield return block();

            Consume();
        }

        protected void Consume()
        {
            _tokenIndex++;
        }
    }
}