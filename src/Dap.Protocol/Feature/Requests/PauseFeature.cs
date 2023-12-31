using MediatR;
using OmniSharp.Extensions.JsonRpc;
using OmniSharp.Extensions.JsonRpc.Generation;

// ReSharper disable once CheckNamespace
namespace OmniSharp.Extensions.DebugAdapter.Protocol
{
    namespace Requests
    {
        [Parallel]
        [Method(RequestNames.Pause, Direction.ClientToServer)]
        [GenerateHandler]
        [GenerateHandlerMethods]
        [GenerateRequestMethods]
        public record PauseArguments : IRequest<PauseResponse>
        {
            /// <summary>
            /// Pause execution for this thread.
            /// </summary>
            public long ThreadId { get; init; }
        }

        public record PauseResponse;
    }
}
