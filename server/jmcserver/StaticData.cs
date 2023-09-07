using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using JMCLSP.Datas.Minecraft.Command;
using JMCLSP.Datas.Workspace;
using Newtonsoft.Json;

namespace JMCLSP
{
    internal class StaticData
    {
        public static readonly WorkspaceContainer Workspaces = new();
        public static readonly string LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Logs");
        public static string MinecraftVersion;
        public static CommandData CommandData { get; set; }

        public StaticData()
        {
            MinecraftVersion = "1.20.1";
            CommandData = new(GetCommandNodes(MinecraftVersion));
        }

        /// <summary>
        /// Update a minecraft version
        /// </summary>
        /// <param name="version"></param>
        public static void UpdateVersion(string version)
        {
            MinecraftVersion = version;
            CommandData = new(GetCommandNodes(MinecraftVersion));
        }

        /// <summary>
        /// Json command tree to memory data
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static Dictionary<string, CommandNode> GetCommandNodes(string version)
        {
            var v = version.Replace(".", "_");
            var asm = Assembly.GetExecutingAssembly();
            var resouceStream = asm.GetManifestResourceStream($"JMCLSP.Resource.{v}_commands.json");
            var reader = new StreamReader(resouceStream);
            var jsonText = reader.ReadToEnd();

            reader.Dispose();

            var root = JsonConvert.DeserializeObject<CommandNode>(jsonText);
            return root.Childrens;
        }
    }
}
