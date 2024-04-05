using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.ERP.DTO.Employee
{
    /// <summary>
    /// 【舊版】打卡紀錄
    /// </summary>
    public class PunchRecordDTO
    {
        public string idNumber { get; set; }
        public DateTime recordedDateTime { get; set; }
    }
}
