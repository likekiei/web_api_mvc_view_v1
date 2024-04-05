using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Main_EF.Table
{
    /// <summary>
    /// TestTemplate Table
    /// </summary>
    [Index(nameof(No), nameof(Name), IsUnique = true, Name = "Index_1")] // 不允許重複
    public class _TestTemplate
    {
        /// <summary>
        /// 主Key
        /// </summary>
        [Key]
        [Display(Name = "主Key")]
        public long Id { get; set; }
        /// <summary>
        /// 代號
        /// </summary>
        [Required]
        [StringLength(128)]
        [Display(Name = "代號")]
        public string? No { get; set; }
        /// <summary>
        /// 名稱
        /// </summary>
        [StringLength(128)]
        [Display(Name = "名稱")]
        public string? Name { get; set; }

        #region 關聯性
        /// <summary>
        /// 所屬之角色資訊  [1對多][角色1---*員工]
        /// </summary>
        //[ForeignKey("Role_Id")]
        //public virtual Role RoleInfo { get; set; }

        /// <summary>
        /// 擁有該角色的使用者  [1對多][角色1---*員工][角色底下有使用者，不可刪除]
        /// </summary>
        //public virtual ICollection<Employee> Employee_Infos { get; set; }

        /// <summary>
        /// 場地資訊  [1對1][場地1---*電錶]
        /// </summary>
        //public virtual Venue Venue_Info { get; set; }

        /// <summary>
        /// 電錶資訊  [單向][關聯電錶]
        /// </summary>
        //[ForeignKey("ElectricMeter_No")]
        //public virtual ElectricMeter ElectricMeter_Info { get; set; }
        #endregion
    }
}
