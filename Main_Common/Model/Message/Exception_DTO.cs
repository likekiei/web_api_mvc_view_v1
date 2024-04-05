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
    /// 【DTO】例外訊息
    /// </summary>
    public class Exception_DTO
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
        /// 查找值
        /// </summary>
        [Display(Name = "查找值")]
        public string QueryVal { get; set; }
        /// <summary>
        /// 問題值
        /// </summary>
        [Display(Name = "問題值")]
        public string ProblemVal { get; set; }
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

        //== 建構 ===================================================================

        /// <summary>
        /// 建構-初始值
        /// </summary>
        public Exception_DTO()
        {

        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="QueryVal">查詢值</param>
        public Exception_DTO(bool defaultSuccess, string queryVal)
        {
            if (defaultSuccess)
            {
                this.IsSuccess = true;
                this.E_StatusCode = E_StatusCode.成功;
            }
            else
            {
                this.IsSuccess = false;
                this.E_StatusCode = E_StatusCode.失敗;
            }

            this.QueryVal = queryVal;
        }
    }
}
