using JMCLSP.Datas;
using JMCLSP.Helper;
using JMCLSP.Lexer.JMC.Types;
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
            var file = ExtensionData.Workspaces.GetJMCFile(request.TextDocument.Uri);
            if (file == null)
            {
                var link = new List<LocationOrLocationLink>();
                return link;
            }
            else
            {
                var link = new List<LocationOrLocationLink>();
                var lexer = file.Lexer;
                var lexerTokens = lexer.Tokens;
                var currentToken = lexer.GetJMCToken(request.Position);
                if (currentToken == null) return link;

                var tokenIndex = file.Lexer.Tokens.IndexOf(currentToken);

                if (currentToken.TokenType == JMCTokenType.LITERAL &&
                    lexerTokens[tokenIndex + 1].TokenType == JMCTokenType.LPAREN)
                {
                    var defines = ExtensionData.Workspaces.GetJMCFunctionDefines();
                    foreach (var define in defines)
                    {
                        var result = define.Tokens.Find(v => v.Value.Split(' ').Last() == currentToken.Value);
                        if (result != null)
                        {
                            var location = new LocationLink
                            {
                                OriginSelectionRange = currentToken.Range,
                                TargetRange = result.Range,
                                TargetSelectionRange = result.Range,
                                TargetUri = define.DocumentUri
                            };
                            link.Add(location);
                            break;
                        }
                    }
                }

                _logger.LogInformation($"Definition Token: {LoggerHelper.ObjectToJson(currentToken)}");
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
