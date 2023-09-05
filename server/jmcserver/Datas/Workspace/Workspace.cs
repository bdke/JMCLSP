using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OmniSharp.Extensions.LanguageServer.Protocol;

namespace JMCLSP.Datas.Workspace
{
    internal class Workspace
    {
        public List<JMCFile> JMCFiles { get; set; } = new();
        public List<HJMCFile> HJMCFiles { get; set; } = new();
        public JMCConfig Config { get; set; }
        public string Path { get; set; }
        public DocumentUri DocumentUri { get; set; }
        public Workspace(DocumentUri Uri)
        {
            var fspath = DocumentUri.GetFileSystemPath(Uri);
            if (fspath == null)
                return;
            Path = fspath;
            DocumentUri = Uri;

            var jmcfiles = Directory.GetFiles(fspath, "*.jmc", SearchOption.AllDirectories);
            JMCFiles = jmcfiles.Select(v => new JMCFile(v)).ToList();

            var hjmcfiles = Directory.GetFiles(fspath, "*.hjmc", SearchOption.AllDirectories);
            HJMCFiles = hjmcfiles.Select(v => new HJMCFile(v)).ToList();

            var config = Directory.GetFiles(fspath, "jmc_config.json");
        }

        /// <summary>
        /// Search for a JMC File
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public JMCFile? FindJMCFile(DocumentUri uri) => JMCFiles.Find(v => v.DocumentUri == uri);
    }
}
