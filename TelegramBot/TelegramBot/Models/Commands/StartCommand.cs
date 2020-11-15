using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBot.Models.Commands
{
    public class StartCommand : TelegramCommand
    {
        public override string Name => @"/start";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }

        public override async Task Execute(Message message, ITelegramBotClient botClient)
        {
            var chatId = message.Chat.Id;

            await botClient.SendTextMessageAsync(chatId, "Hello User, " +
                                                         "\nTo get the exchange rate, please enter a command /info and enter the currency and date with spaces " +
                                                         "\n(ex: /info USD 25.10.2020).",   
                                                         parseMode: ParseMode.Html, false, false, 0);
        }
    }
}