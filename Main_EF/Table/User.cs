using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main_Common.Enum.E_ProjectType;

namespace Main_EF.Table
{
    /// <summary>
    /// 使用者
    /// </summary>
    [Index(nameof(GUID), IsUnique = true, Name = "IX_001")] // 不允許重複
    [Index(nameof(CompanyId), nameof(No), IsUnique = true, Name = "IX_002")] // 不允許重複
    public class User
    {
        #region == 欄位 ===============================================================================
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
        /// 代號 (視同帳號)
        /// </summary>
        [MaxLength(128)]
        [Required]
        [Display(Name = "代號")]
        public string? No { get; set; }
        /// <summary>
        /// 名稱
        /// </summary>
        [MaxLength(128)]
        [Required]
        [Display(Name = "名稱")]
        public string? Name { get; set; }
        /// <summary>
        /// 部門Id
        /// </summary>
        [Required]
        [Display(Name = "部門Id")]
        public long DeptId { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        [Required]
        [Display(Name = "角色Id")]
        public long RoleId { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        [MaxLength(128)]
        [Required]
        [Display(Name = "密碼")]
        public string? Password { get; set; }
        /// <summary>
        /// 信箱
        /// </summary>
        [DataType(DataType.EmailAddress)]
        [Display(Name = "信箱")]
        public string? Mail { get; set; }
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
        [Display(Name = "檔案綁定用Guid")]
        public Guid? FileBindGuid { get; set; }
        /// <summary>
        /// 其它欄位
        /// </summary>
        [Display(Name = "其它欄位")]
        public string? Other { get; set; }
        #endregion

        #region == 關聯性 ===============================================================================
        /// <summary>
        /// 公司資訊  [1對多][公司1---*使用者]
        /// </summary>
        [ForeignKey("CompanyId")]
        public virtual Companys CompanyInfo { get; set; }

        /// <summary>
        /// 角色資訊  [1對多][角色1---*使用者]
        /// </summary>
        [ForeignKey("RoleId")]
        public virtual Role RoleInfo { get; set; }

        ///// <summary>
        ///// 部門資訊  [1對多][部門1---*使用者]
        ///// </summary>
        //[ForeignKey("Dept_Id")]
        //public virtual Dept Dept_Info { get; set; }

        ///// <summary>
        ///// 登入狀態清單  [1對多][使用者1---*登入狀態](允許關聯刪除)
        ///// </summary>
        //public virtual ICollection<LoginStatus> LoginStatus_List { get; set; }
        #endregion
    }
}
