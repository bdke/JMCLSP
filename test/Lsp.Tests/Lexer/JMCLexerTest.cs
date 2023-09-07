using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using JMCLSP.Lexer.JMC;
using JMCLSP.Lexer.JMC.Types;
using Xunit;

namespace Lsp.Tests.Lexer
{
    //TODO: test case for commands
    public class JMCLexerTest
    {
        [Theory]
        [InlineData("function", JMCTokenType.FUNCTION)]
        [InlineData("switch", JMCTokenType.SWITCH)]
        [InlineData("case", JMCTokenType.CASE)]
        [InlineData("if", JMCTokenType.IF)]
        [InlineData("else", JMCTokenType.ELSE)]
        [InlineData("do", JMCTokenType.DO)]
        [InlineData("while", JMCTokenType.WHILE)]
        [InlineData("new", JMCTokenType.NEW)]
        [InlineData("class", JMCTokenType.CLASS)]
        public void Keyword_Tests(string text, JMCTokenType expected)
        {
            var type = JMCLexer.GetTokenType(text);
            Assert.Equal(expected, type);
        }

        [Theory]
        [MemberData(nameof(StatementTestData))]
        public void Statement_Tests(string text, IEnumerable<JMCTokenType> expected)
        {
            var lexer = new JMCLexer(text);
            var tokenTypes = lexer.Tokens.Select(v => v.TokenType).ToArray();
            expected.Should().Equal(tokenTypes);
        }

        [Theory]
        [InlineData("123", JMCTokenType.NUMBER)]
        [InlineData("1.23", JMCTokenType.NUMBER)]
        [InlineData("//test comment", JMCTokenType.COMMENT)]
        [InlineData("$test", JMCTokenType.VARIABLE)]
        [InlineData("$test.get()", JMCTokenType.VARIABLE_CALL)]
        public void SpecialCases_Tests(string text, JMCTokenType expected)
        {
            var type = JMCLexer.GetTokenType(text);
            Assert.Equal(expected, type);
        }

        public static IEnumerable<object[]> StatementTestData => new List<object[]>
        {
            new object[]
            {
                "function test() {}", new JMCTokenType[]
                {
                    JMCTokenType.FUNCTION, JMCTokenType.LITERAL, 
                    JMCTokenType.LPAREN, JMCTokenType.RPAREN, 
                    JMCTokenType.LCP, JMCTokenType.RCP,
                }
            },
            new object[]
            {
                "class name.name {}", new JMCTokenType[]
                {
                    JMCTokenType.CLASS, JMCTokenType.LITERAL, JMCTokenType.LCP, JMCTokenType.RCP
                }
            },
            new object[]
            {
                "switch ($test) {case 1: test();}", new JMCTokenType[]
                {
                    JMCTokenType.SWITCH, JMCTokenType.LPAREN, JMCTokenType.VARIABLE, JMCTokenType.RPAREN,
                    JMCTokenType.LCP, 
                    JMCTokenType.CASE, JMCTokenType.NUMBER, JMCTokenType.COLON,
                    JMCTokenType.LITERAL, JMCTokenType.LPAREN, JMCTokenType.RPAREN, JMCTokenType.SEMI,
                    JMCTokenType.RCP,
                }
            },
            new object[]
            {
                "if ($test == 3) {} else {}", new JMCTokenType[]
                {
                    JMCTokenType.IF, 
                    JMCTokenType.LPAREN, JMCTokenType.VARIABLE, JMCTokenType.OP_EQUAL, JMCTokenType.NUMBER ,JMCTokenType.RPAREN,
                    JMCTokenType.LCP, JMCTokenType.RCP,
                    JMCTokenType.ELSE,
                    JMCTokenType.LCP, JMCTokenType.RCP
                }
            }
        };
    }
}
