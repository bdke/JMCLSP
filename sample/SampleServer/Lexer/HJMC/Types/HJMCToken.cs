using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;

namespace JMCLSP.Lexer.HJMC.Types
{
    internal class HJMCToken
    {
        public HJMCTokenType Type { get; set; }
        public List<string> Values { get;set; }
    }
}
