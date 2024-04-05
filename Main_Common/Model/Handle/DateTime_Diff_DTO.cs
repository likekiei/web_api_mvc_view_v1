using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.Handle
{
    /// <summary>
    /// 日期時間差值DTO
    /// </summary>
    public class DateTime_Diff_DTO
    {
        /// <summary>
        /// 開始值
        /// </summary>
        [Display(Name = "開始值")]
        public DateTime? Date_STA { get; set; }
        /// <summary>
        /// 結束值
        /// </summary>
        [Display(Name = "結束值")]
        public DateTime? Date_END { get; set; }
        /// <summary>
        /// 差值(TimeSpan)
        /// </summary>
        [Display(Name = "差值(TimeSpan)")]
        public TimeSpan? TimeSpan_Diff { get; set; }
        /// <summary>
        /// 年
        /// </summary>
        [Display(Name = "年")]
        public int Year { get; set; }
        /// <summary>
        /// 月
        /// </summary>
        [Display(Name = "月")]
        public int Month { get; set; }
        /// <summary>
        /// 日
        /// </summary>
        [Display(Name = "日")]
        public int Day { get; set; }
    }
}
