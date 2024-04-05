using Main_Common.Enum.E_ProjectType;
using Main_Common.Enum.E_StatusType;
using Main_Common.Model.Basic;
using Main_Common.Tool.CustomAttribute;
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
    /// 【List】功能代碼
    /// </summary>
    public class FunctionCode_List
    {
        #region == private的屬性 ===============================================================================
        ///// <summary>
        ///// 視力
        ///// </summary>
        //[Display(Name = "視力")]
        //private decimal? _Eyesight { get; set; }
        //private decimal? _AA { get; set; }
        #endregion

        #region == 主要屬性 ===============================================================================
        /// <summary>
        /// 主Key
        /// </summary>
        [Display(Name = "主Key")]
        public long? Id { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        [Display(Name = "角色Id")]
        public long? RoleId { get; set; }
        /// <summary>
        /// 角色名稱
        /// </summary>
        [Display(Name = "角色名稱")]
        public string? RoleName { get; set; }
        /// <summary>
        /// 功能代碼Id
        /// </summary>
        [Display(Name = "功能代碼Id")]
        public E_Function? FunctionCodeId { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        [Display(Name = "備註")]
        public string? Rem { get; set; }
        /// <summary>
        /// 公司Id
        /// </summary>
        [Display(Name = "公司Id")]
        public long CompanyId { get; set; }
        /// <summary>
        /// 公司名稱
        /// </summary>
        [Display(Name = "公司名稱")]
        public string? CompanyName { get; set; }
        #endregion

        #region == 其他屬性 ===============================================================================
        // ...
        #endregion

        #region == 建構 ===============================================================================
        /// <summary>
        /// 建構-初始值
        /// </summary>
        public FunctionCode_List()
        {
            // ...
        }
        #endregion
    }
}
