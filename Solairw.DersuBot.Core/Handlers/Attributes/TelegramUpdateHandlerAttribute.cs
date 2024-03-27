using Telegram.Bot.Types.Enums;

namespace Solairw.DersuBot.Handlers.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class TelegramUpdateHandlerAttribute : Attribute
{
    public UpdateType UpdateType { get; }

    public TelegramUpdateHandlerAttribute(UpdateType updateType)
    {
        UpdateType = updateType;
    }
}