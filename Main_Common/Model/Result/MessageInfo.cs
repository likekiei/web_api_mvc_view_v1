using Main_Common.Enum;
using Main_Common.Enum.E_StatusType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.Result
{
    /// <summary>
    /// 訊息DTO
    /// </summary>
    public class MessageInfo
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
        ///// <summary>
        ///// 例外訊息
        ///// </summary>
        //[Display(Name = "例外訊息")]
        //public List<Exception_Model> Exception_Infos { get; set; }

        //== 建構 ===================================================================

        /// <summary>
        /// 建構-初始值
        /// </summary>
        public MessageInfo()
        {

        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        public MessageInfo(bool defaultSuccess)
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
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="Focus_TXT">關注值</param>
        public MessageInfo(bool defaultSuccess, string focusTXT)
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

            this.Focus_TXT = focusTXT;
        }
    }
}
