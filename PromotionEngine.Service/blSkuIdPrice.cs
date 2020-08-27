using PromotionEngine.DataAccess;
using PromotionEngine.Model;
using System.Linq;

namespace PromotionEngine.Service
{
    public class blSkuIdPrice : IblSkuIdPrice
    {
        public SkuIdPrice GetSkuIdPrice(int skuId)
        {
            SkuIdPrice skuidPrice =
                   (from l in Global.skuIdPrice
                    where l.skuid == skuId
                    select new SkuIdPrice
                    {
                        skuid = l.skuid,
                        sku = l.sku,
                        unitprice = l.unitprice,
                    }).FirstOrDefault();

            return skuidPrice;
        }

    }

    public interface IblSkuIdPrice
    {
        SkuIdPrice GetSkuIdPrice(int skuId);
    }
}
