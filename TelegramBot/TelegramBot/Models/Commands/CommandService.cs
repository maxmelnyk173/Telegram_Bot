using System.Collections.Generic;
using TelegramBot.Models.Commands;

namespace TelegramTelegramBot.Models.Commands
{
    public class CommandService: ICommandService
    {
        private readonly List<TelegramCommand> _commands;

        public CommandService()
        {
            _commands = new List<TelegramCommand>
            {
                new StartCommand(),
                new GetCurrencyCommand()
            };
        }

        public List<TelegramCommand> Get() => _commands;
    }
}