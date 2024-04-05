using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.Handle.View
{
    /// <summary>
    /// 【DTO】彈出框
    /// </summary>
    public class Dialog_DTO
    {
        /// <summary>
        /// 標題
        /// </summary>
        [Display(Name = "標題")]
        public string Title { get; set; }
        /// <summary>
        /// 內容
        /// </summary>
        [Display(Name = "內容")]
        public string Content { get; set; }
    }
}
