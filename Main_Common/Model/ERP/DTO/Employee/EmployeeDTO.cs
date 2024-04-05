using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.ERP
{
    /// <summary>
    /// 員工資訊
    /// </summary>
    public class EmployeeDTO
    {
        /// <summary>
        /// 員工代號
        /// </summary>       
        public string? no { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string? name { get; set; }
        /// <summary>
        /// 身分證
        /// </summary>
        public string? idNumber { get; set; }
        /// <summary>
        /// 部門代號
        /// </summary>
        public string? pointNumber { get; set; }
        /// <summary>
        /// 手機號碼
        /// </summary>
        public string? mobilePhone { get; set; }
        /// <summary>
        /// 到職日
        /// </summary>
        public DateTime? hireDate { get; set; }
        /// <summary>
        /// 離職日
        /// </summary>
        public DateTime? retireDate { get; set; }
        /// <summary>
        /// 終審日期
        /// </summary>
        public DateTime? clsDate { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? updateDate { get; set; }

    }
}
