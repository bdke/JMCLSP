using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMCLSP.Lexer.JMC.Types;

namespace Lsp.Tests.Lexer
{
    public class JMCLexerTestCase
    {
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

        public static IEnumerable<object[]> FormatFunctionsTestData = new List<object[]>()
        {
            new object[]
            {
                "class test{function test(){}}", "test.test"
            }
        };
    }
}
