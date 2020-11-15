using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBot.Models.Commands
{
    public class GetCurrencyCommand : TelegramCommand
    {
        public override string Name => @"/info";
        
        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }
        
        public override async Task Execute(Message message, ITelegramBotClient botClient)
        {
            var chatId = message.Chat.Id;
            
            var keys = message.Text.Split(" ");

            if (keys.Length != 3)
            {
                await botClient.SendTextMessageAsync(chatId, "Invalid input! Please enter the data in the format  - /info currency dd.mm.yyyy", parseMode: ParseMode.Html, false, false, 0);
            }
            else
            {
                var keyDate = (keys.GetValue(2)).ToString();
                
                var keyCurrency = (keys.GetValue(1)).ToString();

                if (IsDateTime(keyDate) != true)
                {
                    await botClient.SendTextMessageAsync(chatId, "Invalid date! Please enter the data in the format - dd.mm.yyyy", parseMode: ParseMode.Html, false, false, 0);
                }
                else
                {
                    var url = "https://api.privatbank.ua/p24api/exchange_rates?json&date=" + keyDate;

                    var jsonData = string.Empty;
                    try
                    {
                        jsonData = new WebClient().DownloadString(url);
                    }
                    catch (Exception)
                    {
                        await botClient.SendTextMessageAsync(chatId, "Invalid input! Please enter the data in the format - /info currency dd.mm.yyyy", parseMode: ParseMode.Html, false, false, 0);
                    }

                    var result = JsonConvert.DeserializeObject<BankData>(jsonData);

                    var matchData = result.СurrenciesData.FirstOrDefault(p => p.ForeighnCurrency == keyCurrency);

                    if (matchData != null)
                    {
                        var sb = new StringBuilder();

                        sb.Append("Base currency: " + matchData.BaseCurrency +
                                  "\nForeign currency: " + matchData.ForeighnCurrency +
                                  "\nSale Rate: " + matchData.SaleRate +
                                  "\nPurchase Rate: " + matchData.SaleRate);

                        await botClient.SendTextMessageAsync(chatId, sb.ToString(), parseMode: ParseMode.Html, false, false, 0);
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(chatId, "Invalid currency or no data on it", parseMode: ParseMode.Html, false, false, 0);
                    }
                }
            }
        }

        private static bool IsDateTime(string messageDate)
        {
            return DateTime.TryParseExact(messageDate, "dd'.'MM'.'yyyy",
                                          CultureInfo.InvariantCulture,
                                          DateTimeStyles.None, out _);
        }
    }
}
