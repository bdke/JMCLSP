﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using OmniSharp.Extensions.DebugAdapter.Protocol;
using OmniSharp.Extensions.DebugAdapter.Protocol.Client;
using OmniSharp.Extensions.DebugAdapter.Protocol.Models;
using OmniSharp.Extensions.DebugAdapter.Protocol.Requests;
using OmniSharp.Extensions.DebugAdapter.Shared;
using OmniSharp.Extensions.JsonRpc;

namespace OmniSharp.Extensions.DebugAdapter.Client
{
    public class DebugAdapterClientOptions : DebugAdapterRpcOptionsBase<DebugAdapterClientOptions>, IDebugAdapterClientRegistry, IInitializeRequestArguments
    {
        public DebugAdapterClientOptions()
        {
            WithAssemblies(typeof(DebugAdapterClientOptions).Assembly, typeof(DebugAdapterRequestRouter).Assembly);
            RequestProcessIdentifier = new ParallelRequestProcessIdentifier();
        }

        public string? ClientId { get; set; }
        public string? ClientName { get; set; }
        public string AdapterId { get; set; } = null!;
        public string? Locale { get; set; }
        public bool LinesStartAt1 { get; set; }
        public bool ColumnsStartAt1 { get; set; }
        public PathFormat? PathFormat { get; set; }
        public bool SupportsVariableType { get; set; }
        public bool SupportsVariablePaging { get; set; }
        public bool SupportsRunInTerminalRequest { get; set; }
        public bool SupportsMemoryReferences { get; set; }
        public bool SupportsProgressReporting { get; set; }
        public bool SupportsInvalidatedEvent { get; set; }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.AddHandler(
            string method, IJsonRpcHandler handler, JsonRpcHandlerOptions? options
        )
        {
            return AddHandler(method, handler, options);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.
            AddHandler(string method, JsonRpcHandlerFactory handlerFunc, JsonRpcHandlerOptions? options)
        {
            return AddHandler(method, handlerFunc, options);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.AddHandlers(params IJsonRpcHandler[] handlers)
        {
            return AddHandlers(handlers);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.AddHandler(
            JsonRpcHandlerFactory handlerFunc, JsonRpcHandlerOptions? options
        )
        {
            return AddHandler(handlerFunc, options);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.AddHandler(IJsonRpcHandler handler, JsonRpcHandlerOptions? options)
        {
            return AddHandler(handler, options);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.AddHandler<TTHandler>(JsonRpcHandlerOptions? options)
        {
            return AddHandler<TTHandler>(options);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.AddHandler<TTHandler>(string method, JsonRpcHandlerOptions? options)
        {
            return AddHandler<TTHandler>(method, options);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.AddHandler(Type type, JsonRpcHandlerOptions? options)
        {
            return AddHandler(type, options);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.AddHandler(string method, Type type, JsonRpcHandlerOptions? options)
        {
            return AddHandler(method, type, options);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.AddHandlerLink(string fromMethod, string toMethod)
        {
            return AddHandlerLink(fromMethod, toMethod);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.OnJsonRequest(
            string method, Func<JToken, Task<JToken>> handler, JsonRpcHandlerOptions? options
        )
        {
            return OnJsonRequest(method, handler, options);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.OnJsonRequest(
            string method, Func<JToken, CancellationToken, Task<JToken>> handler, JsonRpcHandlerOptions? options
        )
        {
            return OnJsonRequest(method, handler, options);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.OnRequest<TParams, TResponse>(
            string method, Func<TParams, Task<TResponse>> handler, JsonRpcHandlerOptions? options
        )
        {
            return OnRequest(method, handler, options);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.OnRequest<TParams, TResponse>(
            string method, Func<TParams, CancellationToken, Task<TResponse>> handler, JsonRpcHandlerOptions? options
        )
        {
            return OnRequest(method, handler, options);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.OnRequest<TResponse>(
            string method, Func<Task<TResponse>> handler, JsonRpcHandlerOptions? options
        )
        {
            return OnRequest(method, handler, options);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.OnRequest<TResponse>(
            string method, Func<CancellationToken, Task<TResponse>> handler, JsonRpcHandlerOptions? options
        )
        {
            return OnRequest(method, handler, options);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.OnRequest<TParams>(
            string method, Func<TParams, Task> handler, JsonRpcHandlerOptions? options
        )
        {
            return OnRequest(method, handler, options);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.OnRequest<TParams>(
            string method, Func<TParams, CancellationToken, Task> handler, JsonRpcHandlerOptions? options
        )
        {
            return OnRequest(method, handler, options);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.OnRequest<TParams>(
            string method, Func<CancellationToken, Task> handler, JsonRpcHandlerOptions? options
        )
        {
            return OnRequest<TParams>(method, handler, options);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.OnNotification<TParams>(
            string method, Action<TParams, CancellationToken> handler, JsonRpcHandlerOptions? options
        )
        {
            return OnNotification(method, handler, options);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.OnJsonNotification(
            string method, Action<JToken> handler, JsonRpcHandlerOptions? options
        )
        {
            return OnJsonNotification(method, handler, options);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.OnJsonNotification(
            string method, Func<JToken, CancellationToken, Task> handler, JsonRpcHandlerOptions? options
        )
        {
            return OnJsonNotification(method, handler, options);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.OnJsonNotification(
            string method, Func<JToken, Task> handler, JsonRpcHandlerOptions? options
        )
        {
            return OnJsonNotification(method, handler, options);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.OnJsonNotification(
            string method, Action<JToken, CancellationToken> handler, JsonRpcHandlerOptions? options
        )
        {
            return OnJsonNotification(method, handler, options);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.OnNotification<TParams>(
            string method, Action<TParams> handler, JsonRpcHandlerOptions? options
        )
        {
            return OnNotification(method, handler, options);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.OnNotification<TParams>(
            string method, Func<TParams, CancellationToken, Task> handler, JsonRpcHandlerOptions? options
        )
        {
            return OnNotification(method, handler, options);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.OnNotification<TParams>(
            string method, Func<TParams, Task> handler, JsonRpcHandlerOptions? options
        )
        {
            return OnNotification(method, handler, options);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.OnNotification(
            string method, Action handler, JsonRpcHandlerOptions? options
        )
        {
            return OnNotification(method, handler, options);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.OnNotification(
            string method, Func<CancellationToken, Task> handler, JsonRpcHandlerOptions? options
        )
        {
            return OnNotification(method, handler, options);
        }

        IDebugAdapterClientRegistry IJsonRpcHandlerRegistry<IDebugAdapterClientRegistry>.OnNotification(
            string method, Func<Task> handler, JsonRpcHandlerOptions? options
        )
        {
            return OnNotification(method, handler, options);
        }
    }
}
