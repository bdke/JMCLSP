using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using JMCLSP.Lexer.Types;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using Serilog;
using Range = OmniSharp.Extensions.LanguageServer.Protocol.Models.Range;

namespace JMCLSP.Lexer
{
    internal class JMCLexer
    {
        public static Regex SPLIT_PATTERN = 
            new(@"(\/\/.*)|(\`(?:.|\s)*\`)|(\b-?\d*\.?\d+\b)|([""\'].*[""\'])|(\s|\;|\{|\}|\[|\]|\(|\)|\|\||&&|==|!=|!|,|:|\=\>|\=)");

        public List<JMCToken> Tokens { get; set; } = new();

        private readonly string RawText;
        private readonly string[] SplitedText;
        private readonly string[] TrimmedText;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public JMCLexer(string text)
        {
            RawText = text;
            SplitedText = SPLIT_PATTERN.Split(RawText);
            TrimmedText = SplitedText.Select(x => x.Trim()).ToArray();
            var currentPos = 0;
            for (var i = 0; i < TrimmedText.Length; i++)
            {
                if (string.IsNullOrEmpty(TrimmedText[i]))
                {
                    currentPos += SplitedText[i].Length;
                    continue;
                }

                var token = Tokenize(TrimmedText[i], currentPos ,Tokens);
                if (token != null)
                {
                    Tokens.Add(token);
                }
                currentPos += SplitedText[i].Length;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pos"></param>
        /// <param name="parsedTokens"></param>
        /// <returns></returns>
        public JMCToken? Tokenize(string text, int pos , IEnumerable<JMCToken> parsedTokens)
        {
            var position = OffsetToPosition(pos, RawText);
            var type = GetTokenType(text);
            return new()
            {
                Offset = pos,
                Position = position,
                TokenType = type,
                Value = text,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public JMCToken? GetJMCToken(Position pos)
        {
            for (var i = 0; i < Tokens.Count ;i++)
            {
                var c = Tokens[i];
                var next = Tokens[i + 1];
                if (next != null)
                {
                    var range = new Range(c.Position, next.Position);
                    if (range.Contains(pos))
                    {
                        return c;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private static Position OffsetToPosition(int offset, string text)
        {
            var line = 0;
            var col = 0;
            for (var i = 0; i < offset; i++)
            {
                if (text[i] == '\n')
                {
                    line++;
                    col = 0;
                } 
                else
                {
                    col++;
                }
            }
            
            return new Position(line, col);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static JMCTokenType GetTokenType(string text)
        {
            switch (text)
            {
                //keyword
                case "function":
                    return JMCTokenType.FUNCTION;
                case "switch":
                    return JMCTokenType.SWITCH;
                case "case":
                    return JMCTokenType.CASE;
                case "if":
                    return JMCTokenType.IF;
                case "else":
                    return JMCTokenType.ELSE;
                case "do":
                    return JMCTokenType.DO;
                case "while":
                    return JMCTokenType.WHILE;
                case "new":
                    return JMCTokenType.NEW;

                //brackets
                case "(":
                    return JMCTokenType.LPAREN;
                case ")":
                    return JMCTokenType.RPAREN;
                case "{":
                    return JMCTokenType.LCP;
                case "}":
                    return JMCTokenType.RCP;
                case "[":
                    return JMCTokenType.LMP;
                case "]":
                    return JMCTokenType.RMP;
                case ":":
                    return JMCTokenType.COLON;
                case ";":
                    return JMCTokenType.SEMI;
                default:
                    break;
            }

            //special cases
            if (new Regex(@"^\$[a-zA-Z_.][0-9a-zA-Z_.]*$").IsMatch(text)) return JMCTokenType.VARIABLE;
            else if (new Regex(@"^\$[a-zA-Z_.][0-9a-zA-Z_.]*\s*\.").IsMatch(text)) return JMCTokenType.VARIABLE_CALL;
            else if (new Regex(@"^(?![0-9])\S+$").IsMatch(text)) return JMCTokenType.LITERAL;

            return JMCTokenType.UNKNOWN;
        }
    }
}
