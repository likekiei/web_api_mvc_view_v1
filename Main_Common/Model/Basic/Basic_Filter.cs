using Main_Common.Enum.E_ProjectType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Main_Common.Model.Basic
{
    /// <summary>
    /// 【查詢】基本屬性
    /// </summary>
    /// <remarks>盡量別改動</remarks>
    public class Basic_Filter
    {
        #region == private的屬性 ===============================================================================
        ///// <summary>
        ///// 視力
        ///// </summary>
        //[Display(Name = "視力")]
        //private decimal? _Eyesight { get; set; }
        //private decimal? _AA { get; set; }
        #endregion

        #region == 主要屬性 ===============================================================================
        /// <summary>
        /// 頁數  [有給分頁，沒給不處理]
        /// </summary>
        [Display(Name = "頁數")]
        public int? PageNumber { get; set; }
        /// <summary>
        /// 每頁筆數
        /// </summary>
        [Display(Name = "每頁筆數")]
        public int? PageSize { get; set; }

        /// <summary>
        /// 關鍵字
        /// </summary>
        [Display(Name = "關鍵字")]
        public string? Keyword { get; set; }
        /// <summary>
        /// 要忽略的代號
        /// </summary>
        [Display(Name = "要忽略的代號")]
        public string? IgnoreNo { get; set; }
        /// <summary>
        /// 是否啟用中  [null查全部]
        /// </summary>
        [Display(Name = "是否啟用中")]
        public bool? IsEnable { get; set; }
        /// <summary>
        /// 是否為預設選取項目
        /// </summary>
        [Display(Name = "是否為預設選取項目")]
        public bool? IsDefaultSelected { get; set; }
        /// <summary>
        /// 公司Key
        /// </summary>
        [Display(Name = "公司Key")]
        public long? CompanyId { get; set; }
        /// <summary>
        /// 是否允許查詢全部公司
        /// </summary>
        [Display(Name = "是否允許查詢全部公司")]
        public bool IsAllowQueryAllCompany { get; set; }
        /// <summary>
        /// 查詢開始時間
        /// </summary>
        [Display(Name = "查詢開始時間")]
        public DateTime? QueryDateSTA { get; set; }
        /// <summary>
        /// 查詢開始時間 [驗證]
        /// </summary>
        [Required(ErrorMessage = "[必填]查詢開始時間")]
        [Display(Name = "查詢開始時間")]
        public DateTime QueryDateSTA_Valid
        {
            get { return this.QueryDateSTA.GetValueOrDefault(); }
            set { this.QueryDateSTA = value; }
        }
        /// <summary>
        /// 查詢結束時間
        /// </summary>
        [Display(Name = "查詢結束時間")]
        public DateTime? QueryDateEND { get; set; }
        /// <summary>
        /// 查詢結束時間 [驗證]
        /// </summary>
        [Required(ErrorMessage = "[必填]查詢結束時間")]
        [Display(Name = "查詢結束時間")]
        public DateTime QueryDateEND_Valid
        {
            get { return this.QueryDateEND.GetValueOrDefault(); }
            set { this.QueryDateEND = value; }
        }
        #endregion

        #region == 其他屬性 ===============================================================================
        /// <summary>
        /// 綁定Key (用來將本次執行相關的資料串起來)
        /// </summary>
        [Display(Name = "綁定Key")]
        public Guid BindKey { get; set; }
        /// <summary>
        /// 是否繼續執行
        /// </summary>
        [Display(Name = "是否繼續執行")]
        public bool IsContinueEXE { get; set; }
        #endregion

        #region == 建構 ===============================================================================
        /// <summary>
        /// 建構-初始值
        /// </summary>
        public Basic_Filter()
        {
            this.IsContinueEXE = true;
        }
        #endregion
    }
}
