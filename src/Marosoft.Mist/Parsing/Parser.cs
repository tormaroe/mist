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
            return new ListExpression(UntilToken(Tokens.RIGHTPAREN, Element));
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
            var cell = ExpressionFactory.Create(CurrentToken);
            Consume();
            return cell;
        }

        private void Match(int x)
        {
            if (CurrentToken.Type == x)
                Consume();
            else
                throw new ParseException(
                    "expecting {0}; found {1}",
                    Tokens.TokenNames[x],
                    CurrentToken);
        }

        protected bool TokenIs(int tokenType)
        {
            return CurrentToken.Type == tokenType;
        }

        protected Token CurrentToken
        {
            get
            {
                try
                {
                    return _tokens[_tokenIndex];
                }
                catch (ArgumentOutOfRangeException outOfRange)
                {
                    throw new ParseException("Parser trying to read beond end of token stream.\n{0}", GetEndOfTokenStreamForExceptionMessage(_tokens));
                }
            }
        }

        private string GetEndOfTokenStreamForExceptionMessage(List<Token> tokens)
        {
            const int tokensToDisplay = 8;
            int toSkip = tokens.Count > tokensToDisplay ? tokens.Count - tokensToDisplay : 0;
            return tokens
                .Skip(toSkip)
                .Aggregate("", (acc, token) => acc + " " + token.ToString())
                + " ... ";
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