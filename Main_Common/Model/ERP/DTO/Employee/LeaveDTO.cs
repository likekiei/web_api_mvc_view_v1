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
    public class LeaveDTO
    {
        /// <summary>
        /// 身分證字號
        /// </summary>
        public string idNumber { get; set; }       
        /// <summary>
        /// 請假開始日期
        /// </summary>
        public DateTime leaveDate { get; set; }       
        /// <summary>
        /// 假別
        /// </summary>
        public int leaveType { get; set; }
        /// <summary>
        /// 假別名稱
        /// </summary>
        public string note { get; set; }
       
    }
}
