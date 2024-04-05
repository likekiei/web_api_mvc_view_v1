using Main_Common.Enum;
//using Main_Common.Model.Connect;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.Account
{
    /// <summary>
    /// 【Model】Token
    /// </summary>
    public class Token_Model
    {
        /// <summary>
        /// 登入ID
        /// </summary>
        [Display(Name = "登入ID")]
        public Guid? Login_ID { get; set; }
        /// <summary>
        /// 登入ID
        /// </summary>
        [Display(Name = "登入ID")]
        public string Login_Name { get; set; }
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
        /// <summary>
        /// 登入時間
        /// </summary>
        [Display(Name = "登入時間")]
        public DateTime Login_Time { get; set; }
    }
}
