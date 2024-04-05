using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.DTO.MO_WorkOrder
{
    public class MoWorkOrder_Filter
    {
        /// <summary>
        /// 查詢開始時間
        /// </summary>
        [Display(Name = "查詢開始時間")]
        [DataType(DataType.Date)]
        public DateTime? Query_Date_STA { get; set; }
      

        /// <summary>
        /// 查詢結束時間
        /// </summary>
        [Display(Name = "查詢結束時間")]
        [DataType(DataType.Date)]
        public DateTime? Query_Date_END { get; set; }

        /// <summary>
        /// 製令單號
        /// </summary>
        //[Display(Name = "製令單號")]
        //public string No { get; set; }

    }
}
