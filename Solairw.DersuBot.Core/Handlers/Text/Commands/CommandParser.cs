using System.Data;
using System.Reflection;
using Solairw.DersuBot.Handlers.Attributes;

namespace Solairw.DersuBot.Handlers.Text.Commands;

public class CommandParser
{
    private readonly Dictionary<string, ICommandHandler> _commandCache = new();

    public CommandParser(IServiceProvider serviceProvider)
    {
        var applicationTypes = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(t => t.GetTypes())
            .Where(t => t.GetInterfaces().Any(t => t == typeof(ICommandHandler)));

        foreach (var type in applicationTypes)
        {
            var textCommandAttribute = type.GetCustomAttribute<CommandAttribute>();
            if (textCommandAttribute == null)
                continue;
            
            if (_commandCache.ContainsKey(textCommandAttribute.CommandName))
                throw new InvalidConstraintException($"Повторение названия команды: {textCommandAttribute.CommandName}");
            
            _commandCache.Add(textCommandAttribute.CommandName, (ICommandHandler)serviceProvider.GetService(type));
        }
    }

    public bool TryParse(string commandText, out ICommandHandler commandHandler)
    {
        var commandName = commandText.Split(' ')[0];
        return _commandCache.TryGetValue(commandName, out commandHandler);
    }
}