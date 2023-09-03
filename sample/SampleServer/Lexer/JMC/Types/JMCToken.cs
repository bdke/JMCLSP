using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;

namespace JMCLSP.Lexer.JMC.Types
{
    internal class JMCToken
    {
        public JMCTokenType TokenType { get; set; } = JMCTokenType.UNKNOWN;
        public Position Position { get; set; } = new(-1, -1);
        public int Offset { get; set; } = -1;
        public string Value { get; set; } = string.Empty;
        public JMCToken() { }
        public JMCToken(JMCTokenType tokenType, Position position, string value, int offset)
        {
            TokenType = tokenType;
            Position = position;
            Value = value;
            Offset = offset;
        }
    }
}
