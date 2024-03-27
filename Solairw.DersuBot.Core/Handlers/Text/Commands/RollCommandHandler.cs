using Solairw.DersuBot.Handlers.Attributes;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Solairw.DersuBot.Handlers.Text.Commands;

[Command("/roll")]
public class RollCommandHandler : ICommandHandler
{
    // private enum RollType : byte

    private static readonly Random Random = new();

    public async Task HandleAsync(ITelegramBotClient botClient, Chat chat, User sender, CancellationToken cancellationToken,
        params string[] args)
    {
        var rollMessage = GetRoll(args);
        if (rollMessage == null)
        {
            await botClient.SendTextMessageAsync(chat.Id,
                $"⚠Передано некорректное значение для параметра команды /roll", cancellationToken: cancellationToken);
                
            return;
        }

        await botClient.SendTextMessageAsync(chat.Id, $"{sender.FirstName} с помощью рандома выбирает {rollMessage}",
            cancellationToken: cancellationToken, parseMode: ParseMode.Html);
    }

    private string? GetRoll(string[] arguments)
    {
        if (arguments.Length > 1)
            return PickRandom(arguments);

        if (arguments.Length == 0)
            return RandomDice();

        if (int.TryParse(arguments[0], out var number) && number > 0)
            return $"{Random.Next(0, number)}";

        return null;
    }

    private string RandomDice()
    {
        return $"{Random.Next(1, 7)}";
    }

    private string PickRandom(string[] strings)
    {
        return strings[Random.Next(0, strings.Length)];
    }
}