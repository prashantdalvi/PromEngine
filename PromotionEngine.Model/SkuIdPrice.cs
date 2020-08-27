using Newtonsoft.Json;

namespace PromotionEngine.Model
{
    public class SkuIdPrice
    {
        [JsonProperty(PropertyName = "skuid")]
        public int skuid { get; set; }
        [JsonProperty(PropertyName = "sku")]
        public string sku { get; set; }
        [JsonProperty(PropertyName = "unitprice")]
        public int unitprice { get; set; }
    }


}