using Main_Common.Enum;
using Main_Common.Enum.E_StatusType;
using Main_Common.Model.Handle.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.Result
{
    /// <summary>
    /// 【Model】訊息
    /// </summary>
    public class Message_Model
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
        /// 狀態碼名稱
        /// </summary>
        [Display(Name = "狀態碼名稱")]
        public string E_StatusCode_Name
        {
            get { return this.E_StatusCode.ToString(); }
        }
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
        /// 綁定Key
        /// </summary>
        [Display(Name = "綁定Key")]
        public Guid? Bind_Key { get; set; }
        /// <summary>
        /// 主要Key值
        /// </summary>
        [Display(Name = "主要Key值")]
        public string Table_Key_Main { get; set; }
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
        public Message_Model()
        {

        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="bindKey">綁定用Key</param>
        public Message_Model(Guid? bindKey)
        {
            this.Bind_Key = bindKey;
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="bindKey">綁定用Key</param>
        public Message_Model(bool defaultSuccess, Guid? bindKey)
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
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="focusTXT">關注值</param>
        /// <param name="bindKey">綁定用Key</param>
        public Message_Model(bool defaultSuccess, string focusTXT, Guid? bindKey)
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
            this.Focus_TXT = focusTXT;
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="statusType">狀態碼</param>
        /// <param name="message">訊息</param>
        /// <param name="focusTXT">關注值</param>
        /// <param name="bindKey">綁定用Key</param>
        public Message_Model(bool defaultSuccess, E_StatusCode statusType, string message, string focusTXT, Guid? bindKey)
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
            this.E_StatusCode = statusType;
            this.Message = message;
            this.Focus_TXT = focusTXT;
        }
    }
}
