using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.ResultApi.Order
{
    public class SalesOrderDTO
    {
        /// <summary>
        /// 銷貨單號    PS_NO
        /// </summary>
        //[Display(Name = "銷貨單號")]
        //public string sales_number { get; set; }
        /// <summary>
        /// 銷貨識別碼 範例： SA
        /// </summary>
        //[Display(Name = "銷貨識別碼")]
        //public string sales_Identifier { get; set; }
        /// <summary>
        /// 銷貨日期
        /// </summary>
        [Display(Name = "銷貨日期")]
        public DateTime sales_date { get; set; }
        ///// <summary>
        ///// 來源單據識別碼
        ///// </summary>
        //[Display(Name = "來源單據識別碼")]
        //public string source_Identifier { get; set; }
        ///// <summary>
        ///// 來源單據單號
        ///// </summary>
        //[Display(Name = "來源單據單號")]
        //public string source_number { get; set; }
        /// <summary>
        /// 客戶代號    EC005
        /// </summary>
        [Display(Name = "客戶代號")]
        public string customer_id { get; set; }
        /// <summary>
        /// 員工代號
        /// </summary>
        //[Display(Name = "員工代號")]
        //public string employee_id { get; set; }
        /// <summary>
        /// 部門代號    SD01
        /// </summary>
        [Display(Name = "部門代號")]
        public string department_id { get; set; }
        /// <summary>
        /// 立帳方式    範例： 1
        /// </summary>
        [Display(Name = "立帳方式")]
        public string set_up_account { get; set; }
        ///// <summary>
        ///// 立帳單號(ERP)    
        ///// </summary>  AR2B210001
        //[Display(Name = "立帳單號(ERP)")]
        //public string set_up_number { get; set; }
        ///// <summary>
        ///// 立帳日期 
        ///// </summary>
        //[Display(Name = "立帳日期")]
        //public DateTime set_up_date { get; set; }
        /// <summary>
        /// 交貨方式    1
        /// </summary>
        //[Display(Name = "交貨方式")]
        //public string delivery_method { get; set; }
        /// <summary>
        /// 地址    
        /// </summary>
        //[Display(Name = "地址")]
        //public string address { get; set; }
        /// <summary>
        /// 交易方式    1    
        /// </summary>
        //[Display(Name = "交易方式")]
        //public string means_of_transaction { get; set; }
        /// <summary>
        /// 交易備註    結帳期:112-11-01(pay-date); 票據到期日:112-12-01(chk_date)
        /// </summary>
        //[Display(Name = "交易備註")]
        //public string transaction_notes { get; set; }
        ///// <summary>
        ///// 付款日期    
        ///// </summary>
        //[Display(Name = "付款日期")]
        //public DateTime pay_date { get; set; }
        ///// <summary>
        ///// 付款天數    1  
        ///// </summary>
        //[Display(Name = "付款天數")]
        //public int pay_dates { get; set; }
        ///// <summary>
        ///// 票據天數    30
        ///// </summary>
        //[Display(Name = "票據天數")]
        //public int chk_dates { get; set; }
        ///// <summary>
        ///// 票據日期    
        ///// </summary>
        //[Display(Name = "票據日期")]
        //public DateTime chk_date { get; set; }
        ///// <summary>
        ///// 間隔天數    30 
        ///// </summary>
        ////[Display(Name = "間隔天數")]
        ////public int interval_dates { get; set; }
        ///// <summary>
        ///// 扣稅類別    2
        ///// </summary>
        //[Display(Name = "扣稅類別")]
        //public string tax_category { get; set; }
        ///// <summary>
        ///// 匯率   
        ///// </summary>
        //[Display(Name = "匯率")]
        //public decimal exchange_rate { get; set; }
        ///// <summary>
        ///// 輸入員    
        ///// </summary>
        //[Display(Name = "輸入員")]
        //public string input_operator { get; set; }
        ///// <summary>
        ///// 審核人   
        ///// </summary>
        //[Display(Name = "審核人")]
        //public string reviewer { get; set; }
        ///// <summary>
        ///// 列印註記   
        ///// </summary>
        //[Display(Name = "列印註記")]
        //public string print_annotations { get; set; }
        ///// <summary>
        ///// 出庫結案標誌   
        ///// </summary>
        //[Display(Name = "出庫結案標誌")]
        //public string warehouse_closing_sign { get; set; }
        ///// <summary>
        ///// 立帳結案標誌   
        ///// </summary>
        //[Display(Name = "立帳結案標誌")]
        //public string account_closing_sign { get; set; }
        ///// <summary>
        ///// 出庫補開發票標記   
        ///// </summary>
        //[Display(Name = "出庫補開發票標記")]
        //public string outbound_reissue_invoice_mark { get; set; }
        ///// <summary>
        ///// 終審日期   
        ///// </summary>
        //[Display(Name = "終審日期")]
        //public DateTime final_date { get; set; }
        ///// <summary>
        ///// 輸單日期   
        ///// </summary>
        //[Display(Name = "輸單日期")]
        //public DateTime system_date { get; set; }
        ///// <summary>
        ///// 最近修改日期   
        ///// </summary>
        //[Display(Name = "最近修改日期")]
        //public DateTime modify_date { get; set; }
        ///// <summary>
        ///// 最近修改人   
        ///// </summary>
        //[Display(Name = "最近修改人")]
        //public string modify_man { get; set; }
        /// <summary>
        /// 最備註   
        /// </summary>
        //[Display(Name = "備註")]
        //public string rem { get; set; }
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
        /// 傳票模板    /VOH_ID 02
        /// </summary>
        [Display(Name = "傳票模板")]
        public string subpoena_template { get; set; }

        #region == 發票 ==
        /// <summary>
        /// 類別  INV_NO/INV_ID
        /// </summary>
        [Display(Name = "類別")]
        public string category { get; set; }
        /// <summary>
        /// 冊序
        /// </summary>
        //[Display(Name = "冊序")]
        //public int book_order { get; set; }
        /// <summary>
        /// 發票號碼    INV_NO
        /// </summary>
        [Display(Name = "發票號碼")]
        public string invoice_number { get; set; }
        /// <summary>
        /// 發票日期    //INV_NO/INV_DD
        /// </summary>
        [Display(Name = "發票日期")]
        public DateTime invoice_date { get; set; }
        /// <summary>
        /// 申報期別    //INV_NO/INV_YM
        /// </summary>
        [Display(Name = "申報期別")]
        public int reporting_period { get; set; }
        /// <summary>
        /// 發票部門代號    //INV_NO/DEP
        /// </summary>
        [Display(Name = "發票部門代號")]
        public string invoice_department_id { get; set; }
        /// <summary>       //INV_NO/UNI_NO_BUY	
        /// 買受人統一編號
        /// </summary>
        [Display(Name = "買受人統一編號")]
        public string buyer_unified_number { get; set; }
        /// <summary>
        /// 買受人抬頭       //INV_NO/TITLE_BUY	
        /// </summary>
        [Display(Name = "買受人抬頭")]
        public string buyer { get; set; }
        ///// <summary>
        ///// 買受人發票地址  
        ///// </summary>
        //[Display(Name = "買受人發票地址")]
        //public string buyer_invoice_address { get; set; }
        /// <summary>
        /// 營業人統一編號     //INV_NO/UNI_NO_PAY
        /// </summary>
        [Display(Name = "營業人統一編號")]
        public string unified_business_number { get; set; }
        /// <summary>
        /// 營業人抬頭   //INV_NO/TITLE_PAY
        /// </summary>
        [Display(Name = "營業人抬頭")]
        public string seller { get; set; }
        /// <summary>
        /// 銷售金額    //INV_NO/AMT
        /// </summary>
        [Display(Name = "銷售金額")]
        public decimal sales_amount { get; set; }
        /// <summary>
        /// 稅別      //INV_NO/TAX_ID1		
        /// </summary>
        [Display(Name = "發票稅別")]
        public string tax_category { get; set; }
        /// <summary>
        /// 稅額      //INV_NO/TAX
        /// </summary>
        [Display(Name = "稅額")]
        public decimal tax_price { get; set; }
        /// <summary>
        /// 合計      //AMT+TAX?
        /// </summary>
        [Display(Name = "稅合計")]
        public decimal total_price { get; set; }
        //{
        //    get { return sales_amount + tax_price; }
        //    set { this.total_price = value; }
        //}
        /// <summary>
        /// 銷貨固資    //INV_NO/TAX_ID2(銷貨可扣1、銷貨不可扣2、固資可扣3、固資不可扣4)			
        /// </summary>
        [Display(Name = "銷貨固資")]
        public string tax_id { get; set; }
        /// <summary>
        /// 檢查碼     //INV_NO/CHK_CODE
        /// </summary>
        [Display(Name = "檢查碼")]
        public string checksum { get; set; }
        #endregion
     
        /// <summary>
        /// 表身項目 [要給]
        /// </summary>
        [Display(Name = "表身項目")]
        public List<SalesOrderTFDTO> TFs { get; set; }
        //== 建構 ===================================================================

        /// <summary>
        /// 【建構】
        /// </summary>
        public SalesOrderDTO()
        {
            this.TFs = new List<SalesOrderTFDTO>();
        }
    }
}
