using Main_Common.Enum.E_ProjectType;
using Main_Common.Enum.E_StatusType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_EF.Table
{
    /// <summary>
    /// Log紀錄
    /// </summary>
    public class ErrorLog
    {
        /// <summary>
        /// 主Key
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "主Key")]
        public long Id { get; set; }
        /// <summary>
        /// 紀錄時間
        /// </summary>
        [Display(Name = "紀錄時間")]
        public DateTime LogDate { get; set; }

        #region == Log狀態 ==
        /// <summary>
        /// 成功否
        /// </summary>
        [Display(Name = "成功否")]
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 狀態碼Id
        /// </summary>
        //[Display(Name = "狀態碼Id")]
        //public E_StatusCode StatusCodeId { get; set; }
        /// <summary>
        /// 狀態碼名稱
        /// </summary>
        [Display(Name = "狀態碼名稱")]
        public string? StatusCodeName { get; set; }
        /// <summary>
        /// Log種類Id
        /// </summary>
        //[Display(Name = "Log種類Id")]
        //public E_LogType LogTypeId { get; set; }
        /// <summary>
        /// Log種類名稱
        /// </summary>
        [Display(Name = "Log種類名稱")]
        public string? LogTypeName { get; set; }
        /// <summary>
        /// 動作種類Id
        /// </summary>
        //[Display(Name = "動作種類Id")]
        //public E_Action ActionId { get; set; }
        /// <summary>
        /// 動作種類名稱
        /// </summary>
        [Display(Name = "動作種類名稱")]
        public string? ActionName { get; set; }
        #endregion

        #region == Log訊息 ==
        /// <summary>
        /// KeyWord
        /// </summary>
        [Display(Name = "KeyWord")]
        public string? KeyWord { get; set; }
        /// <summary>
        /// Log訊息
        /// </summary>
        [Display(Name = "Log訊息")]
        public string? Message { get; set; }
        /// <summary>
        /// 系統例外訊息
        /// </summary>
        //[Display(Name = "系統例外訊息")]
        //public string? MessageException { get; set; }
        /// <summary>
        /// 其他訊息
        /// </summary>
        //[Display(Name = "其他訊息")]
        //public string? MessageOther { get; set; }
        /// <summary>
        /// 方法路徑 [Library/Controller/Action]
        /// </summary>
        [Display(Name = "方法路徑")]
        public string? FunctionPath { get; set; }
        ///// <summary>
        ///// Table種類Id
        ///// </summary>
        //[Display(Name = "Table種類Id")]
        //public E_DBTable DBTableId { get; set; }
        ///// <summary>
        ///// Table種類_名稱
        ///// </summary>
        //[Display(Name = "Table種類_名稱")]
        //public string? DBTableName { get; set; }
        ///// <summary>
        ///// 主要Key值
        ///// </summary>
        //[Display(Name = "主要Key值")]
        //public string? TableKey_Main { get; set; }
        #endregion

        #region == Log資料 ==
        ///// <summary>
        ///// 請求資料-類別名稱
        ///// </summary>
        //[Display(Name = "請求資料-類別名稱")]
        //public string? QueryClassName { get; set; }
        /// <summary>
        /// 請求資料-Json
        /// </summary>
        [Display(Name = "請求資料-Json")]
        public string? QueryJson { get; set; }
        ///// <summary>
        ///// 回傳資料-類別名稱
        ///// </summary>
        //[Display(Name = "回傳資料-類別名稱")]
        //public string? ResultClassName { get; set; }
        /// <summary>
        /// 回傳資料-Json
        /// </summary>
        [Display(Name = "回傳資料-Json")]
        public string? ResultJson { get; set; }
        #endregion

        /// <summary>
        /// 備註
        /// </summary>
        [Display(Name = "備註")]
        public string? Rem { get; set; }
        ///// <summary>
        ///// 次序
        ///// </summary>
        //[Display(Name = "次序")]
        //public int SEQ { get; set; }
        ///// <summary>
        ///// 群組綁定Key (用來將本次執行相關資料的Log紀錄串起來)
        ///// </summary>
        //[Display(Name = "群組綁定Key")]
        //public Guid? BindKey { get; set; }
        ///// <summary>
        ///// 執行綁定Key (用來將本次執行的Log紀錄串起來)
        ///// </summary>
        //[Display(Name = "執行綁定Key")]
        //public Guid? BindKey_ByAction { get; set; }
        ///// <summary>
        ///// 公司Id
        ///// </summary>
        //[Display(Name = "公司Id")]
        //public long? CompanyId { get; set; }
        /// <summary>
        /// 使用者Id
        /// </summary>
        [Display(Name = "使用者Id")]
        public long? UserId { get; set; }
        ///// <summary>
        ///// 其它
        ///// </summary>
        //[Display(Name = "其它")]
        //public string? Other { get; set; }
    }
}
