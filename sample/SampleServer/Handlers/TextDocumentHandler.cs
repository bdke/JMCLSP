using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OmniSharp.Extensions.LanguageServer.Protocol;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Document;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using OmniSharp.Extensions.LanguageServer.Protocol.Server.Capabilities;

namespace JMCLSP.Handlers
{
    internal class TextDocumentHandler : TextDocumentSyncHandlerBase
    {
        private readonly ILogger<TextDocumentHandler> _logger;

        private readonly DocumentSelector _documentSelector = new(
            new DocumentFilter
            {
                Pattern = "**/*.jmc"
            }
        );

        public TextDocumentSyncKind Change { get; } = TextDocumentSyncKind.Full;

        public TextDocumentHandler(ILogger<TextDocumentHandler> logger)
        {
            _logger = logger;
        }
        public override Task<Unit> Handle(DidOpenTextDocumentParams request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Opened Document: ${request.TextDocument.Uri}");
            return Unit.Task;
        }
        public override Task<Unit> Handle(DidChangeTextDocumentParams request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Changed Document: ${request.TextDocument.Uri}");
            return Unit.Task;
        }
        public override Task<Unit> Handle(DidSaveTextDocumentParams request, CancellationToken cancellationToken) {
            _logger.LogDebug($"Saved Document: ${request.TextDocument.Uri}");
            return Unit.Task;
        }
        public override Task<Unit> Handle(DidCloseTextDocumentParams request, CancellationToken cancellationToken) {
            _logger.LogDebug($"Closed Document: ${request.TextDocument.Uri}");
            return Unit.Task;
        }

        public override TextDocumentAttributes GetTextDocumentAttributes(DocumentUri uri)
            => new(uri, "jmc");
        protected override TextDocumentSyncRegistrationOptions CreateRegistrationOptions(
            SynchronizationCapability capability, ClientCapabilities clientCapabilities)
            => new()
            {
                DocumentSelector = _documentSelector,
                Change = Change,
                Save = new SaveOptions() { IncludeText = true }
            };
    }
}
