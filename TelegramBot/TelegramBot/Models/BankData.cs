using Newtonsoft.Json;
using System.Collections.Generic;

namespace TelegramBot.Models
{
    public class BankData
    {
        [JsonProperty("exchangeRate")]
        public List<CurrencyData> СurrenciesData { get; set; }
    }
}
