
using Main_Common.Tool;

namespace Main_Common.Model.Result
{
    /// <summary>
    /// 回傳Json用DTO
    /// </summary>
    public class ResultJson_DTO
    {
        #region == 主要屬性 ===============================================================================
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool isSuccess { get; set; }
        /// <summary>
        /// 標題
        /// </summary>
        public string? title { get; set; }
        /// <summary>
        /// 訊息
        /// </summary>
        public string? message { get; set; }
        /// <summary>
        /// 指向Url
        /// </summary>
        public string? toUrl { get; set; }
        /// <summary>
        /// Log Url
        /// </summary>
        /// <remarks>前端Alert需要產生導頁至簡易Log頁面的話才給值</remarks>
        public string? logUrl { get; set; }
        #endregion

        #region == 建構 ===============================================================================
        /// <summary>
        /// 建構-初始值
        /// </summary>
        public ResultJson_DTO()
        {
            this.isSuccess = true;
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        public ResultJson_DTO(bool defaultSuccess)
        {
            if (defaultSuccess)
            {
                this.title = "【成功】";
            }
            else
            {
                this.title = "【失敗】";
            }

            this.isSuccess = defaultSuccess;
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="message">訊息</param>
        public ResultJson_DTO(bool defaultSuccess, string message)
        {
            if (defaultSuccess)
            {
                this.title = "【成功】";
            }
            else
            {
                this.title = "【失敗】";
            }

            this.isSuccess = defaultSuccess;
            this.message = message;
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="defaultSuccess">是否預設成功</param>
        /// <param name="title">標題</param>
        /// <param name="message">訊息</param>
        public ResultJson_DTO(bool defaultSuccess, string title, string message)
        {
            if (defaultSuccess)
            {
                this.title = string.IsNullOrEmpty(title) ? "【成功】" : title;
            }
            else
            {
                this.title = string.IsNullOrEmpty(title) ? "【失敗】" : title;
            }

            this.isSuccess = defaultSuccess;
            this.title = title;
            this.message = message;
        }
        #endregion
    }
}
