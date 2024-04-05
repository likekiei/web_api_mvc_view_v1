using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.ERP
{
    /// <summary>
    /// 請假資訊
    /// </summary>
    public class TakeLeaveDTO
    {
        /// <summary>
        /// 身分證字號
        /// </summary>
        public string IdNo { get; set; }
        /// <summary>
        /// 員工代號
        /// </summary>
        public string Emp_No { get; set; }

        /// <summary>
        /// 請假開始日期
        /// </summary>
        public DateTime SDate { get; set; }
        /// <summary>
        /// 請假開始日期
        /// </summary>
        public DateTime EDate { get; set; }
        /// <summary>
        /// 假別
        /// </summary>
        public string LeaveType { get; set; }
        /// <summary>
        /// 假別代號
        /// </summary>
        public string LeaveNo { get; set; }
        /// <summary>
        /// 時數or天數
        /// </summary>
        public decimal? Count { get; set; }
    }
}
