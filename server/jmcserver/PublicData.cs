using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMCLSP.Datas.Workspace;

namespace JMCLSP
{
    internal static class PublicData
    {
        public static WorkspaceContainer Workspaces = new();
        public static string LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Logs");
    }
}
