using Telegram.Bot;
using Telegram.Bot.Types;

namespace Solairw.DersuBot.Core.Handlers;

public interface ITelegramBotUpdateHandler
{
    Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
}