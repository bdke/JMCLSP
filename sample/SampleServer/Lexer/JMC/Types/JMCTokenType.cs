using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMCLSP.Lexer.JMC.Types
{
    public enum JMCTokenType
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
        LITERAL,
        EQUAL,
        NOT_EQUAL,
        GREATER_OR_EQUAL,
        LESS_OR_EQUAL,
        GREATER_THEN,
        LESS_THEN,
        PLUS_EQUAL,
        MINUS_EQUAL,
        DIVIDE_EQUAL,
        REMINDER_EQUAL,
        MULTIPLY_EQUAL,
        PLUS,
        MINUS,
        MULTIPLY,
        DIVIDE,
        REMINDER,
        ARROW_FUNC,
        OR,
        AND,
        COMMENT,
        MULTILINE_STRING,
        NUMBER,
        STRING
    }
}
