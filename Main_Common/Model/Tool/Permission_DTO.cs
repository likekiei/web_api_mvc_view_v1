using Main_Common.Enum.E_ProjectType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Main_Common.Model.Tool
{
    /// <summary>
    /// 【DTO】權限參數
    /// </summary>
    public class Permission_DTO
    {
        #region == 主要屬性 ===============================================================================
        /// <summary>
        /// 公司Id
        /// </summary>
        [Display(Name = "公司Id")]
        public long CompanyId { get; set; }
        /// <summary>
        /// 公司等級
        /// </summary>
        [Display(Name = "公司等級")]
        public E_CompanyLevel CompanyLevelId { get; set; }
        /// <summary>
        /// 權限
        /// </summary>
        [Display(Name = "權限")]
        public E_PermissionType PermissionTypeId { get; set; }
        /// <summary>
        /// 前台or後台 [T：後台][F：前台]
        /// </summary>
        public bool IsAdminView { get; set; }
        /// <summary>
        /// 是否為後門權限
        /// </summary>
        [Display(Name = "是否為後門權限")]
        public bool IsBackDoor { get; set; }
        #endregion

        #region == 建構 ===============================================================================
        /// <summary>
        /// 建構-初始值
        /// </summary>
        public Permission_DTO()
        {
            // ...
        }
        #endregion
    }
}
