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
    /// 【DTO】Excel訊息
    /// </summary>
    public class ExcelMessage_DTO
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [Display(Name = "是否成功")]
        public bool IsSuccess { get; set; }
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
        /// Row索引 (對應Excel上存放訊息的儲存格位置)
        /// </summary>
        [Display(Name = "Row索引")]
        public int? Row_Index { get; set; }
        /// <summary>
        /// Cel索引 (對應Excel上存放訊息的儲存格位置)
        /// </summary>
        [Display(Name = "Cel索引")]
        public int? Cel_Index { get; set; }
        /// <summary>
        /// 綁定Key
        /// </summary>
        [Display(Name = "綁定Key")]
        public Guid? Bind_Key { get; set; }

        //== 建構 ===================================================================

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="bindKey">綁定用Key</param>
        public ExcelMessage_DTO()
        {

        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="bindKey">綁定用Key</param>
        public ExcelMessage_DTO(bool defaultSuccess)
        {
            if (defaultSuccess)
            {
                this.IsSuccess = true;
            }
            else
            {
                this.IsSuccess = false;
            }
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="bindKey">綁定用Key</param>
        public ExcelMessage_DTO(bool defaultSuccess, Guid? bindKey)
        {
            if (defaultSuccess)
            {
                this.IsSuccess = true;
            }
            else
            {
                this.IsSuccess = false;
            }

            this.Bind_Key = bindKey;
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="message"></param>
        /// <param name="bindKey">綁定用Key</param>
        public ExcelMessage_DTO(bool defaultSuccess, string message, Guid? bindKey)
        {
            if (defaultSuccess)
            {
                this.IsSuccess = true;
            }
            else
            {
                this.IsSuccess = false;
            }

            this.Message = message;
            this.Bind_Key = bindKey;
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="rowIndex"></param>
        /// <param name="celIndex"></param>
        /// <param name="bindKey">綁定用Key</param>
        public ExcelMessage_DTO(bool defaultSuccess, int rowIndex, int celIndex, Guid? bindKey)
        {
            if (defaultSuccess)
            {
                this.IsSuccess = true;
            }
            else
            {
                this.IsSuccess = false;
            }

            this.Row_Index = rowIndex;
            this.Cel_Index = celIndex;
            this.Bind_Key = bindKey;
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="message"></param>
        /// <param name="rowIndex"></param>
        /// <param name="celIndex"></param>
        /// <param name="bindKey">綁定用Key</param>
        public ExcelMessage_DTO(bool defaultSuccess, string message, int rowIndex, int celIndex, Guid? bindKey)
        {
            if (defaultSuccess)
            {
                this.IsSuccess = true;
            }
            else
            {
                this.IsSuccess = false;
            }

            this.Message = message;
            this.Row_Index = rowIndex;
            this.Cel_Index = celIndex;
            this.Bind_Key = bindKey;
        }
    }
}
