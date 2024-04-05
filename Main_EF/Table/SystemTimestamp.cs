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
    /// 系統時間戳記 (用來記錄最早跟最晚的執行時間點)
    /// </summary>
    /// <remarks>默認只需存在一筆資料</remarks>
    public class SystemTimestamp
    {
        /// <summary>
        /// 主Key (公司Id)(自行輸入)
        /// </summary>
        [Key]
        [ForeignKey("CompanyInfo")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "主Key")]
        public long CompanyId { get; set; }
        /// <summary>
        /// 每日庫存明細表，最早的一次變更時間 (如有值，表示需從該日期開始重新計算)
        /// </summary>
        [Display(Name = "每日庫存明細表，最早的一次變更時間")]
        public DateTime? DailyStockDetailReport_FirstChangeDate { get; set; }
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
        /// 修改時間
        /// </summary>
        [Display(Name = "修改時間")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 其它欄位
        /// </summary>
        [Display(Name = "其它欄位")]
        public string? Other { get; set; }

        //== 關聯性 ==================================================================================

        #region == 關聯性 ==
        /// <summary>
        /// 公司資訊  [1對1][公司1---1系統時間戳記](依公司為主)(關聯刪除)
        /// </summary>
        //[ForeignKey("CompanyId")]
        public virtual Companys CompanyInfo { get; set; }
        #endregion
    }
}
