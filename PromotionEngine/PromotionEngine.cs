using Microsoft.Extensions.Options;
using PromotionEngine.DataAccess;
using PromotionEngine.Model;
using PromotionEngine.Service;
using PromotionEngine.Utility;
using System;
using System.IO;
using System.Windows.Forms;

namespace PromotionEngine
{
    public partial class PromotionEngine : Form
    {

        private readonly IServiceProvider serviceProvider;
        private readonly IblActivePromotion blActivePromotion;
        private readonly AppSettings settings;
        private PromotionData promotionData = new PromotionData();
        string SkuIddatapath = Path.Combine(Environment.CurrentDirectory, "SkuIddata.json");

        public PromotionEngine(
            IServiceProvider serviceProvider,
                IblActivePromotion blActivePromotion,
                IOptions<AppSettings> settings
            )
        {
            InitializeComponent();

            this.serviceProvider = serviceProvider;
            this.blActivePromotion = blActivePromotion;
            this.settings = settings.Value;

            try
            {
                string path = Path.Combine(Environment.CurrentDirectory, "SkuIdPrice.json");
                JsonParser.JsonParserToList(path, promotionData);
                if (File.Exists(SkuIddatapath))
                    File.Delete(SkuIddatapath);
            }
            catch (Exception ex)
            {
                Logger.WriteException(ex);
            }
            BindData();
        }

        private void BindData()
        {
            cmbSKUIDPrice.DataSource = Global.skuIdPrice;
            cmbSKUIDPrice.ValueMember = "skuid";
            cmbSKUIDPrice.DisplayMember = "sku";

            cmbActivePromotion.DataSource = Global.activePromotions;
            cmbActivePromotion.ValueMember = "PromotionId";
            cmbActivePromotion.DisplayMember = "ActivePromotions";
        }
        private void btnResult_Click(object sender, EventArgs e)
        {
            lblResult.Text = "Result is:" + calculatePromotion().ToString();

            Global.resultSkuIdPrice = JsonParser.JsonParserToActivePromotion(SkuIddatapath);

            dataGridView1.DataSource = Global.resultSkuIdPrice;

            lblResult.Text = "Result is:" + TotalPrice();
        }

        private int calculatePromotion()
        {
            int calculateValue = 0;

            try
            {
                int activePromotionId = Convert.ToInt32(cmbActivePromotion.SelectedValue == null ? "0" : cmbActivePromotion.SelectedValue);
                int skuId = Convert.ToInt32(cmbSKUIDPrice.SelectedValue == null ? "0" : cmbSKUIDPrice.SelectedValue);

                CalculateResult calculateResult = new CalculateResult();
                calculateValue = calculateResult.CalculateFinalResult(skuId, activePromotionId, Convert.ToInt32(txtUnit.Text), SkuIddatapath);

            }
            catch (Exception ex)
            {
                Logger.WriteException(ex);
            }
            return calculateValue;
        }

        private int TotalPrice()
        {
            int totalPrice = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells.Count > 0 && row.Cells["unitprice"].ToString().Trim() != "0")
                    totalPrice = totalPrice + Convert.ToInt32(row.Cells["unitprice"].Value.ToString());

            }

            return totalPrice;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

            if (File.Exists(SkuIddatapath))
                File.Delete(SkuIddatapath);

            dataGridView1.DataSource = null;


            lblResult.Text = "Result is:";
        }
    }
}
