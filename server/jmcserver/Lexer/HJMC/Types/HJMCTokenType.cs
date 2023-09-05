using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMCLSP.Lexer.HJMC.Types
{
    public enum HJMCTokenType
    {
        UNKNOWN,
        DEFINE,
        BIND,
        CREDIT,
        INCLUDE,
        COMMAND,
        DEL,
        OVERRIDE,
        UNINSTALL,
        STATIC,
        NOMETA
    }
}
