using Telegram.Bot;
using Telegram.Bot.Types;

namespace Solairw.DersuBot.Handlers.Text.Commands;

public interface ICommandHandler
{
    Task HandleAsync(ITelegramBotClient botClient, Chat chat, User sender, CancellationToken cancellationToken,
        params string[] args);
}