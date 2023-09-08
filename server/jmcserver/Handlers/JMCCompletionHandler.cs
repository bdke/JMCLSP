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

            

            return new(list);
        }
        protected override CompletionRegistrationOptions CreateRegistrationOptions(CompletionCapability capability, ClientCapabilities clientCapabilities) => new() 
        {
            AllCommitCharacters = new string[]
                {
                    ".", "#", " ", "/"
                },
            ResolveProvider = true,
            TriggerCharacters = new string[]
                {
                    ".", "#", " ", "/"
                },
            DocumentSelector = LanguageSelector.JMC
        };
    }
}
