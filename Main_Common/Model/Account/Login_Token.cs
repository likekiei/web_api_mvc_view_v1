using Main_Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.Account
{
    /// <summary>
    /// 【Model】登入Token
    /// </summary>
    public class Login_Token
    {
        /// <summary>
        /// 登入後取得之token
        /// </summary>
        [Display(Name = "登入後取得之token")]
        public string Token { get; set; }
        /// <summary>
        /// 登入結果
        /// </summary>
        [Display(Name = "登入結果")]
        public bool Result { get; set; }
        /// <summary>
        /// 系統訊息
        /// </summary>
        [Display(Name = "系統訊息")]
        public string Message { get; set; }

        // == 建構 ===================================================================

        #region == 建構 ==
        /// <summary>
        /// 建構-初始值
        /// </summary>
        public Login_Token()
        {
            // ...
        }

        /// <summary>
        /// 建構-初始值
        /// </summary>
        public Login_Token(bool isSuccess)
        {
            this.Result = isSuccess;
        }
        #endregion

    }
}
