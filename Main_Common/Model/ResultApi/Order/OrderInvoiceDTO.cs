using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.ResultApi.Order
{
    public class OrderInvoiceDTO
    {
        /// <summary>
        /// 銷貨發票
        /// </summary>
        [Display(Name = "銷貨單號")]
        public string order_number { get; set; }
        /// <summary>
        /// 類別
        /// </summary>
        [Display(Name = "類別")]
        public string category { get; set; }
        /// <summary>
        /// 冊序
        /// </summary>
        [Display(Name = "冊序")]
        public string book_order { get; set; }
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
        public string reporting_period { get; set; }
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
        /// 銷項金額
        /// </summary>
        [Display(Name = "銷項金額")]
        public decimal sales_amount { get; set; }
        /// <summary>
        /// 稅別
        /// </summary>
        [Display(Name = "稅別")]
        public decimal tax_category { get; set; }
        /// <summary>
        /// 稅額
        /// </summary>
        [Display(Name = "稅額")]
        public decimal tax_price { get; set; }
        /// <summary>
        /// 合計
        /// </summary>
        [Display(Name = "合計")]
        public decimal total_price { get; set; }
        /// <summary>
        /// 銷貨費用
        /// </summary>
        [Display(Name = "銷貨費用")]
        public bool sale_cost { get; set; }
        /// <summary>
        /// 固定資產
        /// </summary>
        [Display(Name = "固定資產")]
        public string fixed_assets { get; set; }
        /// <summary>
        /// 檢查碼
        /// </summary>
        [Display(Name = "檢查碼")]
        public int checksum { get; set; }
    }
}
