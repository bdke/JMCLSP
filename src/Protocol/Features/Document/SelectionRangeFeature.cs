using System.Diagnostics;
using OmniSharp.Extensions.JsonRpc;
using OmniSharp.Extensions.JsonRpc.Generation;
using OmniSharp.Extensions.LanguageServer.Protocol.Client;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Generation;
using OmniSharp.Extensions.LanguageServer.Protocol.Serialization;
using OmniSharp.Extensions.LanguageServer.Protocol.Server.Capabilities;

// ReSharper disable once CheckNamespace
namespace OmniSharp.Extensions.LanguageServer.Protocol
{
    namespace Models
    {
        [Parallel]
        [Method(TextDocumentNames.SelectionRange, Direction.ClientToServer)]
        [GenerateHandler("OmniSharp.Extensions.LanguageServer.Protocol.Document")]
        [GenerateHandlerMethods]
        [GenerateRequestMethods(typeof(ITextDocumentLanguageClient), typeof(ILanguageClient))]
        [RegistrationOptions(typeof(SelectionRangeRegistrationOptions))]
        [Capability(typeof(SelectionRangeCapability))]
        public partial record SelectionRangeParams : ITextDocumentIdentifierParams, IPartialItemsRequest<Container<SelectionRange>?, SelectionRange>,
                                                     IWorkDoneProgressParams
        {
            /// <summary>
            /// The text document.
            /// </summary>
            public TextDocumentIdentifier TextDocument { get; init; } = null!;

            /// <summary>
            /// The positions inside the text document.
            /// </summary>
            public Container<Position> Positions { get; init; } = null!;
        }

        [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
        public partial record SelectionRange
        {
            /// <summary>
            /// The [range](#Range) of this selection range.
            /// </summary>
            public Range Range { get; init; } = null!;

            /// <summary>
            /// The parent selection range containing this range. Therefore `parent.range` must contain `this.range`.
            /// </summary>
            public SelectionRange Parent { get; init; } = null!;

            private string DebuggerDisplay => $"{Range} {{{Parent}}}";

            /// <inheritdoc />
            public override string ToString()
            {
                return DebuggerDisplay;
            }
        }

        [GenerateRegistrationOptions(nameof(ServerCapabilities.SelectionRangeProvider))]
        [RegistrationName(TextDocumentNames.SelectionRange)]
        public partial class SelectionRangeRegistrationOptions : ITextDocumentRegistrationOptions, IWorkDoneProgressOptions, IStaticRegistrationOptions
        {
        }
    }

    namespace Client.Capabilities
    {
        [CapabilityKey(nameof(ClientCapabilities.TextDocument), nameof(TextDocumentClientCapabilities.SelectionRange))]
        public partial class SelectionRangeCapability : DynamicCapability
        {
            /// <summary>
            /// The maximum number of folding ranges that the client prefers to receive per document. The value serves as a
            /// hint, servers are free to follow the limit.
            /// </summary>
            [Optional]
            public int? RangeLimit { get; set; }

            /// <summary>
            /// If set, the client signals that it only supports folding complete lines. If set, client will
            /// ignore specified `startCharacter` and `endCharacter` properties in a FoldingRange.
            /// </summary>
            public bool LineFoldingOnly { get; set; }
        }
    }

    namespace Document
    {
    }
}
