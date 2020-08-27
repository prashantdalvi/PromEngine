using Newtonsoft.Json;
using PromotionEngine.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PromotionEngine.DataAccess
{
    public static class JsonParser
    {
        public static void JsonParserToList(string filePath, PromotionData promotionData)
        {
            try
            {
                using (StreamReader r = new StreamReader(filePath))
                {
                    string json = r.ReadToEnd();
                    promotionData = Newtonsoft.Json.JsonConvert.DeserializeObject<PromotionData>(json);
                    Global.activePromotions = promotionData.ActivePromotion.ToList();
                    Global.skuIdPrice = promotionData.SkuIdPrice.ToList();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public static List<SkuIdPrice> JsonParserToActivePromotion(string filePath)
        {
            List<SkuIdPrice> skuIdPrice = new List<SkuIdPrice>();
            try
            {
                using (StreamReader r = new StreamReader(filePath))
                {
                    string json = r.ReadToEnd();
                    skuIdPrice = JsonConvert.DeserializeObject<List<SkuIdPrice>>(json);
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            return skuIdPrice;
        }
    }
}
