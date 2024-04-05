using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.ResultApi.Order
{
    public class SalesOrderTFDTO
    {
        /// <summary>
        /// 流水項次 [不用給][前端無該欄位，後端需自行處理][因為有表身拆Bom的問題，所以這個項次只有母件對的上，子件由後端生成]
        /// </summary>
        //[Display(Name = "流水項次")]
        //public int? itm { get; set; }
        ///// <summary>
        ///// 追蹤已交數量項次(流水項次)
        ///// </summary>
        //[Display(Name = "流水項次")]
        //public int? est_itm { get; set; }
        ///// <summary>
        ///// 追蹤退回數量項次(唯一項次)
        ///// </summary>
        //[Display(Name = "追蹤退回數量項次(唯一項次)")]
        //public int? pre_itm { get; set; }
        ///// <summary>
        ///// 出入庫單項次(流水項次)
        ///// </summary>
        //[Display(Name = "出入庫單項次(流水項次)")]
        //public int? oth_itm { get; set; }
        ///// <summary>
        ///// 銷貨單號
        ///// </summary>
        //[Display(Name = "銷貨單號")]
        //public string ps_no { get; set; }
        ///// <summary>
        ///// 單據識別碼
        ///// </summary>
        //[Display(Name = "單據識別碼")]
        //public string ps_id { get; set; }
        ///// <summary>
        ///// 單據日期
        ///// </summary>
        //[Display(Name = "單據日期")]
        //public DateTime ps_dd { get; set; }
        ///// <summary>
        ///// 預交日期
        ///// </summary>
        //[Display(Name = "預交日期")]
        //public DateTime est_dd { get; set; }
        /// <summary>
        /// 來源單據識別碼
        /// </summary>
        //[Display(Name = "來源單據識別碼")]
        //public string os_id { get; set; }
        /// <summary>
        /// 來源單據單號
        /// </summary>
        //[Display(Name = "來源單據單號")]
        //public string os_no { get; set; }
        /// <summary>
        /// 貨品代號
        /// </summary>
        [Display(Name = "貨品代號")]
        public string product_id { get; set; }
        /// <summary>
        /// 貨品名稱
        /// </summary>
        [Display(Name = "貨品名稱")]
        public string product_name { get; set; }
        /// <summary>
        /// 倉庫代號
        /// </summary>
        [Display(Name = "倉庫代號")]
        public string storehouse { get; set; }
        
        ///// <summary>
        ///// 
        ///// </summary>
        //[Display(Name = "")]
        //public string prd_mark { get; set; }
        /// <summary>
        /// 單位
        /// </summary>
        [Display(Name = "單位")]
        public string unit { get; set; }
        /// <summary>
        /// 單價
        /// </summary>
        [Display(Name = "單價")]
        public decimal unit_price { get; set; }
        /// <summary>
        /// 數量
        /// </summary>
        [Display(Name = "數量")]
        public decimal quantity { get; set; }
        ///// <summary>
        ///// 數量
        ///// </summary>
        //[Display(Name = "數量")]
        //public decimal qty_ps { get; set; }
        ///// <summary>
        ///// 外幣金額
        ///// </summary>
        //[Display(Name = "外幣金額")]
        //public decimal amt { get; set; }
        /// <summary>
        /// 金額 AMT
        /// </summary>
        [Display(Name = "金額")]
        public decimal price { get; set; }
        ///// <summary>
        ///// 未稅本位幣
        ///// </summary>
        //[Display(Name = "未稅本位幣")]
        //public decimal amtn_net { get; set; }

        /// <summary>
        /// 稅額
        /// </summary>
        [Display(Name = "稅額")]
        public decimal tax { get; set; }
        /// <summary>
        /// 稅率
        /// </summary>
        //[Display(Name = "稅率")]
        //public decimal tax_rto { get; set; }

        /// <summary>
        /// 折扣
        /// </summary>
        [Display(Name = "折扣")]
        public decimal? discount { get; set; }
        ///// <summary>
        ///// 是否定價政策搭贈品   範例："F"
        ///// </summary>
        //[Display(Name = "是否定價政策搭贈品")]
        //public string free_id_def { get; set; }
        ///// <summary>
        ///// 備註
        ///// </summary>
        //[Display(Name = "備註")]
        //public string remark { get; set; }
    }
}
