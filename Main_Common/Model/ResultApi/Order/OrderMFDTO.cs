using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.ResultApi.Order
{
    public class OrderMFDTO
    {        
        /// <summary>
        /// 銷貨日期
        /// </summary>
        [Display(Name = "銷貨日期")]
        public DateTime sale_date { get; set; }
        /// <summary>
        /// 銷貨單號
        /// </summary>
        [Display(Name = "銷貨單號")]
        public string order_number { get; set; }
        /// <summary>
        /// 銷貨客戶
        /// </summary>
        [Display(Name = "銷貨客戶")]
        public string sales_customer { get; set; }
        /// <summary>
        /// 部門
        /// </summary>
        [Display(Name = "部門")]
        public string department { get; set; }
        /// <summary>
        /// 扣稅類別
        /// </summary>
        [Display(Name = "扣稅類別")]
        public string tax_category { get; set; }
        /// <summary>
        /// 立帳
        /// </summary>
        [Display(Name = "立帳")]
        public string set_up_account { get; set; }
        /// <summary>
        /// 客戶訂單
        /// </summary>
        [Display(Name = "客戶訂單")]
        public string customer_order { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        [Display(Name = "備註")]
        public string remark { get; set; }
        /// <summary>
        /// 傳票模板
        /// </summary>
        [Display(Name = "傳票模板")]
        public string subpoena_template { get; set; }
        //INVOICE
        /// <summary>
        /// 類別
        /// </summary>
        [Display(Name = "類別")]
        public string category { get; set; }
        /// <summary>
        /// 冊序
        /// </summary>
        [Display(Name = "冊序")]
        public int book_order { get; set; }
        /// <summary>
        /// 發票號碼
        /// </summary>
        [Display(Name = "發票號碼")]
        public string invoice_number { get; set; }
        /// <summary>
        /// 發票日期
        /// </summary>
        [Display(Name = "發票日期")]
        public DateTime invoice_date { get; set; }
        /// <summary>
        /// 申報期別
        /// </summary>
        [Display(Name = "申報期別")]
        public int reporting_period { get; set; }
        /// <summary>
        /// 發票部門
        /// </summary>
        [Display(Name = "發票部門")]
        public string invoice_department { get; set; }
        /// <summary>
        /// 買受人統一編號
        /// </summary>
        [Display(Name = "買受人統一編號")]
        public string buyer_unified_number { get; set; }
        /// <summary>
        /// 買受人抬頭
        /// </summary>
        [Display(Name = "買受人抬頭")]
        public string buyer { get; set; }
        /// <summary>
        /// 買受人發票地址
        /// </summary>
        [Display(Name = "買受人發票地址")]
        public string buyer_invoice_address { get; set; }
        /// <summary>
        /// 營業人統一編號
        /// </summary>
        [Display(Name = "營業人統一編號")]
        public string unified_business_number { get; set; }
        /// <summary>
        /// 營業人抬頭
        /// </summary>
        [Display(Name = "營業人抬頭")]
        public string seller { get; set; }
        /// <summary>
        /// 銷售金額
        /// </summary>
        [Display(Name = "銷售金額")]
        public decimal sales_amount { get; set; }
        /// <summary>
        /// 稅別
        /// </summary>
        [Display(Name = "發票稅別")]
        public string tax_category1 { get; set; }
        /// <summary>
        /// 稅額
        /// </summary>
        [Display(Name = "稅額")]
        public decimal tax_price { get; set; }
        /// <summary>
        /// 合計
        /// </summary>
        [Display(Name = "稅合計")]
        public decimal total_price { get; set; }
        //{
        //    get { return sales_amount + tax_price; }
        //    set { this.total_price = value; }
        //}
        /// <summary>
        /// 費用資產
        /// </summary>
        [Display(Name = "費用資產")]
        public string tax_id2 { get; set; }
        /// <summary>
        /// 檢查碼
        /// </summary>
        [Display(Name = "檢查碼")]
        public string checksum { get; set; }

        /// <summary>
        /// 表身項目 [要給]
        /// </summary>
        [Display(Name = "表身項目")]
        public List<OrderTFDTO> TFs { get; set; }
        //== 建構 ===================================================================

        /// <summary>
        /// 【建構】
        /// </summary>
        public OrderMFDTO()
        {
            this.TFs = new List<OrderTFDTO>();
        }
    }
}
