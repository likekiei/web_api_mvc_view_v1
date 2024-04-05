using Main_Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.DTO.Employee
{
    /// <summary>
    /// 【查詢】客戶
    /// </summary>
    public class Employee_Filter
    {
        #region == 通用屬性 ==
        /// <summary>
        /// 頁數  [有給分頁，沒給不處理]
        /// </summary>
        [Display(Name = "頁數")]
        public int? PageNumber { get; set; }
        /// <summary>
        /// 每頁筆數 [預設10][有頁數才有用]
        /// </summary>
        [Display(Name = "每頁筆數")]
        public int? PageSize { get; set; }
        ///// <summary>
        ///// 關鍵字  [代號 or 名稱]
        ///// </summary>
        //[Display(Name = "關鍵字")]
        //public string Keyword { get; set; }
        ///// <summary>
        ///// 要忽略的項目  [代號]
        ///// </summary>
        //[Display(Name = "要忽略的項目")]
        //public string IgnoreNo { get; set; }
        ///// <summary>
        ///// 是否啟用中[null查全部]
        ///// </summary>
        //[Display(Name = "是否啟用中")]
        //public bool? Is_Enable { get; set; }
        /// <summary>
        /// 查詢開始時間
        /// </summary>
        //[Display(Name = "查詢開始時間")]
        //public DateTime? Query_Date_STA { get; set; }
        ///// <summary>
        ///// 查詢結束時間
        ///// </summary>
        //[Display(Name = "查詢結束時間")]
        //public DateTime? Query_Date_END { get; set; }
        #endregion

        /// <summary>
        /// 公司Key [不用給，預設抓登入者的公司]
        /// </summary>
        //[Display(Name = "公司Key")]
        //public long? Company_ID { get; set; }
        /// <summary>
        /// 單號
        /// </summary>
        [Display(Name = "單號")]
        public string No { get; set; }
        /// <summary>
        /// 單號關鍵字
        /// </summary>
        [Display(Name = "名稱關鍵字")]
        public string Name_Keyword { get; set; }
        ///// <summary>
        ///// 訂單編號
        ///// </summary>
        //[Display(Name = "訂單編號")]
        //public string Order_Number { get; set; }
        ///// <summary>
        ///// SO編號
        ///// </summary>
        //[Display(Name = "SO編號")]
        //public string SO_Number { get; set; }
        ///// <summary>
        ///// 櫃號
        ///// </summary>
        //[Display(Name = "櫃號")]
        //public string Cabinet_Number { get; set; }
    }
}
