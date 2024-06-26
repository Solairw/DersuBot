﻿using Solairw.DersuBot.Handlers.Attributes;
using Solairw.DersuBot.Handlers.Text.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Solairw.DersuBot.Core.Handlers.Text;

[TelegramUpdateHandler(UpdateType.Message)]
public class TextMessageHandler : ITelegramBotUpdateHandler
{
    private readonly CommandParser _commandParser;

    public TextMessageHandler(CommandParser commandParser)
    {
        _commandParser = commandParser;
    }

    public Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message.Type != MessageType.Text)
            return Task.CompletedTask;

        var messageText = update.Message.Text;
        if (string.IsNullOrWhiteSpace(messageText))
            return Task.CompletedTask;

        var textIsCommand = messageText.StartsWith('/');
        if (!textIsCommand)
            return Task.CompletedTask;

        var chat = update.Message.Chat;
        var sender = update.Message.From;

        var splitText = messageText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var commandName = splitText[0];
        var commandArguments = splitText.Skip(1).ToArray();

        if (_commandParser.TryParse(commandName, out var command))
            return command.HandleAsync(botClient, chat, sender, cancellationToken, commandArguments);

        return botClient.SendTextMessageAsync(chat.Id,
            $"{sender.FirstName}, Дерсу не смог распознать команду: {messageText}", cancellationToken: cancellationToken);
    }
}