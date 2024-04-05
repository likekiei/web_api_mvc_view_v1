using Main_Common.Enum.E_ProjectType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Main_Common.Model.Data.FunctionCode
{
    /// <summary>
    /// 【DTO】功能代碼
    /// </summary>
    public class FunctionCode_DTO
    {
        #region == 主要屬性 ===============================================================================
        /// <summary>
        /// Key
        /// </summary>
        [Display(Name = "Key")]
        public long? Id { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        [Display(Name = "角色Id")]
        public long RoleId { get; set; }
        /// <summary>
        /// 功能代碼Id
        /// </summary>
        [Display(Name = "功能代碼Id")]
        public E_Function FunctionCodeId { get; set; }
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
