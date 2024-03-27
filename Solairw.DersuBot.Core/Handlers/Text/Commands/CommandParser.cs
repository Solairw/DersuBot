namespace Solairw.DersuBot.Handlers.Text.Commands;

public class CommandParser
{
    private readonly Dictionary<string, ICommandHandler> _commandHandlers = new();
}