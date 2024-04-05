using Main_Common.Enum.E_ProjectType;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_EF.Table
{
    /// <summary>
    /// 公司
    /// </summary>
    [Index(nameof(GUID), IsUnique = true, Name = "IX_001")] // 不允許重複
    [Index(nameof(No), IsUnique = true, Name = "IX_002")] // 不允許重複
    public class Companys
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
        /// 代號 (視同帳號)
        /// </summary>
        [MaxLength(128)]
        [Required]
        [Display(Name = "代號")]
        public string? No { get; set; }
        /// <summary>
        /// 名稱
        /// </summary>
        [Required]
        [Display(Name = "名稱")]
        public string? Name { get; set; }
        /// <summary>
        /// 公司分級Id
        /// </summary>
        [Required]
        [Display(Name = "公司分級Id")]
        public E_CompanyLevel CompanyLevelId { get; set; }
        /// <summary>
        /// 公司分級名稱
        /// </summary>
        [Display(Name = "公司分級名稱")]
        public string? CompanyLevelName { get; set; }
        /// <summary>
        /// 統編
        /// </summary>
        [Display(Name = "統編")]
        public string? UnifiedNumber { get; set; }
        /// <summary>
        /// 電話
        /// </summary>
        [Display(Name = "電話")]
        public string? Tel { get; set; }
        /// <summary>
        /// 傳真
        /// </summary>
        [Display(Name = "傳真")]
        public string? Fax { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        [Display(Name = "公司地址")]
        public string? Address1 { get; set; }
        /// <summary>
        /// 通訊地址
        /// </summary>
        [Display(Name = "通訊地址")]
        public string? Address2 { get; set; }
        /// <summary>
        /// 信箱
        /// </summary>
        [EmailAddress]
        [Display(Name = "信箱")]
        public string? EMail { get; set; }
        /// <summary>
        /// 負責人
        /// </summary>
        [Display(Name = "負責人")]
        public string? ResponsibMan { get; set; }
        /// <summary>
        /// 聯絡人
        /// </summary>
        [Display(Name = "聯絡人")]
        public string? ContactMan { get; set; }
        /// <summary>
        /// 聯絡人職稱
        /// </summary>
        [Display(Name = "聯絡人職稱")]
        public string? ContactManJobName { get; set; }
        /// <summary>
        /// 聯絡人手機
        /// </summary>
        [Display(Name = "聯絡人手機")]
        public string? ContactManPhone { get; set; }
        /// <summary>
        /// 是否為預設選取項目
        /// </summary>
        [Display(Name = "是否為預設選取項目")]
        public bool IsDefaultSelected { get; set; }
        /// <summary>
        /// 是否停用
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

        //== 關聯性 ==================================================================================

        #region == 關聯性 ==
        /// <summary>
        /// 系統時間戳記資訊  [1對1][公司1---1系統時間戳記](依公司為主)(關聯刪除)
        /// </summary>
        public virtual SystemTimestamp SystemTimestampInfo { get; set; }
        /// <summary>
        /// 該公司的使用者  [1對多][公司1---*使用者](有資料，則不允許刪除)
        /// </summary>
        public virtual ICollection<User> UserList { get; set; }

        ///// <summary>
        ///// 該公司的角色  [1對多][公司1---*角色](有資料，則不允許刪除)
        ///// </summary>
        //public virtual ICollection<Role> Role_Infos { get; set; }

        ///// <summary>
        ///// 該公司的部門  [1對多][公司1---*部門](有資料，則不允許刪除)
        ///// </summary>
        //public virtual ICollection<Dept> Dept_Infos { get; set; }

        ///// <summary>
        ///// 該公司的使用者  [1對多][公司1---*使用者](有資料，則不允許刪除)
        ///// </summary>
        //public virtual ICollection<User> User_Infos { get; set; }
        #endregion
    }
}
