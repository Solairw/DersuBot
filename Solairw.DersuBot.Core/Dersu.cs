using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace Solairw.DersuBot.Core;

public sealed class Dersu
{
    private readonly TelegramBotClient _telegramBotClient;

    public Dersu(string telegramBotToken, IUpdateHandler updateHandler)
    {
        _telegramBotClient = new TelegramBotClient(telegramBotToken);
        _telegramBotClient.StartReceiving(updateHandler, new ReceiverOptions
        {
            AllowedUpdates = new []
            {
               UpdateType.Message,
               UpdateType.EditedMessage,
               UpdateType.CallbackQuery
            }
        });
        
    }

   
}