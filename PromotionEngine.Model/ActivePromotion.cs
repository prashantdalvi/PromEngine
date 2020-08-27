using Newtonsoft.Json;

namespace PromotionEngine.Model
{
    public class ActivePromotion
    {
        [JsonProperty(PropertyName = "PromotionId")]
        public int PromotionId { get; set; }
        [JsonProperty(PropertyName = "ActivePromotions")]
        public string ActivePromotions { get; set; }
        [JsonProperty(PropertyName = "unit")]
        public int unit { get; set; }
        [JsonProperty(PropertyName = "Promotions")]
        public int Promotions { get; set; }
        [JsonProperty(PropertyName = "SubPromotionId")]
        public int SubPromotionId { get; set; }
    }

}
