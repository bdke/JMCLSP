using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMCLSP.Datas;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Document;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;

namespace JMCLSP.Handlers
{
    internal class JMCCompletionHandler : CompletionHandlerBase
    {
        public override async Task<CompletionItem> Handle(CompletionItem request, CancellationToken cancellationToken) => request;
        public override async Task<CompletionList> Handle(CompletionParams request, CancellationToken cancellationToken)
        {
            var file = ExtensionData.Workspaces.GetJMCFile(request.TextDocument.Uri);
            if (file == null)
            {
                return new();
            }

            var list = new List<CompletionItem>();

            var lexer = file.Lexer;
            var funcs = ExtensionData.Workspaces.GetJMCFunctionDefines()
                .SelectMany(v => v.Tokens)
                .DistinctBy(v => v.Value);
            var vars = ExtensionData.Workspaces.GetJMCVariables()
                .SelectMany(v => v.Tokens)
                .Where(v => !v.Value.EndsWith(".get", StringComparison.CurrentCulture))
                .DistinctBy(v => v.Value);

            foreach ( var func in funcs )
            {
                list.Add(new()
                {
                    Label = func.Value,
                    Kind = CompletionItemKind.Function,
                });
            }

            foreach ( var v in vars )
            {
                list.Add(new() 
                { 
                    Label = v.Value[1..],
                    Kind = CompletionItemKind.Variable, 
                    InsertText = v.Value
                });
            }

            return new(list);
        }
        protected override CompletionRegistrationOptions CreateRegistrationOptions(CompletionCapability capability, ClientCapabilities clientCapabilities) => new() 
        {
            ResolveProvider = true,
            TriggerCharacters = new string[]
                {
                    ".", "#", " ", "/", "$"
                },
            DocumentSelector = LanguageSelector.JMC
        };
    }
}
