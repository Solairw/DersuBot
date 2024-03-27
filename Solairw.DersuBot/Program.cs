// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Solairw.DersuBot.Configuration;
using Solairw.DersuBot.Core;
using Solairw.DersuBot.Dependencies;
using Solairw.DersuBot.Handlers;
using Solairw.DersuBot.Handlers.Text.Commands;
using Solairw.DersuBot.Telegram;
using Telegram.Bot.Polling;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
        {
            services.AddMemoryCache();
            services.Configure<Settings>(context.Configuration.GetSection("Settings"));
            services.AddSingleton<TelegramBotMessageHandlersFactory>();
            services.AddSingleton<IUpdateHandler, TelegramUpdateHandler>();
            services.AddSingleton<CommandParser>();
            services.AddHostedService<Worker>();

            services.AddSingleton(services =>
            {
                var settings = services.GetService<IOptions<Settings>>();
                var updateHandler = services.GetService<IUpdateHandler>();
                // var loggerFactory = services.GetService<ILoggerFactory>();
                
                return new Dersu(settings.Value.TelegramBotToken, updateHandler);
            });

            services.RegisterTelegramDependencies();
        }
    ).Build()
    .RunAsync();
        