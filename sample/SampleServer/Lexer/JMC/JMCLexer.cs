using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using JMCLSP.Lexer.JMC.Types;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Projection;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using Serilog;
using Range = OmniSharp.Extensions.LanguageServer.Protocol.Models.Range;

namespace JMCLSP.Lexer.JMC
{
    public class JMCLexer
    {
        public static Regex SPLIT_PATTERN =
            new(@"(\/\/.*)|(\`(?:.|\s)*\`)|(\b-?\d*\.?\d+\b)|([""\'].*[""\'])|(\s|\;|\{|\}|\[|\]|\(|\)|\|\||&&|==|!=|[\<\>]\=|[\<\>]|!|,|:|\=\>|[\+\-\*\%\/]\=|[\+\-\*\%\/])");

        public List<JMCToken> Tokens { get; set; } = new();
        public string RawText { get; set; }

        /// <summary>
        /// initialize the JMC lexer
        /// </summary>
        /// <param name="text">raw text</param>
        public JMCLexer(string text)
        {
            RawText = text;
            InitTokens();
        }

        public async Task InitTokensAsync() => InitTokens();

        /// <summary>
        /// 
        /// </summary>
        public void InitTokens()
        {
            Tokens.Clear();

            var splitedText = SPLIT_PATTERN.Split(RawText);
            var trimmedText = splitedText.Select(x => x.Trim()).ToArray();
            var currentPos = 0;

            var arr = trimmedText.AsSpan();
            for (var i = 0; i < trimmedText.Length; i++)
            {
                ref var value = ref trimmedText[i];
                if (string.IsNullOrEmpty(value))
                {
                    currentPos += splitedText[i].Length;
                    continue;
                }

                var token = Tokenize(value, currentPos, Tokens);
                if (token != null)
                {
                    Tokens.Add(token);
                }
                currentPos += splitedText[i].Length;
            }
        }

        public async Task ChangeRawTextAsync(TextDocumentContentChangeEvent change) => ChangeRawText(change);

        public void ChangeRawText(TextDocumentContentChangeEvent change)
        {
            var r = change.Range;
            if (r == null)
                return;

            var start = PositionToOffset(r.Start) + 1;
            var length = change.RangeLength;

            RawText = RawText.Remove(start, length).Insert(start, change.Text);
        }

        /// <summary>
        /// Tokenize a string
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pos">offset of the text</param>
        /// <param name="parsedTokens">previous tokens</param>
        /// <returns></returns>
        public JMCToken? Tokenize(string text, int pos, IEnumerable<JMCToken> parsedTokens)
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
        /// get a token by <see cref="Position"/>
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public JMCToken? GetJMCToken(Position pos)
        {
            var arr = Tokens.ToArray().AsSpan();
            for (var i = 0; i < Tokens.Count; i++)
            {
                ref var c = ref arr[i];
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
        /// Convert offset to <see cref="Position"/>
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private int PositionToOffset(Position pos)
        {
            var line = pos.Line;

            var currentLine = 0;
            var offset = 0;

            var chars = RawText.ToCharArray().AsSpan();
            for (; offset < RawText.Length; offset++)
            {
                ref var c = ref chars[offset];
                if (c == '\r')
                {
                    offset++;
                    currentLine++;
                }
                else if (c == '\n') currentLine++;
                if (currentLine == line) break;
            }

            return offset += pos.Character;
        }

        /// <summary>
        /// get <see cref="Position"/> by offset of the text
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="text">the whole document text</param>
        /// <returns><see cref="Position"/> of the offset</returns>
        private static Position OffsetToPosition(int offset, string text)
        {
            var line = 0;
            var col = 0;

            var arr = text.ToArray().AsSpan();
            for (var i = 0; i < offset; i++)
            {
                ref var value = ref arr[i];
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
        /// get type of the <see cref="string"/>
        /// </summary>
        /// <param name="text">string that required to be parsed</param>
        /// <returns><see cref="JMCTokenType"/> or <see cref="JMCTokenType.UNKNOWN"/> if it can't recognize</returns>
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

                //boolean ops
                case "==":
                    return JMCTokenType.EQUAL;
                case "!=":
                    return JMCTokenType.NOT_EQUAL;
                case ">=":
                    return JMCTokenType.GREATER_OR_EQUAL;
                case "<=":
                    return JMCTokenType.LESS_OR_EQUAL;
                case ">":
                    return JMCTokenType.GREATER_THEN;
                case "<":
                    return JMCTokenType.LESS_THEN;
                case "||":
                    return JMCTokenType.OR;
                case "&&":
                    return JMCTokenType.AND;

                //operators equal
                case "+=":
                    return JMCTokenType.PLUS_EQUAL;
                case "-=":
                    return JMCTokenType.MINUS_EQUAL;
                case "/=":
                    return JMCTokenType.DIVIDE_EQUAL;
                case "%=":
                    return JMCTokenType.REMINDER_EQUAL;
                case "*=":
                    return JMCTokenType.MULTIPLY_EQUAL;

                //operators
                case "+":
                    return JMCTokenType.PLUS;
                case "-":
                    return JMCTokenType.MINUS;
                case "*":
                    return JMCTokenType.MULTIPLY;
                case "/":
                    return JMCTokenType.DIVIDE;
                case "%":
                    return JMCTokenType.REMINDER;

                //others
                case "=>":
                    return JMCTokenType.ARROW_FUNC;

                default:
                    break;
            }

            //special cases
            if (new Regex(@"^\/\/.*$").IsMatch(text)) return JMCTokenType.COMMENT;
            else if (new Regex(@"^(\b-?\d*\.?\d+\b)$").IsMatch(text)) return JMCTokenType.NUMBER;
            else if (new Regex(@"^([""\'].*[""\'])$").IsMatch(text)) return JMCTokenType.STRING;
            else if (new Regex(@"^\`(?:.|\s)*\`$").IsMatch(text)) return JMCTokenType.MULTILINE_STRING;
            else if (new Regex(@"^\$[a-zA-Z_][0-9a-zA-Z_]*$").IsMatch(text)) return JMCTokenType.VARIABLE;
            else if (new Regex(@"^\$[a-zA-Z_][0-9a-zA-Z_]*\s*\.").IsMatch(text)) return JMCTokenType.VARIABLE_CALL;
            else if (new Regex(@"^(?![0-9])\S+$").IsMatch(text)) return JMCTokenType.LITERAL;
            else return JMCTokenType.UNKNOWN;
        }
    }
}
