using Main_Common.Enum.E_ProjectType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Main_Common.Model.Data.Company
{
    /// <summary>
    /// 【DTO】公司
    /// </summary>
    public class Company_DTO
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
        /// 公司分級Id
        /// </summary>
        [Display(Name = "公司分級Id")]
        public E_CompanyLevel CompanyLevelId { get; set; }
        #endregion
    }
}
