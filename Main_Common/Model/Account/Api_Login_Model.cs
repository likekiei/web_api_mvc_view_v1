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
    /// 【Api】【Model】登入
    /// </summary>
    public class Api_Login_Model
    {
        /// <summary>
        /// 帳號
        /// </summary>
        [Required]
        [Display(Name = "帳號")]
        public string Account { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        [Required]
        [Display(Name = "密碼")]
        public string Password { get; set; }
    }
}
