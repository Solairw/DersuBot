using Microsoft.Extensions.Logging;
using Solairw.DersuBot.Handlers;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Solairw.DersuBot.Telegram;

public sealed class TelegramUpdateHandler : IUpdateHandler
{
    private readonly TelegramBotMessageHandlersFactory _telegramBotMessageHandlersFactory;
    private readonly ILogger _logger;

    public TelegramUpdateHandler(
        TelegramBotMessageHandlersFactory telegramBotMessageHandlersFactory,
        ILoggerFactory loggerFactory)
    {
        _telegramBotMessageHandlersFactory = telegramBotMessageHandlersFactory;
        _logger = loggerFactory.CreateLogger<TelegramUpdateHandler>();
    }
    
    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
            await _telegramBotMessageHandlersFactory.GetHandler(update.Type)
                .HandleAsync(botClient, update, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "При обработке обновления телеграма произошла ошибка");
        }
    }

    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}