using System.Diagnostics;
using JMCLSP.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Window;
using OmniSharp.Extensions.LanguageServer.Server;
using Serilog;

namespace JMCLSP
{

    public class Program
    {
        private static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                        .Enrich.FromLogContext()
                        .WriteTo.File($"{ExtensionData.LogPath}/jmclsp.log", rollingInterval: RollingInterval.Day)
                        .MinimumLevel.Verbose()
                        .CreateLogger();


            var data = new ExtensionData();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            var server = await LanguageServer.From(
                options =>
                    options
                       .WithInput(Console.OpenStandardInput())
                       .WithOutput(Console.OpenStandardOutput())
                       .ConfigureLogging(
                            x => x
                                .AddSerilog(Log.Logger)
                                .AddLanguageProtocolLogging()
                                .SetMinimumLevel(LogLevel.Debug)
                        )
                       .WithHandler<TextDocumentHandler>()
                       .WithHandler<DefinitionHandler>()
                       .WithHandler<JMCCompletionHandler>()
                       .WithServices(x => x.AddLogging(b => b.SetMinimumLevel(LogLevel.Trace)))
                       .WithServices(
                            services =>
                            {
                                services.AddSingleton(
                                    provider =>
                                    {
                                        var loggerFactory = provider.GetService<ILoggerFactory>();
                                        var logger = loggerFactory.CreateLogger<LSPLogger>();

                                        logger.LogInformation("logger set up done");

                                        return new LSPLogger(logger);
                                    }
                                );

                                services.AddSingleton(
                                    new ConfigurationItem
                                    {
                                        Section = "jmc",
                                    }
                                );
                            }
                        )
                       .OnInitialize(
                            async (server, request, token) =>
                            {
                                //capabilities
                                var result = new InitializeResult()
                                {
                                    Capabilities = new()
                                    {
                                        TextDocumentSync = TextDocumentSyncKind.Incremental,
                                        CompletionProvider = new()
                                        {
                                            ResolveProvider = true,
                                            TriggerCharacters = new string[]
                                            {
                                                ".", "#", " ", "/", "$"
                                            }
                                        },
                                        SignatureHelpProvider = new()
                                        {
                                            TriggerCharacters = new string[]
                                            {
                                                "(", ",", " "
                                            },
                                            RetriggerCharacters = new string[]
                                            {
                                                ",", " "
                                            }
                                        },
                                        SemanticTokensProvider = new()
                                        {
                                            Legend = new()
                                            {
                                                TokenTypes = SemanticTokenType.Defaults.ToList(),
                                                TokenModifiers = SemanticTokenModifier.Defaults.ToList()
                                            },
                                            Full = true,
                                            Range = true,
                                        },
                                        DefinitionProvider = true,
                                        Workspace = new()
                                        {
                                            WorkspaceFolders = new()
                                            {
                                                Supported = true
                                            },
                                        }
                                    }
                                };
                            }
                        )
                       .OnInitialized(
                            async (server, request, response, token) =>
                            {
                                var folders = request.WorkspaceFolders;
                                foreach (var folder in folders)
                                {
                                    var workspace = new Datas.Workspace.Workspace(folder.Uri);
                                    ExtensionData.Workspaces.Add(workspace);
                                }
                            }
                        )
                       .OnStarted(
                            async (languageServer, token) =>
                            {
                                languageServer.LogInfo(ExtensionData.LogPath);
                                languageServer.LogInfo("Server Started");
                            }
                        )
            ).ConfigureAwait(false);

            await server.WaitForExit.ConfigureAwait(false);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Fatal(e.ExceptionObject.ToString());
        }
    }

    internal class LSPLogger
    {
        private readonly ILogger<LSPLogger> _logger;

        public LSPLogger(ILogger<LSPLogger> logger)
        {
            _logger = logger;
        }
    }
}
