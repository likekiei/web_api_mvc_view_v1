using Main_Common.Enum;
using Main_Common.Enum.E_StatusType;
using Main_Common.Model.DTO.Cust;
using Main_Common.Model.DTO.MO_WorkOrder;
using Main_Common.Model.ERP;
using Main_Common.Model.ResultApi.Order;
using Main_Common.Model.Search;
using Main_Common.Model.Tool;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.ResultApi
{
    /// <summary>
    /// 【Api】【回傳結果】
    /// </summary>
    public class OrderResult
    {
        #region == 主要屬性 ===============================================================================
        /// <summary>
        /// Item
        /// </summary>
        [Display(Name = "Item")]
        public int item { get; set; }
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
        /// UserID
        /// </summary>
        public string UserID { get; set; }
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
        //public Pageing_DTO PageDTO { get; set; }
        /// <summary>
        /// 回傳資料
        /// </summary>
        //public List<OrderMFDTO> Datas { get; set; }

        /// <summary>
        /// 回傳資料
        /// </summary>
        public List<MfMoWorkOrder_DTO> Datas { get; set; }

        /// <summary>
        /// 回傳資料
        /// </summary>
        //public OrderDTO Data { get; set; }
        #endregion

        #region == 建構 ===============================================================================
        /// <summary>
        /// 建構-初始值
        /// </summary>
        public OrderResult()
        {
            // ...
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="userID">使用者</param>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="statusType">狀態碼</param>
        /// <param name="message">訊息</param>
        public OrderResult(string userID, bool defaultSuccess, E_StatusCode statusType, string message)
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
        }
        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="userID">使用者</param>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="statusType">狀態碼</param>
        /// <param name="message">訊息</param>
        /// <param name="data">資料</param>
        //public OrderResult(string userID, bool defaultSuccess, E_StatusCode statusType, string message, OrderDTO data)
        //{
        //    if (defaultSuccess)
        //    {
        //        this.IsSuccess = true;
        //        this.Title = "【成功】";
        //    }
        //    else
        //    {
        //        this.IsSuccess = false;
        //        this.Title = "【失敗】";
        //    }

        //    this.E_StatusCode = statusType;
        //    this.Message = message;
        //    this.UserID = userID;
        //    //this.Message_Exception = messageEX;
        //    //this.Message_Other = messageOther;
        //    //this.PageDTO = new PageDTO();
        //    //this.Data = new List<Order_DTO>();
        //    //this.PageDTO = pageDTO;
        //    this.Data = data;
        //    //this.Exception_Infos = new List<Exception_Model>();
        //}
        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="userID">使用者</param>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="statusType">狀態碼</param>
        /// <param name="message">訊息</param>
        /// <param name="data">資料</param>
        public OrderResult(string userID, bool defaultSuccess, E_StatusCode statusType, string message, List<MfMoWorkOrder_DTO> data)
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
            this.UserID = userID;
            //this.Message_Exception = messageEX;
            //this.Message_Other = messageOther;
            //this.PageDTO = new PageDTO();
            //this.Data = new List<Order_DTO>();        
            this.Datas = data;
            //this.Exception_Infos = new List<Exception_Model>();
        }
        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="userID">使用者</param>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="statusType">狀態碼</param>
        /// <param name="message">訊息</param>
        /// <param name="pageDTO">分頁訊息</param>
        /// <param name="data">資料</param>
        //public OrderResults(string userID, bool defaultSuccess, E_StatusCode statusType, string message, Pageing_DTO pageDTO, List<OrderMFDTO> data)
        //{
        //    if (defaultSuccess)
        //    {
        //        this.IsSuccess = true;
        //        this.Title = "【成功】";
        //    }
        //    else
        //    {
        //        this.IsSuccess = false;
        //        this.Title = "【失敗】";
        //    }
        //    this.E_StatusCode = statusType;
        //    this.Message = message;
        //    this.UserID = userID;
        //    //this.Message_Exception = messageEX;
        //    //this.Message_Other = messageOther;
        //    //this.PageDTO = new PageDTO();
        //    //this.Data = new List<Order_DTO>();
        //    this.PageDTO = pageDTO;
        //    this.Datas = data;
        //    //this.Exception_Infos = new List<Exception_Model>();
        //}
    
    }

    #endregion
}
