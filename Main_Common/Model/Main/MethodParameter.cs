using Main_Common.Model.Message;
using Main_Common.Model.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.Main
{
    /// <summary>
    /// 給Method使用的通用屬性參數
    /// <para>使用前請依狀況自行重置</para>
    /// </summary>
    public class MethodParameter
    {
        #region == 主要屬性 ===============================================================================
        /// <summary>
        /// 綁定Key
        /// </summary>
        public Guid BindKey { get; set; }
        /// <summary>
        /// 當前時間 (含毫秒)
        /// </summary>
        public DateTime TodayFull { get; set; }
        /// <summary>
        /// 當前時間 (不含毫秒)
        /// </summary>
        public DateTime Today { get; set; }
        /// <summary>
        /// 成功失敗
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 文字清單
        /// </summary>
        public List<string> Texts { get; set; }
        /// <summary>
        /// 錯誤文字
        /// </summary>
        public string? ErrorText { get; set; }
        /// <summary>
        /// 錯誤文字清單
        /// </summary>
        public List<string> ErrorTexts { get; set; }
        /// <summary>
        /// 拆字結果清單
        /// </summary>
        public List<string> Splits { get; set; }
        /// <summary>
        /// 通用的默認關鍵值訊息
        /// </summary>
        public string? ComFocusText { get; set; }
        /// <summary>
        /// 暫時的默認關鍵值訊息
        /// </summary>
        public string? TmpFocusText { get; set; }
        /// <summary>
        /// 檢查結果
        /// </summary>
        public bool CheckResult { get; set; }
        /// <summary>
        /// 訊息DTO
        /// </summary>
        public Message_DTO MessageDTO { get; set; }
        /// <summary>
        /// 簡易結果回傳
        /// </summary>
        public ResultSimple ResultSimple { get; set; }
        /// <summary>
        /// 回傳用JsonDTO
        /// </summary>
        public ResultJson_DTO ResultJsonDTO { get; set; }
        #endregion

        #region == 建構 ===============================================================================
        /// <summary>
        /// 建構-初始值
        /// </summary>
        public MethodParameter()
        {
            this.Generate_BaseData(null);
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="bindKey">綁定Key</param>
        public MethodParameter(Guid bindKey)
        {
            this.Generate_BaseData(null);
            this.BindKey = bindKey;
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        /// <param name="currDate">當前時間(建議含毫秒)</param>
        public MethodParameter(DateTime currDate)
        {
            this.Generate_BaseData(currDate);
        }
        #endregion

        #region == 方法 ===============================================================================
        /// <summary>
        /// 生成基礎資料(預設值)
        /// </summary>
        /// <param name="currDate">當前時間(建議含毫秒)</param>
        private void Generate_BaseData(DateTime? currDate)
        {
            this.TodayFull = DateTime.UtcNow.AddHours(8);
            this.Today = Convert.ToDateTime(this.TodayFull.ToString());
            this.Texts = new List<string>();
            this.ErrorTexts = new List<string>();
            this.Splits = new List<string>();

            // [有無值][T：有值][F：無值，取當前時間]
            if (currDate.HasValue)
            {
                this.TodayFull = currDate.Value;
                this.Today = Convert.ToDateTime(this.TodayFull.ToString());
            }
            else
            {
                this.TodayFull = DateTime.UtcNow.AddHours(8);
                this.Today = Convert.ToDateTime(this.TodayFull.ToString());
            }
        }

        /// <summary>
        /// 重置Model(訊息相關)
        /// </summary>
        /// <param name="bindKey">綁定Key</param>
        public void Reset_Message(Guid bindKey)
        {
            this.BindKey = bindKey;
            this.IsSuccess = false;
            this.Texts = new List<string>();
            this.ErrorText = null;
            this.ErrorTexts = new List<string>();
            this.Splits = new List<string>();
            this.ComFocusText = null;
            this.TmpFocusText = null;
            this.MessageDTO = null;
            this.ResultSimple = null;
            this.ResultJsonDTO = null;
        }
        #endregion
    }
}
