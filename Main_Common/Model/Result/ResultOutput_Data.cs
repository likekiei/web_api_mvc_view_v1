using Main_Common.Enum.E_StatusType;
using Main_Common.Model.Message;
using Main_Common.Model.Tool;

namespace Main_Common.Model.Result
{
    /// <summary>
    /// 處理結果 + 資料
    /// </summary>
    public class ResultOutput_Data<T>
    {
        #region == 主要屬性 ===============================================================================
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
        public string? E_StatusCode_Name
        {
            get { return this.E_StatusCode.ToString(); }
        }
        /// <summary>
        /// 標題
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// 訊息
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        /// 系統例外訊息
        /// </summary>
        public string? Message_Exception { get; set; }
        /// <summary>
        /// 其他訊息
        /// </summary>
        public string? Message_Other { get; set; }
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
        public Pageing_DTO Pageing_DTO { get; set; }
        /// <summary>
        /// 訊息s
        /// </summary>
        public List<Message_DTO> Message_Infos { get; set; }
        ///// <summary>
        ///// Excel訊息s
        ///// </summary>
        //public List<ExcelMessage_DTO> ExcelMessage_Infos { get; set; }
        ///// <summary>
        ///// 例外訊息
        ///// </summary>
        //public Exception_Model Exception_Info { get; set; }
        ///// <summary>
        ///// 例外訊息s
        ///// </summary>
        //public List<Exception_Model> Exception_Infos { get; set; }
        #endregion

        #region == 建構 ===============================================================================
        /// <summary>
        /// 建構-初始值
        /// </summary>
        public ResultOutput_Data()
        {
            this.IsSuccess = true;
            this.Pageing_DTO = new Pageing_DTO();
            this.Message_Infos = new List<Message_DTO>();
            //this.Exception_Infos = new List<Exception_Model>();
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="data">回傳資料</param>
        public ResultOutput_Data(bool defaultSuccess, T data)
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
            this.Pageing_DTO = new Pageing_DTO();
            this.Message_Infos = new List<Message_DTO>();
            //this.Exception_Infos = new List<Exception_Model>();
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="statusType">狀態碼</param>
        /// <param name="data">回傳資料</param>
        public ResultOutput_Data(bool defaultSuccess, string message, T data)
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
                this.Title = "【失敗】";
                this.E_StatusCode = E_StatusCode.失敗;
            }

            this.Message = message;
            this.Data = data;
            this.Pageing_DTO = new Pageing_DTO();
            this.Message_Infos = new List<Message_DTO>();
            //this.Exception_Infos = new List<Exception_Model>();
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="statusType">狀態碼</param>
        /// <param name="data">回傳資料</param>
        public ResultOutput_Data(bool defaultSuccess, E_StatusCode statusType, T data)
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
            this.Pageing_DTO = new Pageing_DTO();
            this.Message_Infos = new List<Message_DTO>();
            //this.Exception_Infos = new List<Exception_Model>();
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="statusType">狀態碼</param>
        /// <param name="message">訊息</param>
        /// <param name="data">回傳資料</param>
        public ResultOutput_Data(bool defaultSuccess, E_StatusCode statusType, string message, T data)
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
            this.Pageing_DTO = new Pageing_DTO();
            this.Message_Infos = new List<Message_DTO>();
            //this.Exception_Infos = new List<Exception_Model>();
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="statusType">狀態碼</param>
        /// <param name="message">訊息</param>
        /// <param name="messageEX">系統例外訊息</param>
        /// <param name="data">回傳資料</param>
        public ResultOutput_Data(bool defaultSuccess, E_StatusCode statusType, string message, string messageEX, T data)
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
            this.Pageing_DTO = new Pageing_DTO();
            this.Message_Infos = new List<Message_DTO>();
            //this.Exception_Infos = new List<Exception_Model>();
        }
        #endregion

        #region == 方法 ===============================================================================
        //...
        #endregion
    }
}
