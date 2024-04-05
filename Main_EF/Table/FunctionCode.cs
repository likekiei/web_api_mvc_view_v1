using Main_Common.Enum.E_ProjectType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Main_EF.Table
{
    /// <summary>
    /// 功能代碼
    /// </summary>
    [Index(nameof(GUID), IsUnique = true, Name = "IX_001")] // 不允許重複
    public class FunctionCode
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
        /// 角色Id
        /// </summary>
        [Required]
        [Display(Name = "角色Id")]
        public long RoleId { get; set; }
        /// <summary>
        /// 功能代碼Id
        /// </summary>
        [Required]
        [Display(Name = "功能代碼Id")]
        public E_Function FunctionCodeId { get; set; }
        /// <summary>
        /// 功能代碼名稱
        /// </summary>
        [Display(Name = "功能代碼名稱")]
        public string FunctionCodeName { get; set; }
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
        /// 公司資訊  [1對多][公司1---*功能代碼]
        /// </summary>
        [ForeignKey("CompanyId")]
        public virtual Companys CompanyInfo { get; set; }

        /// <summary>
        /// 角色資訊  [1對多][角色1---*功能代碼]
        /// </summary>
        [ForeignKey("RoleId")]
        public virtual Role RoleInfo { get; set; }
        #endregion
    }
}
