using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Main_Common.Model.Data.User
{
    /// <summary>
    /// 【DTO】使用者
    /// </summary>
    public class User_DTO
    {
        #region == 主要屬性 ===============================================================================
        /// <summary>
        /// Key
        /// </summary>
        [Display(Name = "Key")]
        public long? Id { get; set; }
        /// <summary>
        /// 代號
        /// </summary>
        [Display(Name = "代號")]
        public string? No { get; set; }
        /// <summary>
        /// 名稱
        /// </summary>
        [Display(Name = "名稱")]
        public string? Name { get; set; }
        /// <summary>
        /// 查詢日
        /// </summary>
        [Display(Name = "查詢日")]
        public DateTime? QueryDate { get; set; }
        /// <summary>
        /// 公司Id
        /// </summary>
        [Display(Name = "公司Id")]
        public long? CompanyId { get; set; }
        /// <summary>
        /// 公司名稱
        /// </summary>
        [Display(Name = "公司名稱")]
        public string? CompanyName { get; set; }
        #endregion
    }
}
