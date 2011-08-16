﻿// <autogenerated>
// This code was generated by a tool. Any changes made manually will be lost
// the next time this code is regenerated.
// </autogenerated>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Marosoft.Mist.Lexing
{
    public static class Tokens
    {
		public const int EOF = -1;
		public const int WS = 0;
		public const int INT = 1;
		public const int SYMBOL = 2;
		public const int COMMENT = 3;
		public const int STRING = 4;
		public const int LEFTPAREN = 5;
		public const int RIGHTPAREN = 6;
		public const int FUNCTION = 7;
		public const int MACRO = 8;
		public const int LIST = 9;


		public static readonly Dictionary<int, string> TokenNames = new Dictionary<int, string>
        {
			{EOF, "EOF"},
			{WS, "WS"},
			{INT, "INT"},
			{SYMBOL, "SYMBOL"},
			{COMMENT, "COMMENT"},
			{STRING, "STRING"},
			{LEFTPAREN, "LEFTPAREN"},
			{RIGHTPAREN, "RIGHTPAREN"},
			{FUNCTION, "FUNCTION"},
			{MACRO, "MACRO"},
			{LIST, "LIST"},
        
        };
                
        public static readonly List<TokenRecognizer> All = new List<TokenRecognizer>
        {
			new TokenRecognizer(WS, "^([ \\t\\r\\n])+", false),
			new TokenRecognizer(INT, "^(-)?(\\d)+", true),
			new TokenRecognizer(SYMBOL, "^([a-zA-Z\\+\\-\\?\\=\\*])+([\\w\\+\\-\\?\\=\\.\\*!])*", true),
			new TokenRecognizer(COMMENT, "^;[^\\r\\n]*", false),
			new TokenRecognizer(STRING, "^\"[^\"]*\"", true),
			new TokenRecognizer(LEFTPAREN, "^\\(", true),
			new TokenRecognizer(RIGHTPAREN, "^\\)", true),

        };
    }
}
