using PromotionEngine.DataAccess;
using PromotionEngine.Model;
using PromotionEngine.Service;
using System;
using System.IO;
using Xunit;

namespace PromotionEngine.Test
{
    public class PromotionEngine
    {
        PromotionData promotionData = new PromotionData();
        string path = Path.Combine(Environment.CurrentDirectory, "SkuIdPrice.json");
        string SkuIddatapath = Path.Combine(Environment.CurrentDirectory, "SkuIddata.json");
        CalculateResult calculateResult = new CalculateResult();

        [Fact]
        public void ScenarioAATest()
        {
            JsonParser.JsonParserToList(path, promotionData);
            Assert.Equal(50, calculateResult.CalculateFinalResult(1, 1, 1, SkuIddatapath));
        }
        [Fact]
        public void ScenarioABTest()
        {
            JsonParser.JsonParserToList(path, promotionData);
            Assert.Equal(30, calculateResult.CalculateFinalResult(2, 2, 1, SkuIddatapath));
        }
        [Fact]
        public void ScenarioACTest()
        {
            JsonParser.JsonParserToList(path, promotionData);
            Assert.Equal(20, calculateResult.CalculateFinalResult(3, 3, 1, SkuIddatapath));
        }
        [Fact]
        public void ScenarioBATest()
        {
            JsonParser.JsonParserToList(path, promotionData);
            Assert.Equal(230, calculateResult.CalculateFinalResult(1, 1, 5, SkuIddatapath));
        }
        [Fact]
        public void ScenarioBBTest()
        {
            JsonParser.JsonParserToList(path, promotionData);
            Assert.Equal(120, calculateResult.CalculateFinalResult(2, 2, 5, SkuIddatapath));
        }
        [Fact]
        public void ScenarioBCTest()
        {
            JsonParser.JsonParserToList(path, promotionData);
            Assert.Equal(20, calculateResult.CalculateFinalResult(3, 3, 1, SkuIddatapath));
        }
        [Fact]
        public void ScenarioCATest()
        {
            JsonParser.JsonParserToList(path, promotionData);
            Assert.Equal(130, calculateResult.CalculateFinalResult(1, 1, 3, SkuIddatapath));
        }
        [Fact]
        public void ScenarioCBTest()
        {
            JsonParser.JsonParserToList(path, promotionData);
            Assert.Equal(120, calculateResult.CalculateFinalResult(2, 2, 5, SkuIddatapath));
        }
        [Fact]
        public void ScenarioCCTest()
        {
            if (File.Exists(SkuIddatapath))
                File.Delete(SkuIddatapath);
            JsonParser.JsonParserToList(path, promotionData);
            SkuIdPrice skuidPrice = new SkuIdPrice();
            blSkuIdPrice skuIdPrice = new blSkuIdPrice();

            skuidPrice = skuIdPrice.GetSkuIdPrice(3);
            calculateResult.AddDataToJson(skuidPrice, SkuIddatapath);


            Global.resultSkuIdPrice = JsonParser.JsonParserToActivePromotion(SkuIddatapath);

            Assert.Equal(30, calculateResult.CalculateFinalResult(4, 4, 1, SkuIddatapath));
        }
    }
}
