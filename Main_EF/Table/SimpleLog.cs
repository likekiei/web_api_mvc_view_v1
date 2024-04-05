using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_EF.Table
{
    public class SimpleLog
    {
        /// <summary>
        /// 主Key
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "主Key")]
        public long ID { get; set; }
        /// <summary>
        /// 紀錄時間
        /// </summary>
        [Display(Name = "紀錄時間")]
        [Required]
        public DateTime EventDateTime { get; set; }
        /// <summary>
        /// 事件等級
        /// </summary>
        [Required]
        [Display(Name = "EventLevel")]
        public string EventLevel { get; set; }        
        /// <summary>
        /// IP Address
        /// </summary>
        [Display(Name = "IP Address")]
        public string IPAddress { get; set; }
        /// <summary>
        /// ActionName
        /// </summary>
        [Display(Name = "ActionName")]
        public string ActionName { get; set; }
        /// <summary>
        /// User
        /// </summary>
        [Display(Name = "User")]
        public string User { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [Display(Name = "Status")]
        public string Status { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        [Display(Name = "Message")]
        public string Message { get; set; }
        /// <summary>
        /// Data
        /// </summary>
        [Display(Name = "Data")]
        public string Data { get; set; }        
    }
}
