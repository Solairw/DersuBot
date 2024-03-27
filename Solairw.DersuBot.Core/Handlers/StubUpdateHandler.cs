using Solairw.DersuBot.Core.Handlers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Solairw.DersuBot.Handlers;

public class StubUpdateHandler : ITelegramBotUpdateHandler
{
    public Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}