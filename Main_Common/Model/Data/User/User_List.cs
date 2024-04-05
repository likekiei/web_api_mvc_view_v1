using Main_Common.Enum.E_ProjectType;
using Main_Common.Enum.E_StatusType;
using Main_Common.Model.Basic;
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
    /// 【List】使用者
    /// </summary>
    public class User_List
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
        /// 公司Id
        /// </summary>
        [Display(Name = "公司Id")]
        public long CompanyId { get; set; }
        /// <summary>
        /// 公司名稱
        /// </summary>
        [Display(Name = "公司名稱")]
        public string? CompanyName { get; set; }
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
        /// 帳號
        /// </summary>
        [Display(Name = "帳號")]
        public string? Account { get; set; }
        /// <summary>
        /// 信箱
        /// </summary>
        [Display(Name = "信箱")]
        public string? Mail { get; set; }
        /// <summary>
        /// 部門Id
        /// </summary>
        [Display(Name = "部門Id")]
        public long? DeptId { get; set; }
        /// <summary>
        /// 部門名稱
        /// </summary>
        [Display(Name = "部門名稱")]
        public string? DeptName { get; set; }
        /// <summary>
        /// 角色權限Id
        /// </summary>
        [Display(Name = "角色權限Id")]
        public long RoleId { get; set; }
        /// <summary>
        /// 角色權限名稱
        /// </summary>
        [Display(Name = "角色權限名稱")]
        public string? RoleName { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        [Display(Name = "備註")]
        public string? Rem { get; set; }
        #endregion

        #region == 其他屬性 ===============================================================================
        // ...
        #endregion

        #region == 建構 ===============================================================================
        /// <summary>
        /// 建構-初始值
        /// </summary>
        public User_List()
        {
            // ...
        }
        #endregion
    }
}
