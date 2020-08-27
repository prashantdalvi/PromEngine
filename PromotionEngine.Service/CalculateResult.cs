using Newtonsoft.Json;
using PromotionEngine.DataAccess;
using PromotionEngine.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PromotionEngine.Service
{
    public class CalculateResult
    {
        blActivePromotion blActivePromotion = new blActivePromotion();
        public int CalculateFinalResult(int skuId, int promptioid, int orderUnit, string filePath)
        {
            int calculateValue = 0;
            try
            {
                ActivePromotion activePromotion = blActivePromotion.GetActivePromotion(promptioid);

                blSkuIdPrice blSkuIdPrice = new blSkuIdPrice();
                SkuIdPrice skuidPrice = blSkuIdPrice.GetSkuIdPrice(skuId);
                int finalUnitPrice = 0;
                if (activePromotion.SubPromotionId != 0)
                {
                    if (Global.resultSkuIdPrice != null)
                    {
                        if (Global.resultSkuIdPrice.Exists(x => x.skuid == activePromotion.SubPromotionId))
                        {
                            finalUnitPrice = activePromotion.Promotions;
                            skuidPrice.unitprice = finalUnitPrice;
                            UpdateJson(activePromotion.SubPromotionId, 0, filePath);
                            AddDataToJson(skuidPrice, filePath);
                            return finalUnitPrice;
                        }
                    }
                    finalUnitPrice = skuidPrice.unitprice;
                }
                else
                {
                    finalUnitPrice = activePromotion.unit;
                }

                int remainingUnit = orderUnit % finalUnitPrice * skuidPrice.unitprice;
                int groupedUnit = orderUnit / finalUnitPrice * activePromotion.Promotions;
                calculateValue = remainingUnit + groupedUnit;
                skuidPrice.unitprice = calculateValue;
                AddDataToJson(skuidPrice, filePath);

            }
            catch (System.Exception)
            {
                throw;
            }

            return calculateValue;
        }

        public void AddDataToJson(SkuIdPrice skuIdPrice, string filePath)
        {
            string jsonactive = JsonConvert.SerializeObject(skuIdPrice);
            bool blnpath = false;

            if (!File.Exists(filePath))
            {
                blnpath = true;
            }
            else
            {
                string text = File.ReadAllText(filePath);
                text = text.Replace("]", ",");
                File.WriteAllText(filePath, text);
            }

            using (var tw = new StreamWriter(filePath, true))
            {
                if (blnpath)
                {
                    jsonactive = "[" + jsonactive + "]";
                }
                else
                {
                    jsonactive = jsonactive + "]";
                }
                tw.WriteLine(jsonactive.ToString());
                tw.Close();
            }
        }
        private void UpdateJson(int skuid, int finalUnit, string filePath)
        {
            List<SkuIdPrice> skuIdPrice = new List<SkuIdPrice>();

            try
            {
                string json = File.ReadAllText(filePath);

                using (StreamReader r = new StreamReader(filePath))
                {
                    json = r.ReadToEnd();
                    skuIdPrice = JsonConvert.DeserializeObject<List<SkuIdPrice>>(json);
                }
                var query = (from stud in skuIdPrice
                             where stud.skuid == skuid
                             select stud)
                        .Update(st => { st.unitprice = 0; });

                string output = Newtonsoft.Json.JsonConvert.SerializeObject(skuIdPrice, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, output);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Update Error : " + ex.Message.ToString());
            }
        }
    }
    public static class UpdateExtensions
    {
        public delegate void Func<TArg0>(TArg0 element);

        public static int Update<TSource>(this IEnumerable<TSource> source, Func<TSource> update)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (update == null) throw new ArgumentNullException("update");
            if (typeof(TSource).IsValueType)
                throw new NotSupportedException("value type elements are not supported by update.");

            int count = 0;
            foreach (TSource element in source)
            {
                update(element);
                count++;
            }
            return count;
        }
    }
}
