using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.ERP.DTO
{
    /// <summary>
    /// 【輸入】客戶
    /// </summary>
    public class Cust_Input
    {
        /// <summary>
        /// 訂單編號
        /// </summary>
        [Display(Name = "訂單編號")]
        public string OrderNumber { get; set; }
        /// <summary>
        /// 客戶代號
        /// </summary>
        [StringLength(12)]
        [Display(Name = "客戶代號")]
        public string Cust_No { get; set; }
        /// <summary>
        /// 客戶名稱
        /// </summary>
        [StringLength(100)]
        [Display(Name = "客戶名稱")]
        public string Cust_Name { get; set; }
        /// <summary>
        /// 客戶名稱1
        /// </summary>
        [Display(Name = "客戶名稱1")]
        public string Cust_Name_1 { get; set; }
        /// <summary>
        /// 部門代號
        /// </summary>
        [Display(Name = "部門代號")]
        public string Dept_No { get; set; }
        /// <summary>
        /// 公司名稱 (放公司地址欄位)
        /// </summary>
        [Display(Name = "公司名稱")]
        public string Company_Name { get; set; }
        /// <summary>
        /// 連絡電話
        /// </summary>
        [StringLength(100)]
        [Display(Name = "連絡電話")]
        public string Phone_Number { get; set; }
        /// <summary>
        /// 統一編號
        /// </summary>
        [StringLength(20)]
        [Display(Name = "統一編號")]
        public string Uniform_Number { get; set; }
        /// <summary>
        /// 發票地址
        /// </summary>
        [Display(Name = "發票地址")]
        public string Invoice_Address { get; set; }
        /// <summary>
        /// 遷入地址 (放備註欄位)
        /// </summary>
        [Display(Name = "遷入地址")]
        public string MoveIn_Address { get; set; }
        /// <summary>
        /// 遷出地址 (放備註欄位)
        /// </summary>
        [Display(Name = "遷出地址")]
        public string MoveOut_Address { get; set; }
        /// <summary>
        /// 配送地址 (放備註欄位)
        /// </summary>
        [Display(Name = "配送地址")]
        public string Deliver_Address { get; set; }
        /// <summary>
        /// 版次 (放備註欄位)
        /// </summary>
        [Display(Name = "版次")]
        public int Version { get; set; }
        ///// <summary>
        ///// 當前時間
        ///// </summary>
        //[Display(Name = "當前時間")]
        //public DateTime? Today { get; set; }
    }
}
