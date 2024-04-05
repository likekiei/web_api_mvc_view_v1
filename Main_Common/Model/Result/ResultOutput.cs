using Main_Common.Enum;
using Main_Common.Enum.E_StatusType;
using Main_Common.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.Result
{
    /// <summary>
    /// 處理結果
    /// </summary>
    public class ResultOutput
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
        /// 分頁資訊
        /// </summary>
        public PageDTO PageDTO { get; set; }
        /// <summary>
        /// 訊息s
        /// </summary>
        public List<Message_Model> Message_Infos { get; set; }

        //=====================================================================

        /// <summary>
        /// 建構-初始值
        /// </summary>
        public ResultOutput()
        {
            this.IsSuccess = true;
            this.PageDTO = new PageDTO();
            this.Message_Infos = new List<Message_Model>();
            //this.Exception_Infos = new List<Exception_Model>();
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        public ResultOutput(bool defaultSuccess)
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
            
            this.PageDTO = new PageDTO();
            this.Message_Infos = new List<Message_Model>();
            //this.Exception_Infos = new List<Exception_Model>();
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="message">訊息</param>
        public ResultOutput(bool defaultSuccess, string message)
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

            this.Message = message;
            this.PageDTO = new PageDTO();
            this.Message_Infos = new List<Message_Model>();
            //this.Exception_Infos = new List<Exception_Model>();
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="statusType">狀態碼</param>
        public ResultOutput(bool defaultSuccess, E_StatusCode statusType)
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
            this.PageDTO = new PageDTO();
            this.Message_Infos = new List<Message_Model>();
            //this.Exception_Infos = new List<Exception_Model>();
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="statusType">狀態碼</param>
        /// <param name="message">訊息</param>
        public ResultOutput(bool defaultSuccess, E_StatusCode statusType, string message)
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
            this.PageDTO = new PageDTO();
            this.Message_Infos = new List<Message_Model>();
            //this.Exception_Infos = new List<Exception_Model>();
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="message">訊息</param>
        /// <param name="messageEX">系統例外訊息</param>
        public ResultOutput(bool defaultSuccess, string message, string messageEX)
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

            this.Message = message;
            this.Message_Exception = messageEX;
            this.PageDTO = new PageDTO();
            this.Message_Infos = new List<Message_Model>();
            //this.Exception_Infos = new List<Exception_Model>();
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="statusType">狀態碼</param>
        /// <param name="message">訊息</param>
        /// <param name="messageEX">系統例外訊息</param>
        public ResultOutput(bool defaultSuccess, E_StatusCode statusType, string message, string messageEX)
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
            this.PageDTO = new PageDTO();
            this.Message_Infos = new List<Message_Model>();
            //this.Exception_Infos = new List<Exception_Model>();
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="statusType">狀態碼</param>
        /// <param name="message">訊息</param>
        /// <param name="messageEX">系統例外訊息</param>
        /// <param name="messageOther">其他訊息</param>
        public ResultOutput(bool defaultSuccess, E_StatusCode statusType, string message, string messageEX, string messageOther)
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
            this.Message_Other = messageOther;
            this.PageDTO = new PageDTO();
            this.Message_Infos = new List<Message_Model>();
            //this.Exception_Infos = new List<Exception_Model>();
        }
    }
}
