using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.ResultApi.Order
{
    public class OrderTFDTO
    {
        /// <summary>
        /// 流水項次 [不用給][前端無該欄位，後端需自行處理][因為有表身拆Bom的問題，所以這個項次只有母件對的上，子件由後端生成]
        /// </summary>
        [Display(Name = "流水項次")]
        public int? itm { get; set; }
        /// <summary>
        /// 銷貨明細
        /// </summary>
        [Display(Name = "銷貨單號")]
        public string order_number { get; set; }
        /// <summary>
        /// 品名
        /// </summary>
        [Display(Name = "品名")]
        public string product_name { get; set; }
        /// <summary>
        /// 倉庫
        /// </summary>
        [Display(Name = "倉庫")]
        public string storehouse { get; set; }
        /// <summary>
        /// 數量
        /// </summary>
        [Display(Name = "數量")]
        public decimal quantity { get; set; }
        /// <summary>
        /// 單價
        /// </summary>
        [Display(Name = "單價")]
        public decimal unit_price { get; set; }
        /// <summary>
        /// 金額
        /// </summary>
        [Display(Name = "金額")]
        public decimal price { get; set; }
        /// <summary>
        /// 單位
        /// </summary>
        [Display(Name = "單位")]
        public string unit { get; set; }
        /// <summary>
        /// 折扣
        /// </summary>
        [Display(Name = "折扣")]
        public decimal? discount { get; set; }       
    }
}
