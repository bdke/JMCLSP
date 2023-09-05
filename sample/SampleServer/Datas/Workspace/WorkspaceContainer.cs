using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmniSharp.Extensions.LanguageServer.Protocol;

namespace JMCLSP.Datas.Workspace
{
    internal class WorkspaceContainer : List<Workspace>
    {
        public JMCFile? GetJMCFile(DocumentUri uri)
        {
            var items = ToArray().AsSpan();
            for (var i = 0; i < items.Length; i++)
            {
                ref var item = ref items[i];
                var f = item.FindJMCFile(uri);
                if (f != null)
                {
                    return f;
                }
            }
            return null;
        }
    }
}
