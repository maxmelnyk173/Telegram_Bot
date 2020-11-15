using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;

using TelegramBot.Models.Commands;

namespace TelegramBot.Controllers
{
    [ApiController]
    [Route("api/message/update")]
    public class BotController : Controller
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly ICommandService _commandService;
        public BotController(ICommandService commandService, ITelegramBotClient telegramBotClient)
        {
            _commandService = commandService;
            _telegramBotClient = telegramBotClient;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Started");
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Update update)
        {
            if (update == null) return Ok();

            var message = update.Message;

            foreach (var command in _commandService.Get())
            {
                if (command.Contains(message))
                {
                    await command.Execute(message, _telegramBotClient);
                    break;
                }
            }
            return Ok();
        }
    }
}