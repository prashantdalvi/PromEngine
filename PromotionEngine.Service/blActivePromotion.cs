using PromotionEngine.DataAccess;
using PromotionEngine.Model;
using System.Linq;

namespace PromotionEngine.Service
{
    public class blActivePromotion : IblActivePromotion
    {
        public ActivePromotion GetActivePromotion(int activePromotionId)
        {
            ActivePromotion activePromotion =
                  (from l in Global.activePromotions
                   where l.PromotionId == activePromotionId
                   select new ActivePromotion
                   {
                       PromotionId = l.PromotionId,
                       Promotions = l.Promotions,
                       ActivePromotions = l.ActivePromotions,
                       unit = l.unit,
                       SubPromotionId = l.SubPromotionId,
                   }).FirstOrDefault();

            return activePromotion;
        }

    }

    public interface IblActivePromotion
    {
        ActivePromotion GetActivePromotion(int activePromotionId);
    }
}
