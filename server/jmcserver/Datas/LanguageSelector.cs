using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;

namespace JMCLSP.Datas
{
    internal static class LanguageSelector
    {
        public static readonly DocumentSelector JMC = DocumentSelector.ForLanguage("jmc");
    }
}
