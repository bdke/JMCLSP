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
        OP_EQUAL,
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
        REMAINDER,
        ARROW_FUNC,
        OR,
        AND,
        COMMENT,
        MULTILINE_STRING,
        NUMBER,
        STRING,
        CLASS,
        COMMAND_LITERAL,
        COMMAND_UNKNOWN,
        COMMAND_VALUE,
        COMMAND_LCP,
        COMMAND_RCP,
        COMMAND_LMP,
        COMMAND_RMP,
        COMMAND_LPAREN,
        COMMAND_RPAREN,
        COMMAND_FLOAT_OR_DOUBLE,
        COMMAND_INT_OR_LONG,
        COMMAND_CARET,
        COMMAND_TILDE,
        COMMAND_VARIABLE_CALL,
        COMMAND_VARIABLE,
        CONDITION_LITERAL,
        CONDITION_UNKNOWN
    }
}
