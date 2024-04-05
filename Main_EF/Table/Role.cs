using Main_Common.Enum.E_ProjectType;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_EF.Table
{
    /// <summary>
    /// 角色
    /// </summary>
    [Index(nameof(GUID), IsUnique = true, Name = "IX_001")] // 不允許重複
    [Index(nameof(CompanyId), nameof(No), IsUnique = true, Name = "IX_002")] // 不允許重複
    public class Role
    {
        /// <summary>
        /// 主Key
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "主Key")]
        public long Id { get; set; }
        /// <summary>
        /// Guid Key (如有需要，可做映射用)
        /// </summary>
        [Required]
        [Display(Name = "Guid")]
        public Guid GUID { get; set; }
        /// <summary>
        /// 公司Id
        /// </summary>
        [Required]
        [Display(Name = "公司Id")]
        public long CompanyId { get; set; }
        /// <summary>
        /// 代號
        /// </summary>
        [MaxLength(128)]
        [Required]
        [Display(Name = "代號")]
        public string? No { get; set; }
        /// <summary>
        /// 名稱
        /// </summary>
        [StringLength(128)]
        [Required]
        [Display(Name = "名稱")]
        public string? Name { get; set; }
        /// <summary>
        /// 權限種類Id
        /// </summary>
        [Display(Name = "權限種類Id")]
        public E_PermissionType PermissionTypeId { get; set; }
        /// <summary>
        /// 權限種類名稱
        /// </summary>
        [Display(Name = "權限種類名稱")]
        public string? PermissionTypeName { get; set; }
        /// <summary>
        /// 是否停用 [T：停用][F：啟用]
        /// </summary>
        [Display(Name = "是否停用")]
        public bool IsStop { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        [Display(Name = "備註")]
        public string? Rem { get; set; }
        /// <summary>
        /// 建立時間
        /// </summary>
        [Display(Name = "建立時間")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 建立人Id
        /// </summary>
        [Display(Name = "建立人Id")]
        public long? CreateManId { get; set; }
        /// <summary>
        /// 建立人名稱
        /// </summary>
        [Display(Name = "建立人名稱")]
        public string? CreateManName { get; set; }
        /// <summary>
        /// 修改時間
        /// </summary>
        [Display(Name = "修改時間")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 修改人Id
        /// </summary>
        [Display(Name = "修改人Id")]
        public long? UpdateManId { get; set; }
        /// <summary>
        /// 建立人名稱
        /// </summary>
        [Display(Name = "修改人名稱")]
        public string? UpdateManName { get; set; }
        /// <summary>
        /// 外部綁定Key
        /// </summary>
        [Display(Name = "外部綁定Key")]
        public string? OutsideBindKey { get; set; }
        /// <summary>
        /// 檔案綁定用Guid
        /// </summary>
        [Display(Name = "檔案綁定Guid")]
        public Guid? FileBindGuid { get; set; }
        /// <summary>
        /// 其它欄位
        /// </summary>
        [Display(Name = "其它欄位")]
        public string? Other { get; set; }

        //== 關聯性 ==================================================================================

        #region == 關聯性 ==
        /// <summary>
        /// 公司資訊  [1對多][公司1---*角色]
        /// </summary>
        [ForeignKey("CompanyId")]
        public virtual Companys CompanyInfo { get; set; }

        /// <summary>
        /// 該角色的功能代碼  [1對多][角色1---*功能代碼](有資料，則不允許刪除)
        /// </summary>
        public virtual ICollection<FunctionCode> FunctionCodeList { get; set; }

        /// <summary>
        /// 該角色的使用者  [1對多][角色1---*使用者](有資料，則不允許刪除)
        /// </summary>
        public virtual ICollection<User> UserList { get; set; }

        ///// <summary>
        ///// 可使用之功能代碼 [1對多][角色1---*功能碼](需連帶刪除相關聯的功能代碼)
        ///// </summary>
        //public virtual ICollection<Function> Function_Infos { get; set; }
        #endregion
    }
}
