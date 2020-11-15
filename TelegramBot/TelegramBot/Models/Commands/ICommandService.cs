using System.Collections.Generic;

namespace TelegramBot.Models.Commands
{ 
    public interface ICommandService
    {
        List<TelegramCommand> Get();
    }
}