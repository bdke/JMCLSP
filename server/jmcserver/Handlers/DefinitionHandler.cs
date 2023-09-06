using JMCLSP.Datas;
using JMCLSP.Helper;
using Microsoft.Extensions.Logging;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Document;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;

namespace JMCLSP.Handlers
{
    internal sealed class DefinitionHandler : DefinitionHandlerBase
    {
        private readonly ILogger<DefinitionHandler> _logger;

        public DefinitionHandler(ILogger<DefinitionHandler> logger) => _logger = logger;

        public override async Task<LocationOrLocationLinks> Handle(DefinitionParams request, CancellationToken cancellationToken)
        {
            var file = StaticData.Workspaces.GetJMCFile(request.TextDocument.Uri);
            if (file == null)
            {
                var link = new List<LocationOrLocationLink>();
                return link;
            }
            else
            {
                var link = new List<LocationOrLocationLink>();
                var token = file.Lexer.GetJMCToken(request.Position);
                if (token != null)
                {
                    _logger.LogInformation($"{LoggerHelper.ObjectToJson(token)}");
                }
                return link;
            }
        }

        protected override DefinitionRegistrationOptions CreateRegistrationOptions(
            DefinitionCapability capability, ClientCapabilities clientCapabilities)
            => new()
            {
                DocumentSelector = LanguageSelector.JMC,
            };
    }
}
