using System.Collections.Generic;

namespace PromotionEngine.Model
{
    public class PromotionData
    {
        public List<SkuIdPrice> SkuIdPrice { get; set; }
        public List<ActivePromotion> ActivePromotion { get; set; }
    }
}
