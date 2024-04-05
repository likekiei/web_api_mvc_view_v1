using Main_Common.Model.Basic;
using Main_Common.Tool.CustomAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Main_Common.Model.Account
{
    /// <summary>
    /// 【Model】登入
    /// </summary>
    public class Login_Model : Basic_Model_ByDataIO
    {
        #region == 主要屬性 ===============================================================================
        /// <summary>
        /// 登入Id
        /// </summary>
        [Display(Name = "登入Id")]
        public Guid? LoginId { get; set; }
        /// <summary>
        /// 帳號
        /// </summary>
        [MyRequired()]
        [Display(Name = "帳號")]
        public string? Account { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        [MyRequired]
        [Display(Name = "密碼")]
        public string? Password { get; set; }
        #endregion

        #region == 其他屬性 ===============================================================================
        // ...
        #endregion

        #region == 建構 ===============================================================================
        /// <summary>
        /// 建構-初始值
        /// </summary>
        public Login_Model()
        {
            this.CompanyId = 0;
        }
        #endregion
    }
}
