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
    /// 【DTO】登入狀態
    /// </summary>
    public class LoginStatus_DTO
    {
        #region == 主要屬性 ===============================================================================
        /// <summary>
        /// Key
        /// </summary>
        [Display(Name = "Key")]
        public Guid? Id { get; set; }
        /// <summary>
        /// 使用者Id
        /// </summary>
        [Display(Name = "使用者Id")]
        public long? UserId { get; set; }
        #endregion
    }
}
