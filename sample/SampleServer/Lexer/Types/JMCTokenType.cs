using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMCLSP.Lexer.Types
{
    internal enum JMCTokenType
    {
        UNKNOWN,
        FUNCTION,
        SWITCH,
        VARIABLE,
        CASE,
        IF,
        ELSE,
        DO,
        WHILE,
        NEW,
        LPAREN,
        RPAREN,
        LCP,
        LMP,
        RMP,
        RCP,
        COLON,
        SEMI,
        VARIABLE_CALL,
        LITERAL
    }
}
