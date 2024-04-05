using Main_Common.Enum;
using Main_Common.Enum.E_StatusType;
using Main_Common.Model.Error;
using Main_Common.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.Result
{
    /// <summary>
    /// 【Model】處理結果 + 資料
    /// </summary>
    public class Result_Model_Data<T>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 狀態碼 [預設Default]
        /// </summary>
        public E_StatusCode E_StatusCode { get; set; }
        /// <summary>
        /// 狀態碼名稱
        /// </summary>
        public string E_StatusCode_Name
        {
            get { return this.E_StatusCode.ToString(); }
        }
        /// <summary>
        /// 標題
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 訊息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 系統例外訊息
        /// </summary>
        public string Message_Exception { get; set; }
        /// <summary>
        /// 其他訊息
        /// </summary>
        public string Message_Other { get; set; }
        /// <summary>
        /// 失敗時是否刷新
        /// </summary>
        public bool Is_ErrorRefresh { get; set; }
        /// <summary>
        /// 回傳-資料
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 分頁資訊
        /// </summary>
        public PageDTO PageDTO { get; set; }
        /// <summary>
        /// 例外訊息
        /// </summary>
        public Exception_Model Exception_Info { get; set; }
        /// <summary>
        /// 例外訊息s
        /// </summary>
        public List<Exception_Model> Exception_Infos { get; set; }

        //== 建構 ===================================================================

        /// <summary>
        /// 建構-初始值
        /// </summary>
        public Result_Model_Data()
        {
            this.IsSuccess = true;
            this.PageDTO = new PageDTO();
            this.Exception_Infos = new List<Exception_Model>();
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="data">資料</param>
        public Result_Model_Data(bool defaultSuccess, T data)
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

            this.Data = data;
            this.PageDTO = new PageDTO();
            this.Exception_Infos = new List<Exception_Model>();
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="statusType">狀態碼</param>
        /// <param name="defaultSuccess">資料</param>
        public Result_Model_Data(bool defaultSuccess, E_StatusCode statusType ,T data)
        {
            if (defaultSuccess)
            {
                this.IsSuccess = true;
                this.Title = "【成功】";
            }
            else
            {
                this.IsSuccess = false;
                this.Title = "【失敗】";
            }

            this.E_StatusCode = statusType;
            this.Data = data;
            this.PageDTO = new PageDTO();
            this.Exception_Infos = new List<Exception_Model>();
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="statusType">狀態碼</param>
        /// <param name="message">訊息</param>
        /// <param name="defaultSuccess">資料</param>
        public Result_Model_Data(bool defaultSuccess, E_StatusCode statusType, string message, T data)
        {
            if (defaultSuccess)
            {
                this.IsSuccess = true;
                this.Title = "【成功】";
            }
            else
            {
                this.IsSuccess = false;
                this.Title = "【失敗】";
            }

            this.E_StatusCode = statusType;
            this.Message = message;
            this.Data = data;
            this.PageDTO = new PageDTO();
            this.Exception_Infos = new List<Exception_Model>();
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="statusType">狀態碼</param>
        /// <param name="message">訊息</param>
        /// <param name="messageEX">系統例外訊息</param>
        /// <param name="data">資料</param>
        public Result_Model_Data(bool defaultSuccess, E_StatusCode statusType, string message, string messageEX, T data)
        {
            if (defaultSuccess)
            {
                this.IsSuccess = true;
                this.Title = "【成功】";
            }
            else
            {
                this.IsSuccess = false;
                this.Title = "【失敗】";
            }

            this.E_StatusCode = statusType;
            this.Message = message;
            this.Message_Exception = messageEX;
            this.Data = data;
            this.PageDTO = new PageDTO();
            this.Exception_Infos = new List<Exception_Model>();
        }
    }
}
