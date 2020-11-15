using Newtonsoft.Json;

namespace TelegramBot.Models
{
    public class CurrencyData
    {
        [JsonProperty("baseCurrency")]
        public string BaseCurrency { get; set; }

        [JsonProperty("currency")]
        public string ForeighnCurrency { get; set; }

        [JsonProperty("saleRateNB")]
        public double SaleRate { get; set; }

        [JsonProperty("purchaseRateNB")]
        public double PurchaseRate { get; set; }
    }
}
