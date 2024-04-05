using Main_Common.Enum.E_ProjectType;
using Main_Common.Enum.E_StatusType;
using Main_Common.Model.Basic;
using Main_Common.Tool.CustomAttribute;
using Main_Resources.Model.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Main_Common.Model.Data.User
{
    /// <summary>
    /// 【Model】使用者
    /// </summary>
    public class User_Model : Basic_Model_ByDataIO
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
        /// 代號 (視同帳號)
        /// </summary>
        [MyMaxLength(128)]
        [MyRequired]
        [Display(Name = "代號")]
        public string? No { get; set; }
        /// <summary>
        /// 名稱
        /// </summary>
        [MyMaxLength(128)]
        [MyRequired]
        [Display(Name = "名稱")]
        public string? Name { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        [MyRequired]
        [Display(Name = "角色Id")]
        public long? RoleId { get; set; }
        /// <summary>
        /// 角色名稱
        /// </summary>
        [Display(Name = "角色名稱")]
        public string? RoleName { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        [MyMaxLength(128)]
        [MyRequired]
        [Display(Name = "密碼")]
        public string? Password { get; set; }
        /// <summary>
        /// 信箱
        /// </summary>
        [EmailAddress(ErrorMessageResourceType = typeof(RES_ModelValidationMessage), ErrorMessageResourceName = nameof(RES_ModelValidationMessage.EmailAddress))]
        [MyRequired()]
        [Display(Name = "信箱")]
        public string? Mail { get; set; }
        /// <summary>
        /// 照片Id
        /// </summary>
        [Display(Name = "照片Id")]
        public long? ImagePhotoId { get; set; }
        /// <summary>
        /// 照片內容
        /// </summary>
        [Display(Name = "照片內容")]
        public byte[]? ImagePhotoByte { get; set; }
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
        public User_Model()
        {
            // ...
        }
        #endregion
    }
}
