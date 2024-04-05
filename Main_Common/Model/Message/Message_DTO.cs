using Main_Common.Enum.E_StatusType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Main_Common.Model.Message
{
    /// <summary>
    /// 【DTO】訊息
    /// </summary>
    public class Message_DTO
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [Display(Name = "是否成功")]
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 狀態碼 [預設Default]
        /// </summary>
        [Display(Name = "狀態碼")]
        public E_StatusCode E_StatusCode { get; set; }
        /// <summary>
        /// 狀態等級 (預設[警告]，避免忘記設定，造成後續判斷錯誤，故一律默認訊息為[警告]級別)
        /// </summary>
        [Display(Name = "狀態等級")]
        public E_StatusLevel E_StatusLevel { get; set; }
        /// <summary>
        /// 標題
        /// </summary>
        [Display(Name = "標題")]
        public string Title { get; set; }
        /// <summary>
        /// 訊息
        /// </summary>
        [Display(Name = "訊息")]
        public string Message { get; set; }
        /// <summary>
        /// 系統例外訊息
        /// </summary>
        [Display(Name = "系統例外訊息")]
        public string Message_Exception { get; set; }
        /// <summary>
        /// 其他訊息
        /// </summary>
        [Display(Name = "其他訊息")]
        public string Message_Other { get; set; }
        /// <summary>
        /// 關注值
        /// </summary>
        [Display(Name = "關注值")]
        public string Focus_TXT { get; set; }
        /// <summary>
        /// 次序
        /// </summary>
        [Display(Name = "次序")]
        public int SEQ { get; set; }
        /// <summary>
        /// 綁定Key
        /// </summary>
        [Display(Name = "綁定Key")]
        public Guid? Bind_Key { get; set; }
        ///// <summary>
        ///// 主要Key值
        ///// </summary>
        //[Display(Name = "主要Key值")]
        //public string Table_Key_Main { get; set; }
        ///// <summary>
        ///// 相關Key值
        ///// </summary>
        //[Display(Name = "相關Key值")]
        //public List<TableDataBind_DTO> Table_Key_Many { get; set; }
        ///// <summary>
        ///// 例外訊息
        ///// </summary>
        //[Display(Name = "例外訊息")]
        //public List<Exception_Model> Exception_Infos { get; set; }

        //== 建構 ===================================================================

        /// <summary>
        /// 建構-初始值
        /// </summary>
        public Message_DTO()
        {
            this.E_StatusLevel = E_StatusLevel.警告;
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="bindKey">綁定用Key</param>
        public Message_DTO(Guid? bindKey)
        {
            this.Bind_Key = bindKey;
            this.E_StatusLevel = E_StatusLevel.警告;
        }

        ///// <summary>
        ///// 建構-初始值
        ///// </summary>
        ///// <param name="defaultSuccess">是否預設成功</param>
        ///// <param name="eStatusLevel">狀態等級</param>
        ///// <param name="bindKey">綁定用Key</param>
        //public Message_DTO(bool defaultSuccess, E_StatusLevel eStatusLevel, Guid? bindKey)
        //{
        //    if (defaultSuccess)
        //    {
        //        this.IsSuccess = true;
        //        this.E_StatusCode = E_StatusCode.成功;
        //        this.Title = "【成功】";
        //    }
        //    else
        //    {
        //        this.IsSuccess = false;
        //        this.E_StatusCode = E_StatusCode.失敗;
        //        this.Title = "【失敗】";
        //    }

        //    this.Bind_Key = bindKey;
        //    this.E_StatusLevel = eStatusLevel;
        //}

        ///// <summary>
        ///// 建構-初始值
        ///// </summary>
        ///// <param name="defaultSuccess">是否預設成功</param>
        ///// <param name="eStatusLevel">狀態等級</param>
        ///// <param name="focusTXT">關注值</param>
        ///// <param name="bindKey">綁定用Key</param>
        //public Message_DTO(bool defaultSuccess, E_StatusLevel eStatusLevel, string focusTXT, Guid? bindKey)
        //{
        //    if (defaultSuccess)
        //    {
        //        this.IsSuccess = true;
        //        this.E_StatusCode = E_StatusCode.成功;
        //        this.Title = "【成功】";
        //    }
        //    else
        //    {
        //        this.IsSuccess = false;
        //        this.E_StatusCode = E_StatusCode.失敗;
        //        this.Title = "【失敗】";
        //    }

        //    this.Bind_Key = bindKey;
        //    this.E_StatusLevel = eStatusLevel;
        //    this.Focus_TXT = focusTXT;
        //}

        ///// <summary>
        ///// 建構-初始值
        ///// </summary>
        ///// <param name="defaultSuccess">是否預設成功</param>
        ///// <param name="eStatusLevel">狀態等級</param>
        ///// <param name="statusType">狀態碼</param>
        ///// <param name="message">訊息</param>
        ///// <param name="focusTXT">關注值</param>
        ///// <param name="bindKey">綁定用Key</param>
        //public Message_DTO(bool defaultSuccess, E_StatusLevel eStatusLevel, E_StatusCode statusType, string message, string focusTXT, Guid? bindKey)
        //{
        //    if (defaultSuccess)
        //    {
        //        this.IsSuccess = true;
        //        this.E_StatusCode = E_StatusCode.成功;
        //        this.Title = "【成功】";
        //    }
        //    else
        //    {
        //        this.IsSuccess = false;
        //        this.E_StatusCode = E_StatusCode.失敗;
        //        this.Title = "【失敗】";
        //    }

        //    this.Bind_Key = bindKey;
        //    this.E_StatusLevel = eStatusLevel;
        //    this.E_StatusCode = statusType;
        //    this.Message = message;
        //    this.Focus_TXT = focusTXT;
        //}

        ///// <summary>
        ///// 建構-初始值
        ///// </summary>
        ///// <param name="defaultSuccess">是否預設成功</param>
        ///// <param name="eStatusLevel">狀態等級</param>
        ///// <param name="statusType">狀態碼</param>
        ///// <param name="message">訊息</param>
        ///// <param name="focusTXT">關注值</param>
        ///// <param name="bindKey">綁定用Key</param>
        //public Message_DTO(bool defaultSuccess, Guid? bindKey, E_StatusLevel eStatusLevel, E_StatusCode statusType, string focusTXT, string message)
        //{
        //    if (defaultSuccess)
        //    {
        //        this.IsSuccess = true;
        //        this.E_StatusCode = E_StatusCode.成功;
        //        this.Title = "【成功】";
        //    }
        //    else
        //    {
        //        this.IsSuccess = false;
        //        this.E_StatusCode = E_StatusCode.失敗;
        //        this.Title = "【失敗】";
        //    }

        //    this.Bind_Key = bindKey;
        //    this.E_StatusLevel = eStatusLevel;
        //    this.E_StatusCode = statusType;
        //    this.Message = message;
        //    this.Focus_TXT = focusTXT;
        //}

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="bindKey">綁定用Key</param>
        /// <param name="eStatusLevel">狀態等級(沒給默認警告)</param>
        /// <param name="statusType">狀態碼</param>
        /// <param name="focusTXT">關注值</param>
        public Message_DTO(bool defaultSuccess, Guid? bindKey, E_StatusLevel? eStatusLevel, E_StatusCode? statusType, string focusTXT)
        {
            if (defaultSuccess)
            {
                this.IsSuccess = true;
                this.E_StatusCode = E_StatusCode.成功;
                this.Title = "【成功】";
            }
            else
            {
                this.IsSuccess = false;
                this.E_StatusCode = E_StatusCode.失敗;
                this.Title = "【失敗】";
            }

            this.Bind_Key = bindKey;
            this.E_StatusLevel = eStatusLevel ?? E_StatusLevel.警告;
            this.E_StatusCode = statusType ?? this.E_StatusCode;
            this.Focus_TXT = focusTXT;
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="bindKey">綁定用Key</param>
        /// <param name="eStatusLevel">狀態等級(沒給默認警告)</param>
        /// <param name="statusType">狀態碼</param>
        /// <param name="focusTXT">關注值</param>
        /// <param name="title">標題</param>
        /// <param name="message">訊息</param>
        public Message_DTO(bool defaultSuccess, Guid? bindKey, E_StatusLevel? eStatusLevel, E_StatusCode? statusType, string focusTXT, string title, string message)
        {
            if (defaultSuccess)
            {
                this.IsSuccess = true;
                this.E_StatusCode = E_StatusCode.成功;
                this.Title = "【成功】";
            }
            else
            {
                this.IsSuccess = false;
                this.E_StatusCode = E_StatusCode.失敗;
                this.Title = "【失敗】";
            }

            this.Bind_Key = bindKey;
            this.E_StatusLevel = eStatusLevel ?? E_StatusLevel.警告;
            this.E_StatusCode = statusType ?? this.E_StatusCode;
            this.Title = string.IsNullOrEmpty(title) ? this.Title : title;
            this.Message = message;
            this.Focus_TXT = focusTXT;
        }
    }
}
