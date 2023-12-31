using MediatR;
using OmniSharp.Extensions.JsonRpc;
using OmniSharp.Extensions.JsonRpc.Generation;

// ReSharper disable once CheckNamespace
namespace OmniSharp.Extensions.DebugAdapter.Protocol
{
    namespace Requests
    {
        [Parallel]
        [Method(RequestNames.Goto, Direction.ClientToServer)]
        [GenerateHandler]
        [GenerateHandlerMethods]
        [GenerateRequestMethods]
        public record GotoArguments : IRequest<GotoResponse>
        {
            /// <summary>
            /// Set the goto target for this thread.
            /// </summary>
            public long ThreadId { get; init; }

            /// <summary>
            /// The location where the debuggee will continue to run.
            /// </summary>
            public long TargetId { get; init; }
        }

        public record GotoResponse;
    }
}
